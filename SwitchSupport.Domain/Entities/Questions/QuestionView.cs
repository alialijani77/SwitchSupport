using SwitchSupport.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.Entities.Questions
{
    public class QuestionView : BaseEntity
    {
        #region Priperties

        public string UserIP { get; set; }

        public long QuestionId { get; set; }

        #endregion

        #region Relations

        public Question Question { get; set; }

        #endregion
    }
}
