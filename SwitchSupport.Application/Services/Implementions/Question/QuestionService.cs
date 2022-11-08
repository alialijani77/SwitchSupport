using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Common;
using SwitchSupport.Domain.ViewModels.Question;
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
        private readonly ScoreManagementViewModel _socerManagment;
        #region ctor
        public QuestionService(IQuestionRepository questionRepository,IOptions<ScoreManagementViewModel> socerManagment)
        {
            _questionRepository = questionRepository;
            _socerManagment = socerManagment.Value;
        }

        #endregion
        #region Tags

        public async Task<List<Tag>> GetAllTags()
        {           
            return await _questionRepository.GetAllTags();
        }

        public async Task<CreateQuestionResult> CheckTagValidation(long userId, List<string>? tags)
        {
            if(tags != null && tags.Any())
            {
                foreach (var item in tags)
                {
                   var isExsistTag = await _questionRepository.IsExistsTagByName(item.Trim().ToLower());
                   if(isExsistTag) continue;

                    var userRequestTag = await _questionRepository.CheckUserRequestTag(userId, item);
                    if (userRequestTag)
                    {
                        CreateQuestionResult res2 = new CreateQuestionResult();
                        res2.Status = CreateQuestionEnum.NotValidTag;
                        res2.Message = $"تگ {item} باید حداقل توسط {_socerManagment.MinRequestCountForVerifyTag} تایید شود.";
                        return res2;
                    }

                    var tagRequest = new RequestTag();
                    tagRequest.UserId = userId;
                    tagRequest.Title = item;
                    await _questionRepository.AddRequestTag(tagRequest);
                    await _questionRepository.SaveChanges();

                    var countTag = await _questionRepository.GetCountRequestTag(item.ToLower().Trim());
                    if (countTag < _socerManagment.MinRequestCountForVerifyTag)
                    {
                        CreateQuestionResult res3 = new CreateQuestionResult();
                        res3.Status = CreateQuestionEnum.NotValidTag;
                        res3.Message = $"تگ {item} باید حداقل توسط {_socerManagment.MinRequestCountForVerifyTag} تایید شود.";
                        return res3;
                    }

                    Tag tag = new Tag();
                    tag.Title = item;

                    await _questionRepository.AddTag(tag);
                    await _questionRepository.SaveChanges();
                }
                CreateQuestionResult res4 = new CreateQuestionResult();
                res4.Status = CreateQuestionEnum.Success;
                res4.Message = "تگ معتبر می باشد";
                return res4;
            }

            CreateQuestionResult res = new CreateQuestionResult();
            res.Status = CreateQuestionEnum.NotValidTag;
            res.Message = "انتخاب تگ الزامی می باشد";
            return res;
        }
        #endregion
    }
}
