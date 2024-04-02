using System;
using TestSolution.Specs.Support;
using TestSolution.Web.Models;
using Reqnroll;
using Reqnroll.Assist;

namespace TestSolution.Specs.Controller.Support
{
    [Binding]
    public class Conversions
    {
        [StepArgumentTransformation]
        public AskInputModel ConvertAskInputModel(Table questionTable)
        {
            return questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
        }
    }
}
