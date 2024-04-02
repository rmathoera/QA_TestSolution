using System;
using TestSolution.Specs.Controller.Support;
using TestSolution.Specs.Support;
using TestSolution.Web.Controllers;
using TestSolution.Web.Models;

namespace TestSolution.Specs.Controller.Drivers
{
    public class PostAnswerDriver : ActionAttempt<AnswerInputModel, AnswerDetailModel>
    {
        public PostAnswerDriver(QuestionContext questionContext, AuthContext authContext, QuestionDetailsPageDriver questionDetailsPageDriver) : base(answerInput =>
        {
            var controller = new QuestionController();
            var result = questionContext.CurrentAnswer = controller
                .PostAnswer(questionContext.CurrentQuestionId, answerInput, authContext.AuthToken);
            questionDetailsPageDriver.LoadPage(questionContext.CurrentQuestionId);
            return result;
        })
        {
        }
    }
}
