using BabyLog.Client.Models;
using BabyLog.Client.ViewModels;

namespace BabyLogTests.Fakes
{
    // Fake ViewModel used for BUnit tests
    // Prevents real API/database calls during testing
    public class FakeChildViewModel : ChildViewModel
    {
        public FakeChildViewModel()
            : base(null!, null!)
        {
        }

        // Fake logged-in customer
        public override Task LoadCurrentCustomerAsync()
        {
            CurrentCustomer = new CustomerInfo
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "Customer"
            };

            return Task.CompletedTask;
        }

        // Fake child list
        public override Task LoadChildrenAsync()
        {
            Children = new List<Child>
            {
                new Child
                {
                    ChildId = 1,
                    FirstName = "Augusta",
                    BirthDate = new DateTime(2024, 5, 10),
                    Gender = Gender.Female,
                    CustomerId = 1,
                    ChildCreatedDate =
                        new DateTime(2026, 5, 7)
                }
            };

            return Task.CompletedTask;
        }

        // Fake selected child
        public override Task LoadChildAsync(int childId)
        {
            SelectedChild = new Child
            {
                ChildId = childId,
                FirstName = "Augusta",
                BirthDate = new DateTime(2024, 5, 10),
                Gender = Gender.Female,
                CustomerId = 1,
                ChildCreatedDate =
                    new DateTime(2026, 5, 7)
            };

            return Task.CompletedTask;
        }

        // Fake create child operation
        public override Task<bool> CreateChildAsync(Child child)
        {
            return Task.FromResult(true);
        }

        // Fake update child operation
        public override Task<bool> UpdateChildAsync(Child child)
        {
            return Task.FromResult(true);
        }

        // Fake delete child operation
        public override Task<bool> DeleteChildAsync(int childId)
        {
            return Task.FromResult(true);
        }
    }
}