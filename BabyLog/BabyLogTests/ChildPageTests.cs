using Bunit;
using BabyLog.Client.Pages;
using BabyLog.Client.Pages.Children;
using BabyLog.Client.Services;
using BabyLog.Client.ViewModels;
using BabyLogTests.Fakes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BabyLogTests.BUnitTests
{
    public class ChildPageTests : TestContext
    {
        /*
         Dashboard -> Children

         Test:
         Verify that the "Åbn børn" button exists
         and navigates correctly to /children
        */
        [Fact]
        public void Dashboard_Should_Navigate_To_Children_Page()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<ChildViewModel, FakeChildViewModel>();

            var navigationManager = Services.GetRequiredService<NavigationManager>();

            // Act
            var component = Render<Dashboard>();

            var button = component.Find("button");
            button.Click();

            // Assert
            Assert.Contains("/children", navigationManager.Uri);
        }

        /*
         Children -> Create

         Test:
         Verify that the "Opret barn" button
         navigates correctly to /children/create
        */
        [Fact]
        public void Children_Should_Navigate_To_Create_Page()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<ChildViewModel, FakeChildViewModel>();

            var navigationManager = Services.GetRequiredService<NavigationManager>();

            // Act
            var component = Render<Children>();

            var button = component.Find(".btn-primary");
            button.Click();

            // Assert
            Assert.Contains("/children/create", navigationManager.Uri);
        }

        /*
         Delete page should show confirmation text

         Test:
         Verify that delete page shows:
         "Er du sikker?"
        */
        [Fact]
        public void Delete_Page_Should_Show_Confirmation_Text()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<ChildViewModel, FakeChildViewModel>();

            // Act
            var component = Render<DeleteChild>(
                parameters => parameters.Add(p => p.ChildId, 1)
            );

            // Assert
            Assert.Contains("Er du sikker?", component.Markup);
            Assert.Contains("Augusta", component.Markup);
        }

        /*
         Details page should show child information

         Test:
         Verify that child details are displayed:
         - FirstName
         - BirthDate
         - Gender
        */
        [Fact]
        public void Details_Page_Should_Show_Child_Information()
        {
            // Arrange
            Services.AddSingleton<TokenStorageService>();
            Services.AddSingleton<ChildViewModel, FakeChildViewModel>();

            // Act
            var component = Render<ChildDetails>(
                parameters => parameters.Add(p => p.ChildId, 1)
            );

            // Assert
            Assert.Contains("Augusta", component.Markup);
            Assert.Contains("Fødselsdato", component.Markup);
            Assert.Contains("Køn", component.Markup);
        }
    }
}