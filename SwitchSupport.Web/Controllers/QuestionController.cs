﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
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
            if(result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                return Redirect("/");
            }

            createQuestion.SelectTagsJson = JsonConvert.SerializeObject(createQuestion.SelectTags);
            createQuestion.SelectTags = null;
            return View(createQuestion);
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
            if (question == null) return NotFound();
            ViewData["tags"] = await _questionService.GetTagsByQuestionId(questionId);
            return View(question);
        }

        #endregion

        #region Question list By Tag
        [Route("tags/{tagName}")]
        public async Task<IActionResult> QuestionListByTag(FilterQuestionViewModel filter,string tagName)
        {
            tagName = tagName.Trim().ToLower();
            filter.TagTitle = tagName;
            ViewBag.TagTitle = tagName;
            var result = await _questionService.GetAllQuestions(filter);
            return View(result);
        }
        #endregion


    }
}
