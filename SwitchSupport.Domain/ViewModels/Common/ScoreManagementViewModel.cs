using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Common
{
    public class ScoreManagementViewModel
    {
        public int MinRequestCountForVerifyTag { get; set; }

        public int AddNewQuestionScore { get; set; }

        public int MinScoreForBronzeMedal { get; set; }

        public int MinScoreForSilverMedal { get; set; }

        public int MinScoreForGoldMedal { get; set; }

    }
}
