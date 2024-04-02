using System;
using TestSolution.Specs.API.Support;
using TestSolution.Web.Models;

namespace TestSolution.Specs.API.Drivers
{
    public class HomeApiDriver
    {
        private readonly WebApiContext _webApiContext;

        public HomeApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public HomePageModel GetHomePageModel()
        {
            return _webApiContext.ExecuteGet<HomePageModel>("/api/home");
        }
    }
}
