using System;
using TestSolution.Specs.API.Drivers;
using TestSolution.Web.Models;
using Reqnroll;

namespace TestSolution.Specs.API.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private readonly UserApiDriver _userApiDriver;

        public RegistrationStepDefinitions(UserApiDriver userApiDriver)
        {
            _userApiDriver = userApiDriver;
        }

        [Given("there is a user registered with user name {string} and password {string}")]
        public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
        {
            _userApiDriver.Register.Perform(
                new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password });
        }

        [When("the user attempts to register with user name {string} and password {string}")]
        public void WhenTheUserAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
        {
            _userApiDriver.Register.Perform(
                new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password },
                true);
        }

        [Then("the registration should be successful")]
        public void ThenTheRegistrationShouldBeSuccessful()
        {
            _userApiDriver.Register.ShouldBeSuccessful();
        }
    }
}
