using System;
using Microsoft.Extensions.Configuration;

namespace TestSolution.Web.Utils
{
    public static class ConfigurationExtensions
    {
        public static bool AllowVotingForYourItems(this IConfiguration configuration)
            => configuration?.GetValue<bool>("AppSettings:AllowVotingForYourItems") ?? false;
    }
}
