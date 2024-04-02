using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Microsoft.AspNetCore.Mvc;
using TestSolution.Web.DataAccess;
using TestSolution.Web.Models;
using TestSolution.Web.Services;

namespace TestSolution.Web.Controllers
{
    [Route("api/qs")]
    [ApiController]
    public class QuestionSuggestionsController : ControllerBase
    {
        private readonly DataContext _dataContext = new();
        private readonly ModelTransformationService _modelTransformationService;
        private readonly TextAnalysisService _textAnalysisService;

        public QuestionSuggestionsController()
        {
            _modelTransformationService = new ModelTransformationService(_dataContext);
            _textAnalysisService = new TextAnalysisService();
        }

        // POST: api/qs/ -- get suggestions of existing questions
        [HttpPost]
        public List<QuestionSummaryModel> GetQuestionSuggestions([FromBody] AskInputModel askInputModel)
        {
            var texts = new[] { askInputModel.Title, askInputModel.Body }
                .Concat(askInputModel.Tags ?? new string[0]);

            var words = _textAnalysisService.GetRelevantWords(texts);

            if (!words.Any())
                return new List<QuestionSummaryModel>();

            IEnumerable<string> GetQuestionTexts(Question q)
                => new[] { q.Title, q.Body }
                    .Concat(_dataContext.GetTagsByIds(q.TagIds).Select(t => t.Label));

            var relatedQuestions = _dataContext.Questions
                .Select(q => new
                {
                    Question = q,
                    Index = _textAnalysisService.GetSimilarityIndex(GetQuestionTexts(q), words)
                })
                .Where(rq => rq.Index >= 0.5)
                .OrderByDescending(rq => rq.Index)
                .ToArray();
            
            return relatedQuestions
                .Select(rq => _modelTransformationService.ToQuestionSummary(rq.Question))
                .ToList();
        }
    }
}
