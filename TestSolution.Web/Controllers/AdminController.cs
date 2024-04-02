using System;
using Microsoft.AspNetCore.Mvc;

namespace TestSolution.Web.Controllers
{
    /// <summary>
    /// Processes admin requests (exposed on Admin page)
    /// </summary>
    [Route("api/admin/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
    }
}
