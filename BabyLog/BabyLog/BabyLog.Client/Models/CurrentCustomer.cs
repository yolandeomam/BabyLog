namespace BabyLog.Client.Models
{
    // Represents logged-in customer in BabyLog
    // Received from BabyFællesskab API
    public class CurrentCustomer
    {
        public int CustomerId { get; set; }

        // Customer first name
        public string FirstName { get; set; } = string.Empty;

        // Customer last name
        public string LastName { get; set; } = string.Empty;
    }
}