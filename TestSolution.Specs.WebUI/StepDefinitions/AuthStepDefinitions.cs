using System;
using FluentAssertions;
using TestSolution.Specs.WebUI.Drivers;
using TestSolution.Specs.WebUI.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSolution.Specs.Support;
using TestSolution.Web.Models;
using Reqnroll;

namespace TestSolution.Specs.WebUI.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly LoginPageDriver _loginPageDriver;
        private readonly HomePageDriver _homePageDriver;
        private readonly AuthContext _authContext;

        public AuthStepDefinitions(AuthContext authContext, LoginPageDriver loginPageDriver, HomePageDriver homePageDriver)
        {
            _authContext = authContext;
            _loginPageDriver = loginPageDriver;
            _homePageDriver = homePageDriver;
        }

        [Given("user Marvin is authenticated")]
        [Given("the user is authenticated")]
        public void GivenTheUserIsAuthenticated()
        {
            _authContext.Authenticate(DomainDefaults.UserName, DomainDefaults.UserPassword);
        }

        [Given("the user is not authenticated")]
        public void GivenTheUserIsNotAuthenticated()
        {
            _homePageDriver.GetCurrentUser().Should().BeNull();
        }

        [When("the user attempts to log in with user name {string} and password {string}")]
        public void WhenTheUserAttemptsToLogInWithUserNameAndPassword(string userName, string password)
        {
            _loginPageDriver.Perform(
                new LoginInputModel {Name = userName, Password = password}, true);
        }

        [Then("the login attempt should be successful")]
        public void ThenTheLoginAttemptShouldBeSuccessful()
        {
            _loginPageDriver.ShouldBeSuccessful();
        }

        [Then("the user should be authenticated")]
        public void ThenTheUserShouldBeAuthenticated()
        {
            var currentUser = _homePageDriver.GetCurrentUser();
            currentUser.Should().NotBeNull();
            currentUser.Name.Should().Be(_loginPageDriver.LastInput.Name);
        }
    }
}
