using System;
using TestSolution.Specs.API.Support;
using TestSolution.Specs.Support;
using TestSolution.Web.Models;

namespace TestSolution.Specs.API.Drivers
{
    public class UserApiDriver
    {
        public ActionAttempt<RegisterInputModel, UserReferenceModel> Register { get; }

        public UserApiDriver(WebApiContext webApiContext, ActionAttemptFactory actionAttemptFactory)
        {
            Register = actionAttemptFactory.CreateWithStatusCheck<RegisterInputModel, UserReferenceModel>(
                nameof(Register),
                registerInput => webApiContext.ExecutePost<UserReferenceModel>("/api/user", registerInput));
        }
    }
}
