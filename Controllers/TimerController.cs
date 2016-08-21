using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace PracticeTimer.Controllers {
    [Route("api/[controller]")]
    public class TimerController : PracticeTimerController {
        
        [HttpGet("{id}")]
        public IActionResult GetTimer(int id) {
            var timer = Context.Timers.FirstOrDefault(t => t.Id == id);
            

            return new ObjectResult(timer);
        }

        

    }
}