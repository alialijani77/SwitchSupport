using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwitchSupport.Domain.Entities.Location
{
    public class State : BaseEntity
    {
        #region Properties
        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        public long? ParentId { get; set; }
        #endregion

        #region Relations
        public State? Parent { get; set; }

        [InverseProperty("Country")]
        public ICollection<User> UserCountries { get; set; }
        [InverseProperty("City")]
        public ICollection<User> UserCities { get; set; }
        #endregion
    }
}
