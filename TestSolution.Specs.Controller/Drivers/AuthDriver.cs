using System;
using System.Net;
using TestSolution.Specs.Controller.Support;
using TestSolution.Web.Controllers;
using TestSolution.Web.Models;
using TestSolution.Web.Utils;

namespace TestSolution.Specs.Controller.Drivers
{
    public class AuthDriver
    {
        private readonly AuthContext _authContext;
        
        public LoginDriver Login { get; }

        public AuthDriver(LoginDriver login, AuthContext authContext)
        {
            Login = login;
            _authContext = authContext;
        }

        public UserReferenceModel GetCurrentUser()
        {
            var controller = new AuthController();
            try
            {
                return controller.GetCurrentUser(_authContext.AuthToken);
            }
            catch (HttpResponseException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
