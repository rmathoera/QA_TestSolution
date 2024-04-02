using System;
using TestSolution.Specs.WebUI.Drivers;
using TestSolution.Specs.Support;
using TestSolution.Specs.Support.Data;
using TestSolution.Web.Models;
using Reqnroll;
using Reqnroll.Assist;

namespace TestSolution.Specs.WebUI.StepDefinitions
{
    [Binding]
    public class QuestionAskingStepDefinitions
    {
        private readonly AskQuestionPageDriver _askQuestionDriver;
        private readonly QuestionDetailsPageDriver _questionDetailsPageDriver;
        private Table _askQuestionSpecification;
        private QuestionDetailModel _questionDetails;

        public QuestionAskingStepDefinitions(AskQuestionPageDriver askQuestionDriver, QuestionDetailsPageDriver questionDetailsPageDriver)
        {
            _askQuestionDriver = askQuestionDriver;
            _questionDetailsPageDriver = questionDetailsPageDriver;
        }

        [When("the user asks a question as")]
        public void WhenTheUserAsksAQuestionAs(Table questionTable)
        {
            _askQuestionSpecification = questionTable;
            var question = questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
            _askQuestionDriver.Perform(question);
            _questionDetails = _questionDetailsPageDriver.GetQuestionDetails();
        }

        [When("the user attempts to ask a question")]
        public void WhenTheUserAttemptsToAskAQuestion()
        {
            _askQuestionDriver.Perform(DomainDefaults.GetDefaultAskInput(), true);
        }

        [When("the user attempts to ask a question as")]
        public void WhenTheUserAttemptsToAskAQuestionAs(Table questionTable)
        {
            var question = questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
            _askQuestionDriver.Perform(question, true);
        }

        [Then("the question should be posted as above")]
        public void ThenTheQuestionShouldBePostedAsAbove()
        {
            _askQuestionSpecification.CompareToInstance(_questionDetails.ToQuestionData());
        }

        [Then("the question meta data should be")]
        public void ThenTheQuestionMetaDataShouldBe(Table expectedQuestionMetaDataTable)
        {
            expectedQuestionMetaDataTable.CompareToInstance(_questionDetails.ToQuestionData());
        }

        [Then("the ask attempt should fail with error {string}")]
        public void ThenTheAskAttemptShouldFailWithError(string expectedErrorMessageKey)
        {
            _askQuestionDriver.ShouldFailWithError(expectedErrorMessageKey);
        }
    }
}
