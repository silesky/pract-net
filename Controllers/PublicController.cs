using Microsoft.AspNetCore.Mvc;

namespace PracticeTimer.Controllers {

    [Route("/")]
    public class PublicController : PracticeTimerController {

        [HttpGet]
        public IActionResult Index() {
            return View("Index");
        }

        // [HttpGet("")]
        // public IActionResult Login() {

        //     return new ViewResult("Login", );
        // }

    }

}
