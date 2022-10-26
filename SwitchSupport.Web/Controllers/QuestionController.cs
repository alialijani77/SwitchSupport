using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwitchSupport.Web.Controllers
{
    public class QuestionController : BaseController
    {
        #region Question
        [Authorize]
        [HttpGet("create-question")]
        public IActionResult CreateQuestion()
        {
            return View();
        }       
        #endregion
    }
}
