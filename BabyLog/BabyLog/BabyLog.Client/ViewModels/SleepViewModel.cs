using BabyLog.Client.Models;
using BabyLog.Client.Services;
using System.Globalization;

namespace BabyLog.Client.ViewModels
{
    public class SleepViewModel
    {
        private readonly SleepApiService _sleepApiService;

        public SleepViewModel(SleepApiService sleepApiService)
        {
            _sleepApiService = sleepApiService;
        }

        // List of sleep registrations
        public List<Sleep> Sleeps { get; set; } = new();

        // Selected sleep registration
        public Sleep? SelectedSleep { get; set; }

        // Error message shown in UI
        public string? ErrorMessage { get; set; }

        // Selected date for graphs
        public DateTime SelectedDate { get; set; } = DateTime.Today;

        // Daily sleep timeline data
        public List<SleepPeriodGraphItem> DailySleepPeriods { get; set; } = new();

        // Weekly sleep development graph data
        public List<SleepGraphItem> WeeklySleepDevelopment { get; set; } = new();

        // Monthly sleep development graph data
        public List<SleepGraphItem> MonthlySleepDevelopment { get; set; } = new();

        // Yearly sleep development graph data
        public List<SleepGraphItem> YearlySleepDevelopment { get; set; } = new();

        // UC: View sleep registrations
        public virtual async Task LoadSleepByChildAsync(int childId)
        {
            ErrorMessage = null;

            try
            {
                Sleeps = await _sleepApiService.GetSleepByChildAsync(childId);
            }
            catch
            {
                ErrorMessage = "Søvnregistreringer kunne ikke indlæses.";
            }
        }

        // UC: View one sleep registration
        public virtual async Task LoadSleepAsync(int sleepId)
        {
            ErrorMessage = null;

            try
            {
                SelectedSleep = await _sleepApiService.GetSleepAsync(sleepId);

                if (SelectedSleep == null)
                {
                    ErrorMessage = "Søvnregistreringen blev ikke fundet.";
                }
            }
            catch
            {
                ErrorMessage = "Søvnregistreringen kunne ikke indlæses.";
            }
        }

        // UC: Create sleep
        public virtual async Task<bool> CreateSleepAsync(Sleep sleep)
        {
            ErrorMessage = null;

            if (sleep.SleepDate == default)
            {
                ErrorMessage = "Søvndato er påkrævet.";
                return false;
            }

            if (sleep.EndTime <= sleep.StartTime)
            {
                ErrorMessage = "Sluttidspunkt skal være efter starttidspunkt.";
                return false;
            }

            try
            {
                await _sleepApiService.CreateSleepAsync(sleep);

                return true;
            }
            catch
            {
                ErrorMessage = "Søvnregistreringen kunne ikke oprettes.";

                return false;
            }
        }

        // UC: Update sleep
        public virtual async Task<bool> UpdateSleepAsync(Sleep sleep)
        {
            ErrorMessage = null;

            if (sleep.EndTime <= sleep.StartTime)
            {
                ErrorMessage = "Sluttidspunkt skal være efter starttidspunkt.";

                return false;
            }

            try
            {
                await _sleepApiService.UpdateSleepAsync(sleep);

                return true;
            }
            catch
            {
                ErrorMessage = "Søvnregistreringen kunne ikke opdateres.";

                return false;
            }
        }

        // UC: Delete sleep
        public virtual async Task<bool> DeleteSleepAsync(int sleepId)
        {
            ErrorMessage = null;

            try
            {
                await _sleepApiService.DeleteSleepAsync(sleepId);

                return true;
            }
            catch
            {
                ErrorMessage = "Søvnregistreringen kunne ikke slettes.";

                return false;
            }
        }

        // UC: View sleep graphs
        public virtual async Task LoadSleepGraphDataAsync(int childId)
        {
            ErrorMessage = null;

            try
            {
                Sleeps = await _sleepApiService.GetSleepByChildAsync(childId);

                GenerateDailySleepData();
                GenerateWeeklySleepData();
                GenerateMonthlySleepData();
                GenerateYearlySleepData();
            }
            catch
            {
                ErrorMessage = "Søvngrafer kunne ikke indlæses.";
            }
        }

