using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Entities.Common;
using SwitchSupport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.Entities.Questions
{
    public class QuestionUserScore : BaseEntity
    {
        #region Properties

        public QustionScore Type { get; set; }

        public long UserId { get; set; }

        public long QuestionId { get; set; }
        #endregion

        #region Relations
        public User User { get; set; }

        public Question Question { get; set; }
        #endregion
    }
}
