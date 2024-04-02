using System;
using FluentAssertions;
using TestSolution.Specs.Controller.Drivers;
using TestSolution.Specs.Controller.Support;
using TestSolution.Specs.Support;
using TestSolution.Web.Models;
using Reqnroll;

namespace TestSolution.Specs.Controller.StepDefinitions
{
    [Binding]
    public class QuestionVotingStepDefinitions
    {
        private readonly VoteQuestionDriver _voteQuestionDriver;
        private readonly IsolatedAuthContextFactory _isolatedAuthContextFactory;
        private readonly QuestionContext _questionContext;
        private QuestionSummaryModel _question;

        public QuestionVotingStepDefinitions(VoteQuestionDriver voteQuestionDriver, IsolatedAuthContextFactory isolatedAuthContextFactory, QuestionContext questionContext)
        {
            _voteQuestionDriver = voteQuestionDriver;
            _isolatedAuthContextFactory = isolatedAuthContextFactory;
            _questionContext = questionContext;
        }

        [When("the user votes {word} the question")]
        public void WhenTheUserVotesUpTheQuestion(VoteDirection vote)
        {
            _question = _voteQuestionDriver.Perform(vote);
        }

        [When("the user attempts to vote {word} the question")]
        public void WhenTheUserAttemptsToVoteTheQuestion(VoteDirection vote)
        {
            _question = _voteQuestionDriver.Perform(vote, true);
        }

        [Given("another user votes {word} the question in the meanwhile")]
        public void GivenAnotherUserVotesTheQuestionInTheMeanwhile(VoteDirection vote)
        {
            var otherUserAuthContext = _isolatedAuthContextFactory.CreateAuthContext();
            otherUserAuthContext.Authenticate(DomainDefaults.AltUserName, DomainDefaults.AltUserPassword);
            var otherUserVoteDriver = _isolatedAuthContextFactory.CreateDriver<VoteQuestionDriver>(otherUserAuthContext);
            otherUserVoteDriver.Perform(_questionContext.CurrentQuestionId, vote);
        }

        [Then("the vote count of the question should be changed to {int}")]
        public void ThenTheVoteCountOfTheQuestionShouldBeChangedTo(int expectedVoteCount)
        {
            _question.Votes.Should().Be(expectedVoteCount);
        }

        [Then("the question voting attempt should fail with error {string}")]
        public void ThenTheQuestionVotingAttemptShouldFailWithError(string expectedErrorMessageKey)
        {
            _voteQuestionDriver.ShouldFailWithError(expectedErrorMessageKey);
        }
    }
}
