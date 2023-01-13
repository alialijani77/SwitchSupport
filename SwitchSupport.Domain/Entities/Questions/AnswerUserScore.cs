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
    public class AnswerUserScore : BaseEntity
    {
        #region Properties

        public AnswerScore Type { get; set; }

        public long UserId { get; set; }

        public long AnswerId { get; set; }
        #endregion

        #region Relations
        public User User { get; set; }

        public Answer Answer { get; set; }
        #endregion
    }
}
