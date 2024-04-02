using System;
using TestSolution.Specs.WebUI.Drivers;
using TestSolution.Web.Models;
using Reqnroll;

namespace TestSolution.Specs.WebUI.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private readonly RegisterPageDriver _registerPageDriver;

        public RegistrationStepDefinitions(RegisterPageDriver registerPageDriver)
        {
            _registerPageDriver = registerPageDriver;
        }

        [Given("there is a user registered with user name {string} and password {string}")]
        public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
        {
            _registerPageDriver.Perform(
                new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password });
        }

        [When("the user attempts to register with user name {string} and password {string}")]
        public void WhenTheUserAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
        {
            _registerPageDriver.Perform(
                new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password },
                true);
        }

        [Then("the registration should be successful")]
        public void ThenTheRegistrationShouldBeSuccessful()
        {
            _registerPageDriver.ShouldBeSuccessful();
        }
    }
}
