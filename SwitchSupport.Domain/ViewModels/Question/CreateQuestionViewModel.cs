using SwitchSupport.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwitchSupport.Domain.ViewModels.Question
{
    public class CreateQuestionViewModel
    {
        [Display(Name = "عنوان")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public List<string>? SelectTags { get; set; }

        public string? SelectTagsJson { get; set; }

        public long UserId { get; set; }
    }

    public class CreateQuestionResult
    {
        public CreateQuestionEnum Status { get; set; }

        public string Message { get; set; }
    }

    public enum CreateQuestionEnum
    {
        Success,
        NotValidTag
    }
}
