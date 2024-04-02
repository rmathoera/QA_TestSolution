using System;
using TestSolution.Specs.Support;
using TestSolution.Web.Controllers;
using TestSolution.Web.Models;

namespace TestSolution.Specs.Controller.Drivers
{
    public class UserRegistrationDriver : ActionAttempt<RegisterInputModel, UserReferenceModel>
    {
        public UserRegistrationDriver() : base(registerInput =>
        {
            var controller = new UserController();
            return controller.Register(registerInput);
        })
        {
        }
    }
}
