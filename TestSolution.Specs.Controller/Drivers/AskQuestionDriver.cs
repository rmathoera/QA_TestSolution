using System;
using TestSolution.Specs.Controller.Support;
using TestSolution.Specs.Support;
using TestSolution.Web.Controllers;
using TestSolution.Web.Models;

namespace TestSolution.Specs.Controller.Drivers
{
    public class AskQuestionDriver : ActionAttempt<AskInputModel, QuestionSummaryModel>
    {
        public AskQuestionDriver(AuthContext authContext) : base(askInput =>
        {
            var controller = new QuestionController();
            return controller.AskQuestion(askInput, authContext.AuthToken);
        })
        {
        }
    }
}
