using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Question
{
    public class FilterQuestionViewModel : Paging<SwitchSupport.Domain.Entities.Questions.Question>
    {
        public string? Title { get; set; }

        public FilterQuestionSortEnum Sort { get; set; }
    }

    public enum FilterQuestionSortEnum
    {
        [Display(Name = "تاریخ ثبت نزولی")] NewToOld,
        [Display(Name = "تاریخ ثبت صعودی")] OldToNew,
        [Display(Name = "امتیاز صعودی")] ScoreHighToLow,
        [Display(Name = "امتیاز صعودی")] ScoreLowToHigh
    }
}
