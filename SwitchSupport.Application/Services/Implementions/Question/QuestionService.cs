using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Common;
using SwitchSupport.Domain.ViewModels.Question;
using SwitchSupport.Application.Extensions;
using System.Data;


namespace SwitchSupport.Application.Services.Implementions.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ScoreManagementViewModel _socerManagment;
        private readonly IUserService _userService;
        #region ctor
        public QuestionService(IQuestionRepository questionRepository,IOptions<ScoreManagementViewModel> socerManagment, IUserService userService)
        {
            _questionRepository = questionRepository;
            _socerManagment = socerManagment.Value;
            _userService = userService;
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


        public async Task<List<string>> GetTagsByQuestionId(long questionId)
        {
            return await _questionRepository.GetTagsByQuestionId(questionId);
        }
        #endregion

        #region Question

        public async Task<bool> AddQuestion(CreateQuestionViewModel createQuestion)
        {
            var qu = new SwitchSupport.Domain.Entities.Questions.Question()
            {
                Content = createQuestion.Description,
                Title = createQuestion.Title,
                UserId = createQuestion.UserId
            };
            await _questionRepository.AddQuestion(qu);
            await _questionRepository.SaveChanges();

            if(createQuestion.SelectTags != null && createQuestion.SelectTags.Any())
            {
                foreach (var item in createQuestion.SelectTags)
                {
                    var tag = await _questionRepository.GetTagByName(item.Trim().ToLower());
                    if (tag == null) continue;
                    tag.UseCount += 1;
                    await _questionRepository.UpdateTag(tag);
                    var selectTagQuestion = new SelectQuestionTag()
                    {
                        QuestionId = qu.Id,
                        TagId = tag.Id
                     };

                    await _questionRepository.AddQuestionTag(selectTagQuestion);                   
                }
                await _questionRepository.SaveChanges();
            }
            await _userService.UpdateUserScoreAndMedal(createQuestion.UserId, _socerManagment.AddNewQuestionScore);
           
            return true;
        }
        
        public async Task<FilterQuestionViewModel> GetAllQuestions(FilterQuestionViewModel filter)
        {
            var query = await  _questionRepository.GetAllQuestions();

            if(!string.IsNullOrEmpty(filter.TagTitle))
            {
                query = query.Include(t => t.SelectQuestionTags).ThenInclude(t => t.Tag)
                    .Where(s => s.SelectQuestionTags.Any(s => s.Tag.Title.Equals(filter.TagTitle)));
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(q => q.Title.Contains(filter.Title.Trim().ToLower()));
            }

            switch (filter.Sort)
            {
                case FilterQuestionSortEnum.NewToOld:
                    query = query.OrderByDescending(q => q.CreateDate);
                    break;
                case FilterQuestionSortEnum.OldToNew:
                    query = query.OrderBy(q => q.CreateDate);
                    break;
                case FilterQuestionSortEnum.ScoreHighToLow:
                    query = query.OrderByDescending(q => q.Score);
                    break;
                case FilterQuestionSortEnum.ScoreLowToHigh:
                    query = query.OrderBy(q => q.Score);
                    break;               
            }
            var result = query.Include(s => s.Answers).Include(s=>s.SelectQuestionTags).ThenInclude(s=>s.Tag)
                .Include(s=>s.User)
                .Select(q => new QuestionListViewModel()
                {
                    QuestionId = q.Id,
                    Title = q.Title,
                    Score = q.Score,
                    HasAnyTrueAnswer = q.Answers.Any(a => !a.IsDelete && a.IsTrue),
                    HasAnyAnswer = q.Answers.Any(a => !a.IsDelete),
                    Tags = q.SelectQuestionTags.Select(t => t.Tag.Title).ToList(),
                    UserDisplayName = q.User.GetUserDisplayName(),
                    ViewCount = q.ViewCount,
                    AnswersCount = q.Answers.Count(),
                    CreateDate = q.CreateDate.TimeAgo()
                   // AnswerByDisplayName = q.Answers.Where(a => !a.IsDelete).OrderByDescending(a => a.CreateDate).First().User.GetUserDisplayName(),
                    //AnswerByCreateDate = q.Answers.Any(a => !a.IsDelete) ? q.Answers.OrderByDescending(a => a.CreateDate).First().CreateDate.TimeAgo() : null
                }).AsQueryable();
            
            await filter.SetPaging(result);

            return filter;
        }
        #endregion

        #region FilterTag

        public async Task<FilterTagViewModel> GetAllFilterTags(FilterTagViewModel filter)
        {
            var query = await _questionRepository.GetAllFilterTags();

            if(!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(t=>t.Title.Contains(filter.Title.ToLower().Trim()));
            }

            switch (filter.Sort)
            {
                case FilterTagSortEnum.NewToOld:
                    query = query.OrderByDescending(t => t.CreateDate);
                    break;
                case FilterTagSortEnum.OldToNew:
                    query = query.OrderBy(t => t.CreateDate);
                    break;
                case FilterTagSortEnum.UseCountHighToLow:
                    query = query.OrderByDescending(t => t.UseCount);
                    break;
                case FilterTagSortEnum.UseCountLowToHigh:
                    query = query.OrderBy(t => t.UseCount);
                    break;
            }

            await filter.SetPaging(query);

            return filter;
        }

        public async Task<Domain.Entities.Questions.Question?> GetQuestionById(long questionId)
        {
            return await _questionRepository.GetQuestionById(questionId);
        }

        #endregion

        #region Answer

        public async Task<bool> AnswerQuestion(AnswerQuestionViewModel answerQuestion)
        {
            var question = _questionRepository.GetQuestionById(answerQuestion.QuestionId);

            if(question == null)
            {
                return false;
            }
            Answer answer = new Answer()
            {
                Content = answerQuestion.Answer,
                QuestionId = answerQuestion.QuestionId,
                UserId = answerQuestion.UserId
            };

            await _questionRepository.AnswerQuestion(answer);
            await _questionRepository.SaveChanges();
            return true;
        }

        public async Task<List<Answer>> GetQuestionAnswerList(long questionId)
        {
            return await _questionRepository.GetQuestionAnswerList(questionId);
        }

        #endregion
    }
}
