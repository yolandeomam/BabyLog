using BabyLog.Client.Models;
using BabyLog.Client.ViewModels;

namespace BabyLogTests.Fakes
{
    // Fake ViewModel used for BUnit tests
    // Prevents real API/database calls during testing
    public class FakeSleepViewModel : SleepViewModel
    {
        public FakeSleepViewModel()
            : base(null!)
        {
        }

        // Fake sleep list
        public override Task LoadSleepByChildAsync(int childId)
        {
            Sleeps = new List<Sleep>
            {
                new Sleep
                {
                    SleepId = 1,
                    ChildId = childId,
                    SleepDate = new DateTime(2026, 5, 14),
                    StartTime = new TimeSpan(8, 30, 0),
                    EndTime = new TimeSpan(10, 0, 0)
                }
            };

            return Task.CompletedTask;
        }

        // Fake selected sleep
        public override Task LoadSleepAsync(int sleepId)
        {
            SelectedSleep = new Sleep
            {
                SleepId = sleepId,
                ChildId = 1,
                SleepDate = new DateTime(2026, 5, 14),
                StartTime = new TimeSpan(8, 30, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };

            return Task.CompletedTask;
        }

        // Fake create sleep operation
        public override Task<bool> CreateSleepAsync(Sleep sleep)
        {
            return Task.FromResult(true);
        }

        // Fake update sleep operation
        public override Task<bool> UpdateSleepAsync(Sleep sleep)
        {
            return Task.FromResult(true);
        }

        // Fake delete sleep operation
        public override Task<bool> DeleteSleepAsync(int sleepId)
        {
            return Task.FromResult(true);
        }

        // Fake graph data
        public override Task LoadSleepGraphDataAsync(int childId)
        {
            DailySleepPeriods = new List<SleepPeriodGraphItem>
            {
                new SleepPeriodGraphItem
                {
                    Label = "08:30 - 10:00",
                    StartHour = 8.5,
                    EndHour = 10,
                    DurationHours = 1.5m
                }
            };

            WeeklySleepDevelopment = new List<SleepGraphItem>
            {
                new SleepGraphItem
                {
                    Label = "man.",
                    TotalHours = 1.5m,
                    SleepCount = 1
                }
            };

            MonthlySleepDevelopment = new List<SleepGraphItem>
            {
                new SleepGraphItem
                {
                    Label = "14/05",
                    TotalHours = 1.5m,
                    SleepCount = 1
                }
            };

            YearlySleepDevelopment = new List<SleepGraphItem>
            {
                new SleepGraphItem
                {
                    Label = "maj",
                    TotalHours = 1.5m,
                    SleepCount = 1
                }
            };

            return Task.CompletedTask;
        }
    }
}