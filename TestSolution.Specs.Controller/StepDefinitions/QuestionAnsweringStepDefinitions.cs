using System;
using TestSolution.Specs.Controller.Drivers;
using TestSolution.Web.Models;
using Reqnroll;
using Reqnroll.Assist;

namespace TestSolution.Specs.Controller.StepDefinitions
{
    [Binding]
    public class QuestionAnsweringStepDefinitions
    {
        private readonly PostAnswerDriver _postAnswer;

        public QuestionAnsweringStepDefinitions(PostAnswerDriver postAnswer)
        {
            _postAnswer = postAnswer;
        }

        [When("the user answers the question as")]
        public void WhenTheUserAnswersTheQuestionAs(Table answerTable)
        {
            var answer = answerTable.CreateInstance<AnswerInputModel>();
            _postAnswer.Perform(answer);
        }

        [When("the user attempts to answer the question")]
        public void WhenTheUserAttemptsToAnswerTheQuestion()
        {
            _postAnswer.Perform(new AnswerInputModel {Content = "Sample content"}, true);
        }

        [When("the user attempts to answer the question as")]
        public void WhenTheUserAttemptsToAnswerTheQuestionAs(Table answerTable)
        {
            var answer = answerTable.CreateInstance<AnswerInputModel>();
            _postAnswer.Perform(answer, true);
        }

        [Then("the answer attempt should fail with error {string}")]
        public void ThenTheAnswerAttemptShouldFailWithError(string expectedErrorMessageKey)
        {
            _postAnswer.ShouldFailWithError(expectedErrorMessageKey);
        }
    }
}
