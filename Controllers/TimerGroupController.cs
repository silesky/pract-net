using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PracticeTimer.Controllers {
    
    [Route("api/[controller]")]
    public class TimerGroupController : PracticeTimerController {

        public IActionResult Get(Guid id) {
            var entity = Context.TimerGroups.FirstOrDefault(tg => tg.Id == id);
        } 

    }

}