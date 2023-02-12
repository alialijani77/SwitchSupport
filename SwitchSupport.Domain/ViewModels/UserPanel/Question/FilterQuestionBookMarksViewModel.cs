using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.UserPanel.Question
{
    public class FilterQuestionBookMarksViewModel : Paging<Domain.Entities.Questions.Question>
    {
        public long userId { get; set; }
    }
}
