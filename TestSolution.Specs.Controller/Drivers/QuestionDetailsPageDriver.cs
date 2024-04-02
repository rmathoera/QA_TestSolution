using System;
using System.Linq;
using FluentAssertions;
using TestSolution.Specs.Support;
using TestSolution.Web.Controllers;
using TestSolution.Web.Models;

namespace TestSolution.Specs.Controller.Drivers
{
    public class QuestionDetailsPageDriver
    {
        private readonly QuestionContext _questionContext;

        public QuestionDetailsPageDriver(QuestionContext questionContext)
        {
            _questionContext = questionContext;
        }

        public QuestionDetailModel PageContent { get; set; }

        public void LoadPage(Guid? questionId = null)
        {
            var controller = new QuestionController();
            PageContent = controller.GetQuestionDetails(questionId ?? _questionContext.CurrentQuestionId);
        }

        public AnswerDetailModel GetAnswerByContentFromPageContent(string content)
        {
            var answer = PageContent.Answers
                .FirstOrDefault(a => a.Content == content);
            answer.Should().NotBeNull();
            return answer;
        }

        public AnswerDetailModel GetAnswerByIdFromPageContent(Guid? answerId = null)
        {
            var id = answerId ?? _questionContext.CurrentAnswerId;
            var answer = PageContent.Answers
                .FirstOrDefault(a => a.Id == id);
            answer.Should().NotBeNull();
            return answer;
        }
    }
}
