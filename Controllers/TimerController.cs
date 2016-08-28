using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PracticeTimer.Data.Entities;
using PracticeTimer.DataTransferObjects;

namespace PracticeTimer.Controllers {
    [Route("api/[controller]")]
    public class TimerController : PracticeTimerController {

    

        [HttpGet("{id:guid}")]
        public IActionResult GetTimer(Guid id) {
            var timer = Context.Timers.FirstOrDefault(t => t.Id == id);

            Console.WriteLine("get individual timer hit!" );
            return new ObjectResult(timer);
        }
    [HttpGet("")]
        public IActionResult GetAll () {
            Console.WriteLine("# hit get all timers");
            var timers = Context.Timers.ToList();

            return new ObjectResult(timers);
        }
        [HttpPut("")]
        public IActionResult Create (TimerDto dto) {
                // when you hit the put route, you get {success: true} if it suceeded
                // http://localhost:5000/api/timer/f46020bc-e1c0-4225-a04f-20415156b3a5
                var timerEntity = new Data.Entities.Timer();
                // private variables are camelCase
                timerEntity.Id = dto.Id; //new Guide() would create a blank guid
                timerEntity.Time = dto.Time;
                timerEntity.Ticking = dto.Ticking;
                timerEntity.Title = dto.Title;
                timerEntity.TimerGroupId = dto.TimerGroupId;
                timerEntity.Paused = dto.Paused;
                Context.Timers.Add(timerEntity);
                Context.SaveChanges();
                return new ObjectResult(new {success=true});
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id) {
            var entity = Context.Timers.First(t => t.Id == id);

            Context.Timers.Remove(entity);
            Context.SaveChanges();

            return new ObjectResult(new {Success = true});
        }


    }
}
