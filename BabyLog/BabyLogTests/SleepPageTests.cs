using ApexCharts;
using Bunit;
using BabyLog.Client.Pages.Sleep;
using BabyLog.Client.Services;
using BabyLog.Client.ViewModels;
using BabyLogTests.Fakes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BabyLogTests.BUnitTests
{
    public class SleepPageTests : TestContext
    {
        /*
         Sleep list -> Create sleep

         Test:
         Verify that the "Opret søvn" button exists
         and navigates correctly to /sleep/create
        */
        [Fact]
        public void SleepList_Should_Navigate_To_Create_Sleep_Page()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<SleepViewModel, FakeSleepViewModel>();

            var navigationManager =
                Services.GetRequiredService<NavigationManager>();

            // Act
            var component = Render<SleepList>(
                parameters => parameters.Add(p => p.ChildId, 1)
            );

            var button = component.Find("button.btn-primary");
            button.Click();

            // Assert
            Assert.Contains("/children/1/sleep/create", navigationManager.Uri);
        }

        /*
         Sleep list should show sleep registration

         Test:
         Verify that existing sleep data is displayed:
         - Sleep date
         - Start time
         - End time
        */
        [Fact]
        public void SleepList_Should_Show_Sleep_Registration()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<SleepViewModel, FakeSleepViewModel>();

            // Act
            var component = Render<SleepList>(
                parameters => parameters.Add(p => p.ChildId, 1)
            );

            // Assert
            Assert.Contains("Søvnregistrering", component.Markup);
            Assert.Contains("08:30", component.Markup);
            Assert.Contains("10:00", component.Markup);
        }

        /*
         Delete sleep page should show confirmation text

         Test:
         Verify that delete page shows:
         "Er du sikker?"
        */
        [Fact]
        public void DeleteSleep_Should_Show_Confirmation_Text()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<SleepViewModel, FakeSleepViewModel>();

            // Act
            var component = Render<DeleteSleep>(
                parameters => parameters
                    .Add(p => p.ChildId, 1)
                    .Add(p => p.SleepId, 1)
            );

            // Assert
            Assert.Contains("Er du sikker?", component.Markup);
            Assert.Contains("Slet", component.Markup);
        }

        /*
         Sleep graph page should show graph sections

         Test:
         Verify that graph page shows:
         - Søvn på dagen
         - Udvikling over ugen
         - Udvikling over måneden
         - Udvikling over året
        */
        [Fact]
        public void SleepGraph_Should_Show_Graph_Sections()
        {
            // Arrange
            JSInterop.Mode = JSRuntimeMode.Loose;

            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<SleepViewModel, FakeSleepViewModel>();
            Services.AddApexCharts();

            // Act
            var component = Render<SleepGraph>(
                parameters => parameters.Add(p => p.ChildId, 1)
            );

            // Assert
            Assert.Contains("Søvn på dagen", component.Markup);
            Assert.Contains("Udvikling over ugen", component.Markup);
            Assert.Contains("Udvikling over måneden", component.Markup);
            Assert.Contains("Udvikling over året", component.Markup);
        }
    }
}