using System.Linq;
using TestSolution.Web.Models;
using TestSolution.Web.Services;
using Microsoft.AspNetCore.Mvc;
using TestSolution.Web.DataAccess;

namespace TestSolution.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DataContext _dataContext = new();
        private readonly ModelTransformationService _modelTransformationService;

        public HomeController()
        {
            _modelTransformationService = new ModelTransformationService(_dataContext);
        }

        // GET: api/home
        [HttpGet]
        public HomePageModel GetHomePageModel(string token = null)
        {
            var model = new HomePageModel();
            model.MainMessage = "Welcome to ReqOverflow!";
            model.UserName = AuthenticationServices.GetCurrentUserName(HttpContext, token);
            model.IsAdmin = AuthenticationServices.IsAdmin(HttpContext, token);

            model.LatestQuestions = _dataContext.Questions
                .OrderByDescending(q => q.AskedAt)
                .Take(10)
                .Select(q => _modelTransformationService.ToQuestionSummary(q))
                .ToList();
            
            return model;
        }
    }
}