using System.ComponentModel.DataAnnotations;

namespace BabyLog.Models
{
    public class Sleep
    {
        public int SleepId { get; set; }

        // Reference to child
        [Required]
        public int ChildId { get; set; }

        // Date for the sleep registration
        [Required]
        public DateTime SleepDate { get; set; }

        // Sleep start time
        [Required]
        public TimeSpan StartTime { get; set; }

        // Sleep end time
        [Required]
        public TimeSpan EndTime { get; set; }
    }
}