using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Interfaces;
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
        #region ctor
        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        #endregion
        #region Tags

        public async Task<List<Tag>> GetAllTags()
        {
            return await _questionRepository.GetAllTags();
        }
        #endregion
    }
}
