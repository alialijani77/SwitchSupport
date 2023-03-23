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
    public class FilterQuestionViewModel : Paging<QuestionListViewModel>
    {
        public FilterQuestionViewModel()
        {
            CheckedStatus = FilterCheckedStatusEnum.All;
            Sort = FilterQuestionSortEnum.NewToOld;
        }
        public string? Title { get; set; }

        public string? TagTitle { get; set; }

        public FilterQuestionSortEnum Sort { get; set; }

        public FilterCheckedStatusEnum CheckedStatus { get; set; }

    }

    public enum FilterQuestionSortEnum
    {
        [Display(Name = "تاریخ ثبت نزولی")] NewToOld,
        [Display(Name = "تاریخ ثبت صعودی")] OldToNew,
        [Display(Name = "امتیاز صعودی")] ScoreHighToLow,
        [Display(Name = "امتیاز صعودی")] ScoreLowToHigh
    }

    public enum FilterCheckedStatusEnum
    {
        [Display(Name = "همه")] All,
        [Display(Name = "بررسی شده")] IsChecked,
        [Display(Name = "بررسی نشده")] NotChecked
    }
}
