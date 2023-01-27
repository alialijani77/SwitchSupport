using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Enums;
using SwitchSupport.Domain.ViewModels.Question;

namespace SwitchSupport.Web.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;

        #region ctor
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        #endregion

        #region Question
        [Authorize]
        [HttpGet("create-question")]
        public async Task<IActionResult> CreateQuestion()
        {
            return View();
        }


        [HttpPost("create-question"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(CreateQuestionViewModel createQuestion)
        {
            var tagvalidation = await _questionService.CheckTagValidation(HttpContext.User.GetUserId(), createQuestion.SelectTags);

            if (tagvalidation.Status == CreateQuestionEnum.NotValidTag)
            {
                createQuestion.SelectTagsJson = JsonConvert.SerializeObject(createQuestion.SelectTags);
                createQuestion.SelectTags = null;
                TempData[ErrorMessage] = tagvalidation.Message;
                return View(createQuestion);
            }
            createQuestion.UserId = HttpContext.User.GetUserId();
            var result = await _questionService.AddQuestion(createQuestion);
            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                return Redirect("/");
            }

            createQuestion.SelectTagsJson = JsonConvert.SerializeObject(createQuestion.SelectTags);
            createQuestion.SelectTags = null;
            return View(createQuestion);
        }

        [HttpPost("ScoreUpForQuestion")]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> ScoreUpForQuestion(long questionId)
        {
            var result = await _questionService.CreateScoreForQuestion(questionId, QustionScore.Plus, User.GetUserId());

            switch (result)
            {
                case QuestionScoreResult.error:
                    return new JsonResult(new { status = "error" });
                    break;
                case QuestionScoreResult.MinScoreForUpScoreQuestion:
                    return new JsonResult(new { status = "MinScoreForUpScoreQuestion" });
                    break;
                case QuestionScoreResult.MinScoreForDownScoreQuestion:
                    return new JsonResult(new { status = "MinScoreForDownScoreQuestion" });
                    break;
                case QuestionScoreResult.IsExistsUserScoreForQuestion:
                    return new JsonResult(new { status = "IsExistsUserScoreForQuestion" });
                    break;
                case QuestionScoreResult.success:
                    return new JsonResult(new { status = "success" });
                    break;
                default:
                    return NotFound();
            }
        }

        [HttpPost("ScoreDownForQuestion")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ScoreDownForQuestion(long questionId)
        {
            var result = await _questionService.CreateScoreForQuestion(questionId, QustionScore.Minus, User.GetUserId());

            switch (result)
            {
                case QuestionScoreResult.error:
                    return new JsonResult(new { status = "error" });
                    break;
                case QuestionScoreResult.MinScoreForUpScoreQuestion:
                    return new JsonResult(new { status = "MinScoreForUpScoreQuestion" });
                    break;
                case QuestionScoreResult.MinScoreForDownScoreQuestion:
                    return new JsonResult(new { status = "MinScoreForDownScoreQuestion" });
                    break;
                case QuestionScoreResult.IsExistsUserScoreForQuestion:
                    return new JsonResult(new { status = "IsExistsUserScoreForQuestion" });
                    break;
                case QuestionScoreResult.success:
                    return new JsonResult(new { status = "success" });
                    break;
                default:
                    return NotFound();
            }
        }

        [HttpGet("editquestion/{questionId}")]
        [Authorize]
        public async Task<IActionResult> EditQuestion(long questionId)
        {
            var res = await _questionService.FillEditQuestionViewModel(questionId,User.GetUserId());
            if (res == null) return NotFound();
            return View(res);
        }

        [HttpPost("editquestion/{questionId}"),ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditQuestion(EditQuestionViewModel editQuestion)
        {
            return View();
        }

        #endregion

        #region Tags
        [HttpGet("get-tags")]
        public async Task<IActionResult> GetTags(string name)
        {
            var tags = await _questionService.GetAllTags();
            var qu = tags.Where(t => t.Title.Contains(name)).Select(t => t.Title).ToList();

            return Json(qu);
        }


        [Route("tags")]
        public async Task<IActionResult> FilterTag(FilterTagViewModel filter)
        {
            var result = await _questionService.GetAllFilterTags(filter);
            return View(result);
        }

        #endregion

        #region Question List
        [Route("question")]
        public async Task<IActionResult> QuestionList(FilterQuestionViewModel filter)
        {
            var result = await _questionService.GetAllQuestions(filter);
            return View(result);
        }


        [Route("question/{questionId}")]
        public async Task<IActionResult> GetQuestionDetails(long questionId)
        {
            var question = await _questionService.GetQuestionById(questionId);
            var userIp = Request.HttpContext.Connection.RemoteIpAddress;
            if (question == null) return NotFound();
            if (userIp != null)
            {
                await _questionService.AddViewForQuestion(userIp.ToString(), question);
            }
            ViewBag.bookmarks = false;

            if(User.Identity.IsAuthenticated && await _questionService.IsExistsUserQuestionBookmarkByQuestinIdUserId(questionId,User.GetUserId()))
            {
                ViewBag.bookmarks = true;
            }

            ViewData["tags"] = await _questionService.GetTagsByQuestionId(questionId);
            return View(question);
        }
        [Route("q/{questionId}")]
        public async Task<IActionResult> GetQuestionDetailsByShortLink(long questionId)
        {
            var question = await _questionService.GetQuestionById(questionId);
            if (question == null) return NotFound();

            return RedirectToAction("GetQuestionDetails", "Question", new { questionId = questionId });
        }


        [Route("AnswerQuestion")]
        [Authorize]
        public async Task<IActionResult> AnswerQuestion(AnswerQuestionViewModel answerQuestion)
        {
            if (string.IsNullOrEmpty(answerQuestion.Answer))
            {
                return new JsonResult(new { status = "empty" });
            }
            var userId = User.GetUserId();
            answerQuestion.UserId = userId;
            var result = await _questionService.AnswerQuestion(answerQuestion);
            if (result)
            {
                return new JsonResult(new { status = "success" });
            }
            return new JsonResult(new { status = "error" });
        }

        #endregion

        #region Question list By Tag
        [Route("tags/{tagName}")]
        public async Task<IActionResult> QuestionListByTag(FilterQuestionViewModel filter, string tagName)
        {
            tagName = tagName.Trim().ToLower();
            filter.TagTitle = tagName;
            ViewBag.TagTitle = tagName;
            var result = await _questionService.GetAllQuestions(filter);
            return View(result);
        }
        #endregion


        #region Answer
        [HttpPost("SelectTrueAnswer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectTrueAnswer(long answerId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { status = "NotAuth" });
            }

            if (!await _questionService.HasUserAccessToSelectTrueAnswer(User.GetUserId(), answerId))
            {
                return new JsonResult(new { status = "NotAccess" });
            }
            await _questionService.SelectTrueAnswer(User.GetUserId(), answerId);
            return new JsonResult(new { status = "Success" });
        }

        [HttpPost("ScoreUpForAnswer")]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> ScoreUpForAnswer(long answerId)
        {
            var reslut = await _questionService.CreateScoreForAnswer(answerId, AnswerScore.Plus,User.GetUserId());

            switch (reslut)
            {
                case AnswerScoreResult.error:
                    return new JsonResult(new { status = "error" });
                    break;
                case AnswerScoreResult.MinScoreForUpScoreAnswer:
                    return new JsonResult(new { status = "MinScoreForUpScoreAnswer" });
                    break;
                case AnswerScoreResult.MinScoreForDownScoreAnswer:
                    return new JsonResult(new { status = "MinScoreForDownScoreAnswer" });
                    break;
                case AnswerScoreResult.IsExistsUserScoreForScore:
                    return new JsonResult(new { status = "IsExistsUserScoreForScore" });
                    break;
                case AnswerScoreResult.success:
                    return new JsonResult(new { status = "success" });
                    break;
                default:
                    return NotFound();
                    break;
            }        
        }
        [HttpPost("ScoreDownForAnswer")]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> ScoreDownForAnswer(long answerId)
        {
            var reslut = await _questionService.CreateScoreForAnswer(answerId, AnswerScore.Minus, User.GetUserId());

            switch (reslut)
            {
                case AnswerScoreResult.error:
                    return new JsonResult(new { status = "error" });
                    break;
                case AnswerScoreResult.MinScoreForUpScoreAnswer:
                    return new JsonResult(new { status = "MinScoreForUpScoreAnswer" });
                    break;
                case AnswerScoreResult.MinScoreForDownScoreAnswer:
                    return new JsonResult(new { status = "MinScoreForDownScoreAnswer" });
                    break;
                case AnswerScoreResult.IsExistsUserScoreForScore:
                    return new JsonResult(new { status = "IsExistsUserScoreForScore" });
                    break;
                case AnswerScoreResult.success:
                    return new JsonResult(new { status = "success" });
                    break;
                default:
                    return NotFound();
                    break;
            }
        }

        #endregion


        #region AddQuestionToBookmark

        [HttpPost("AddQuestionToBookmark")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddQuestionToBookmark(long questionId)
        {
            if (await _questionService.CheckAddQuestionToBookmark(questionId, User.GetUserId()))
            {
                return new JsonResult(new { status = "success" });
            }
            return new JsonResult(new { status = "error" });
        }

        #endregion
    }
}
