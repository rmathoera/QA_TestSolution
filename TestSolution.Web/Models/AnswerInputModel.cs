using System;

namespace TestSolution.Web.Models
{
    public class AnswerInputModel
    {
        public string Content { get; set; }

        public override string ToString()
            => $"Content: {Content}";
    }
}
