using Microsoft.AspNetCore.Mvc;

namespace PracticeTimer.Controllers {

    public class PublicController : PracticeTimerController {

        [HttpGet("")]
        public IActionResult Login() {

            return new ViewResult("Login", );
        }

    }

}