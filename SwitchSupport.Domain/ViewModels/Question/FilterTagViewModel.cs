using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwitchSupport.Domain.ViewModels.Question
{
    public class FilterTagViewModel : Paging<Tag>
    {
        public string? Title { get; set; }

        public FilterTagSortEnum Sort { get; set; }
    }

    public enum FilterTagSortEnum
    {
        [Display(Name = "تاریخ ثبت نزولی")] NewToOld,
        [Display(Name = "تاریخ ثبت صعودی")] OldToNew,
        [Display(Name = "تعداد استفاده صعودی")] UseCountHighToLow,
        [Display(Name = "تعداد استفاده صعودی")] UseCountLowToHigh
    }
}
