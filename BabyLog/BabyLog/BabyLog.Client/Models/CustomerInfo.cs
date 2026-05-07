namespace BabyLog.Client.Models
{
    // Customer information received from API
    // Used in ViewModels and services
    public class CustomerInfo
    {
        public int CustomerId { get; set; }

        // Customer first name
        public string FirstName { get; set; } = string.Empty;

        // Customer last name
        public string LastName { get; set; } = string.Empty;
    }
}