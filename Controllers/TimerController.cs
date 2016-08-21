using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PracticeTimer.Controllers {
    [Route("api/[controller]")]
    public class TimerController : PracticeTimerController {
        
        [HttpGet("{id}")]
        public IActionResult GetTimer(Guid id) {
            var timer = Context.Timers.FirstOrDefault(t => t.Id == id);
            

            return new ObjectResult(timer);
        }

        [HttpGet("")]
        public IActionResult GetAll () {
            Console.WriteLine("hit get all timers");
            var timers = Context.Timers.ToList();
            
            return new ObjectResult(timers);
        }

        

    }
}