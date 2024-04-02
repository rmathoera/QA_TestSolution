using System;
using TestSolution.Specs.WebUI.Drivers;
using TestSolution.Web.Models;

namespace TestSolution.Specs.WebUI.Support
{
    public class AuthContext
    {
        private readonly LoginPageDriver _loginPageDriver;

        public string LoggedInUserName { get; set; }

        public bool IsLoggedIn => LoggedInUserName != null;

        public AuthContext(LoginPageDriver loginPageDriver)
        {
            _loginPageDriver = loginPageDriver;

            loginPageDriver.OnAuthenticated += (loginInput, authToken) =>
            {
                LoggedInUserName = loginInput.Name;
            };
        }

        public void Authenticate(string userName, string password)
        {
            _loginPageDriver.Perform(new LoginInputModel { Name = userName, Password = password });
        }
    }
}
