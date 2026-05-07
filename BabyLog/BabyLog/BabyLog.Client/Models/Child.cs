using System.ComponentModel.DataAnnotations;

namespace BabyLog.Client.Models
{
    public class Child
    {
        public int ChildId { get; set; }

        // Reference to Customer from BabyFællesskab
        [Required]
        public int CustomerId { get; set; }

        // Child first name
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        // Child birth date
        [Required]
        public DateTime BirthDate { get; set; }

        // Child gender
        [Required]
        public Gender Gender { get; set; }

        // When child profile was created
        [Required]
        public DateTime ChildCreatedDate { get; set; } = DateTime.UtcNow;
    }
}