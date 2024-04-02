using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSolution.Web.DataAccess;
using TestSolution.Web.Services;

namespace TestSolution.Tests
{
    [TestClass]
    public class ModelTransformationServiceTests
    {
        private static readonly DataContext _dataContext = new DataContext(new DataContext.InMemoryPersist());

        private static readonly Question BaseQuestion = new Question
        {
            Id = Guid.NewGuid(),
            Title = "title1",
            Body = "body1",
            AskedAt = DateTime.Now,
            Views = 2,
            Votes = 3,
        };

        private static readonly User User = new User { Name = "XY", Password = "1234" };
        private static readonly Tag Tag1 = new Tag { Label = "tag1" };
        private static readonly Tag Tag2 = new Tag { Label = "tag2" };
        private static readonly Answer Answer1 = new Answer { Content = "Content1", Votes = 1 };
        private static readonly Answer Answer2 = new Answer { Content = "Content2", Votes = 3 };

        private readonly ModelTransformationService _sut = new ModelTransformationService(_dataContext);

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _dataContext.Tags.AddRange(new[] { Tag1, Tag2 });
            _dataContext.Users.Add(User);
        }

        [TestMethod]
        public void ToQuestionDetails_should_keep_simple_values()
        {
            var question = BaseQuestion;

            var result = _sut.ToQuestionDetails(question);

            Assert.AreEqual(question.Id, result.Id);
            Assert.AreEqual(question.Title, result.Title);
            Assert.AreEqual(question.Body, result.Body);
            Assert.AreEqual(question.AskedAt, result.AskedAt);
            Assert.AreEqual(question.Views, result.Views);
            Assert.AreEqual(question.Votes, result.Votes);
        }

        [TestMethod]
        public void ToQuestionDetails_should_get_tag_labels()
        {
            var question = BaseQuestion;
            question.TagIds = new List<Guid>() { Tag1.Id, Tag2.Id };

            var result = _sut.ToQuestionDetails(question);

            CollectionAssert.AreEqual(new[] { Tag1.Label, Tag2.Label }, result.Tags, StringComparer.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void ToQuestionDetails_should_get_user_reference()
        {
            var question = BaseQuestion;
            question.AskedBy = User.Id;

            var result = _sut.ToQuestionDetails(question);

            Assert.AreEqual(User.Id, result.AskedBy.Id);
            Assert.AreEqual(User.Name, result.AskedBy.Name);
        }

        [TestMethod]
        public void ToQuestionDetails_should_get_answers_by_vote_descending()
        {
            var question = BaseQuestion;
            question.Answers.AddRange(new[] { Answer1, Answer2 });

            var result = _sut.ToQuestionDetails(question);

            Assert.AreEqual(2, result.Answers.Count);
            Assert.AreEqual(Answer2.Id, result.Answers[0].Id);
            Assert.AreEqual(Answer1.Id, result.Answers[1].Id);
            Assert.AreEqual(Answer2.Content, result.Answers[0].Content);
            Assert.AreEqual(Answer2.Votes, result.Answers[0].Votes);
        }

        [TestMethod]
        public void ToQuestionSummary_should_keep_simple_values()
        {
            var question = BaseQuestion;

            var result = _sut.ToQuestionSummary(question);

            Assert.AreEqual(question.Id, result.Id);
            Assert.AreEqual(question.Title, result.Title);
            Assert.AreEqual(question.AskedAt, result.AskedAt);
            Assert.AreEqual(question.Views, result.Views);
            Assert.AreEqual(question.Votes, result.Votes);
        }

        [TestMethod]
        public void ToQuestionSummary_should_get_user_reference()
        {
            var question = BaseQuestion;
            question.AskedBy = User.Id;

            var result = _sut.ToQuestionSummary(question);

            Assert.AreEqual(User.Id, result.AskedBy.Id);
            Assert.AreEqual(User.Name, result.AskedBy.Name);
        }

        [TestMethod]
        public void ToQuestionSummary_should_get_answers_count()
        {
            var question = BaseQuestion;
            question.Answers.AddRange(new[] { Answer1, Answer2 });

            var result = _sut.ToQuestionSummary(question);

            Assert.AreEqual(2, result.Answers);
        }

        [TestMethod]
        public void ToAnswerDetails_should_keep_simple_values()
        {
            var answer = Answer1;

            var result = _sut.ToAnswerDetails(answer);

            Assert.AreEqual(answer.Id, result.Id);
            Assert.AreEqual(answer.Content, result.Content);
            Assert.AreEqual(answer.AnsweredAt, result.AnsweredAt);
            Assert.AreEqual(answer.Votes, result.Votes);
        }

        [TestMethod]
        public void ToAnswerDetails_should_get_user_reference()
        {
            var answer = Answer1;
            answer.AnsweredBy = User.Id;

            var result = _sut.ToAnswerDetails(answer);

            Assert.AreEqual(User.Id, result.AnsweredBy.Id);
            Assert.AreEqual(User.Name, result.AnsweredBy.Name);
        }

        [TestMethod]
        public void ToUserReference_should_get_user_id_and_name()
        {
            var user = User;

            var result = _sut.ToUserReference(user);

            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Name, result.Name);
        }

        [TestMethod]
        public void ToUserReference_should_create_unknown_user_placeholder_for_null()
        {
            var result = _sut.ToUserReference(null);

            Assert.AreEqual(Guid.Empty, result.Id);
            Assert.AreEqual("???", result.Name);
        }
    }
}
