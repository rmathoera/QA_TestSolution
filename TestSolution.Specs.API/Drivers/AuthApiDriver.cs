using System;
using System.Net;
using FluentAssertions;
using TestSolution.Specs.API.Support;
using TestSolution.Web.Models;
using TestSolution.Specs.Support;
using TestSolution.Web.Utils;

namespace TestSolution.Specs.API.Drivers
{
    public class AuthApiDriver
    {
        public class LoginDriver : ActionAttempt<LoginInputModel, string>
        {
            private readonly WebApiContext _webApiContext;

            public LoginDriver(WebApiContext webApiContext)
            {
                _webApiContext = webApiContext;
            }

            public event Action<LoginInputModel, string> OnAuthenticated;

            protected override string DoAction(LoginInputModel loginInput)
            {
                var response = _webApiContext.ExecutePost<string>("api/auth", loginInput);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var authToken = response.ResponseData;
                OnAuthenticated?.Invoke(loginInput, authToken);
                return authToken;
            }
        }

        private readonly WebApiContext _webApiContext;

        public LoginDriver Login { get; }

        public AuthApiDriver(WebApiContext webApiContext, LoginDriver login)
        {
            _webApiContext = webApiContext;
            Login = login;
        }

        public UserReferenceModel GetCurrentUser()
        {
            try
            {
                return _webApiContext.ExecuteGet<UserReferenceModel>("/api/auth");
            }
            catch (HttpResponseException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
