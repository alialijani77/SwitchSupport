using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Question
{
    public class AnswerQuestionViewModel
    {
        public long QuestionId { get; set; }

        public long UserId { get; set; }

        public string? Answer { get; set; }
    }
}
