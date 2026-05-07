using BabyLog.Client.Models;
using BabyLog.Client.Services;

namespace BabyLog.Client.ViewModels
{
    public class ChildViewModel
    {
        private readonly ChildApiService _childApiService;
        private readonly CustomerApiService _customerApiService;

        public ChildViewModel(
            ChildApiService childApiService,
            CustomerApiService customerApiService)
        {
            _childApiService = childApiService;
            _customerApiService = customerApiService;
        }

        // List of children for logged-in customer
        public List<Child> Children { get; set; } = new();

        // Selected child
        public Child? SelectedChild { get; set; }

        // Logged-in customer from BabyFællesskab API
        public CustomerInfo? CurrentCustomer { get; set; }

        // Error message shown in UI
        public string? ErrorMessage { get; set; }

        // Loads current customer from API
        public virtual async Task LoadCurrentCustomerAsync()
        {
            CurrentCustomer = null;

            try
            {
                CurrentCustomer =
                    await _customerApiService.GetCurrentCustomerAsync();

                // Customer was not found
                if (CurrentCustomer == null)
                {
                    ErrorMessage = "Kunden kunne ikke findes.";
                }
            }
            catch
            {
                ErrorMessage =
                    "Kunde ikke hente kundeinformation.";
            }
        }

        // UC: Create child
        public virtual async Task<bool> CreateChildAsync(Child child)
        {
            ErrorMessage = null;

            await LoadCurrentCustomerAsync();

            // Customer not logged in
            if (CurrentCustomer == null)
                return false;

            // Validation
            if (string.IsNullOrWhiteSpace(child.FirstName))
            {
                ErrorMessage = "Fornavn er påkrævet.";
                return false;
            }

            // Connect child to logged-in customer
            child.CustomerId = CurrentCustomer.CustomerId;

            try
            {
                await _childApiService.CreateChildAsync(child);

                return true;
            }
            catch
            {
                ErrorMessage =
                    "Barnet kunne ikke oprettes.";

                return false;
            }
        }

        // UC: View children
        public virtual async Task LoadChildrenAsync()
        {
            ErrorMessage = null;

            await LoadCurrentCustomerAsync();

            // Customer not logged in
            if (CurrentCustomer == null)
                return;

            try
            {
                Children =
                    await _childApiService
                        .GetChildrenByCustomerAsync(
                            CurrentCustomer.CustomerId
                        );
            }
            catch
            {
                ErrorMessage =
                    "Børn kunne ikke indlæses.";
            }
        }

        // UC: View one child
        public virtual async Task LoadChildAsync(int childId)
        {
            ErrorMessage = null;

            await LoadCurrentCustomerAsync();

            // Customer not logged in
            if (CurrentCustomer == null)
                return;

            try
            {
                var child =
                    await _childApiService.GetChildAsync(childId);

                // Security check
                if (child == null ||
                    child.CustomerId != CurrentCustomer.CustomerId)
                {
                    ErrorMessage =
                        "Barnet blev ikke fundet, eller du har ikke adgang.";

                    return;
                }

                SelectedChild = child;
            }
            catch
            {
                ErrorMessage =
                    "Barnet kunne ikke indlæses.";
            }
        }

        // UC: Update child
        public virtual async Task<bool> UpdateChildAsync(Child child)
        {
            ErrorMessage = null;

            await LoadCurrentCustomerAsync();

            // Customer not logged in
            if (CurrentCustomer == null)
                return false;

            // Ensure child belongs to logged-in customer
            child.CustomerId = CurrentCustomer.CustomerId;

            try
            {
                await _childApiService.UpdateChildAsync(child);

                return true;
            }
            catch
            {
                ErrorMessage =
                    "Barnet kunne ikke opdateres.";

                return false;
            }
        }

        // UC: Delete child
        public virtual async Task<bool> DeleteChildAsync(int childId)
        {
            ErrorMessage = null;

            await LoadCurrentCustomerAsync();

            // Customer not logged in
            if (CurrentCustomer == null)
                return false;

            try
            {
                // Security check
                var child =
                    await _childApiService.GetChildAsync(childId);

                if (child == null ||
                    child.CustomerId != CurrentCustomer.CustomerId)
                {
                    ErrorMessage =
                        "Barnet blev ikke fundet, eller du har ikke adgang.";

                    return false;
                }

                await _childApiService.DeleteChildAsync(childId);

                return true;
            }
            catch
            {
                ErrorMessage =
                    "Barnet kunne ikke slettes.";

                return false;
            }
        }
    }
}