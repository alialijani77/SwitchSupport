﻿using SwitchSupport.Domain.Entities.Common;
using SwitchSupport.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwitchSupport.Domain.Entities.Tags
{
    public class Tag : BaseEntity
    {
        #region Properties

        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        public int UseCount { get; set; }

        #endregion

        #region Relations

        public ICollection<SelectQuestionTag> SelectQuestionTags { get; set; }

        #endregion
    }
}