        // Refresh graphs when date changes
        public void RefreshGraphs()
        {
            GenerateDailySleepData();
            GenerateWeeklySleepData();
            GenerateMonthlySleepData();
            GenerateYearlySleepData();
        }

        // Daily graph
        private void GenerateDailySleepData()
        {
            DailySleepPeriods.Clear();

            var selectedSleeps = Sleeps
                .Where(s => s.SleepDate.Date == SelectedDate.Date)
                .OrderBy(s => s.StartTime);

            foreach (var sleep in selectedSleeps)
            {
                var endHour =
                    sleep.EndTime < sleep.StartTime
                        ? sleep.EndTime.TotalHours + 24
                        : sleep.EndTime.TotalHours;

                DailySleepPeriods.Add(new SleepPeriodGraphItem
                {
                    Label =
                        $"{sleep.StartTime:hh\\:mm} - {sleep.EndTime:hh\\:mm}",

                    StartHour = sleep.StartTime.TotalHours,

                    EndHour = endHour,

                    DurationHours = CalculateSleepHours(sleep)
                });
            }
        }

        // Weekly graph
        private void GenerateWeeklySleepData()
        {
            var startOfWeek =
                SelectedDate.Date.AddDays(-(int)SelectedDate.DayOfWeek + 1);

            var endOfWeek = startOfWeek.AddDays(7);

            WeeklySleepDevelopment = Sleeps
                .Where(s =>
                    s.SleepDate.Date >= startOfWeek &&
                    s.SleepDate.Date < endOfWeek)
                .GroupBy(s => s.SleepDate.Date)
                .Select(g => new SleepGraphItem
                {
                    Label = g.Key.ToString("ddd"),

                    TotalHours = g.Sum(s => CalculateSleepHours(s)),

                    SleepCount = g.Count()
                })
                .OrderBy(x => x.Label)
                .ToList();
        }

        // Monthly graph
        private void GenerateMonthlySleepData()
        {
            MonthlySleepDevelopment = Sleeps
                .Where(s =>
                    s.SleepDate.Month == SelectedDate.Month &&
                    s.SleepDate.Year == SelectedDate.Year)
                .GroupBy(s => s.SleepDate.Date)
                .Select(g => new SleepGraphItem
                {
                    Label = g.Key.ToString("dd/MM"),

                    TotalHours = g.Sum(s => CalculateSleepHours(s)),

                    SleepCount = g.Count()
                })
                .OrderBy(x => x.Label)
                .ToList();
        }

        // Yearly graph
        private void GenerateYearlySleepData()
        {
            YearlySleepDevelopment = Sleeps
                .Where(s => s.SleepDate.Year == SelectedDate.Year)
                .GroupBy(s => s.SleepDate.Month)
                .Select(g => new SleepGraphItem
                {
                    Label = CultureInfo.CurrentCulture
                        .DateTimeFormat
                        .GetAbbreviatedMonthName(g.Key),

                    TotalHours =
                        Math.Round(
                            g.Average(s => CalculateSleepHours(s)),
                            1),

                    SleepCount = g.Count()
                })
                .OrderBy(x => x.Label)
                .ToList();
        }

        // Shared sleep hour calculation
        private decimal CalculateSleepHours(Sleep sleep)
        {
            var duration = sleep.EndTime - sleep.StartTime;

            // Handles sleep across midnight
            if (duration.TotalMinutes < 0)
            {
                duration = duration.Add(TimeSpan.FromHours(24));
            }

            return (decimal)Math.Round(duration.TotalHours, 2);
        }
    }

    // Daily sleep timeline data
    public class SleepPeriodGraphItem
    {
        // Sleep label shown in graph
        public string Label { get; set; } = string.Empty;

        // Sleep start hour
        public double StartHour { get; set; }

        // Sleep end hour
        public double EndHour { get; set; }

        // Total sleep duration
        public decimal DurationHours { get; set; }
    }

    // Development graph data
    public class SleepGraphItem
    {
        // Label shown in graph
        public string Label { get; set; } = string.Empty;

        // Total sleep hours
        public decimal TotalHours { get; set; }

        // Number of sleep periods
        public int SleepCount { get; set; }
    }
}