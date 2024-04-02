using System;
using System.Collections.Generic;
using TestSolution.Web.Models;
using Reqnroll;

// ReSharper disable once CheckNamespace
namespace TestSolution.Specs.Support
{
    public class QuestionContext
    {
        public Table QuestionSpecification { get; set; }
        public Table AnswerSpecification { get; set; }

        public List<QuestionDetailModel> QuestionsCreated { get; } = new();

        public QuestionDetailModel CurrentQuestion { get; set; }
        public AnswerDetailModel CurrentAnswer { get; set; }

        public Guid CurrentQuestionId => CurrentQuestion.Id;
        public Guid CurrentAnswerId => CurrentAnswer.Id;
    }
}
