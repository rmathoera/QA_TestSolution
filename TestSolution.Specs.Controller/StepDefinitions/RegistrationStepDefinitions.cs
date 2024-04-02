using System;
using TestSolution.Specs.Controller.Drivers;
using TestSolution.Web.Models;
using Reqnroll;

namespace TestSolution.Specs.Controller.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private readonly UserRegistrationDriver _userRegistrationDriver;

        public RegistrationStepDefinitions(UserRegistrationDriver userRegistrationDriver)
        {
            _userRegistrationDriver = userRegistrationDriver;
        }

        [Given("there is a user registered with user name {string} and password {string}")]
        public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
        {
            _userRegistrationDriver.Perform(new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password });
        }

        [When("the user attempts to register with user name {string} and password {string}")]
        public void WhenTheUserAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
        {
            _userRegistrationDriver.Perform(
                new RegisterInputModel {UserName = userName, Password = password, PasswordReEnter = password}, 
                true);
        }

        [Then("the registration should be successful")]
        public void ThenTheRegistrationShouldBeSuccessful()
        {
            _userRegistrationDriver.ShouldBeSuccessful();
        }
    }
}
