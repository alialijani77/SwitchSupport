using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Services.Implementions.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptions<ScoreManagementViewModel> _socerManagment;
        #region ctor
        public QuestionService(IQuestionRepository questionRepository,IOptions<ScoreManagementViewModel> socerManagment)
        {
            _questionRepository = questionRepository;
            _socerManagment = socerManagment;
        }

        #endregion
        #region Tags

        public async Task<List<Tag>> GetAllTags()
        {
            var l = _socerManagment.Value;
            return await _questionRepository.GetAllTags();
        }
        #endregion
    }
}
