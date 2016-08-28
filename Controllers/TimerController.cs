using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PracticeTimer.Data.Entities;
using PracticeTimer.DataTransferObjects;

namespace PracticeTimer.Controllers
{
    [Route("api/[controller]")]
    public class TimerController : PracticeTimerController
    {

        [HttpGet("{id:guid}")]
        public IActionResult GetTimer(Guid id)
        {
            var timer = Context.Timers.FirstOrDefault(t => t.Id == id);
            var timerDto = toDto(timer);
            Console.WriteLine("get individual timer hit!");
            
            return new ObjectResult(timerDto);
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            Console.WriteLine("# hit get all timers");
            var timers = Context.Timers.ToList().Select(toDto).ToList();

            return new ObjectResult(timers);
        }

        private TimerDto toDto(Timer timerEntity) {
            var dto = new TimerDto();
            dto.Id = timerEntity.Id;
            dto.Order = timerEntity.Order;
            dto.Paused = timerEntity.Paused;
            dto.Time = timerEntity.Time;
            dto.Ticking = timerEntity.Ticking;
            dto.Title = timerEntity.Title;
            dto.TimerGroupId = timerEntity.TimerGroupId;
            dto.StartTime = timerEntity.StartTime;
            return dto;
        } 
        // TimerDto is found in the bottom of the http request

        private Timer toEntity(TimerDto dto, Timer savedEntity = null) {
            var entity = (savedEntity != null) ? savedEntity:  new Timer();
            if (entity.Id == Guid.Empty) entity.Id = dto.Id;
            entity.Order = dto.Order;
            entity.Paused = dto.Paused;
            entity.Time = dto.Time;
            entity.Ticking = dto.Ticking;
            entity.Title = dto.Title;
            entity.TimerGroup = Context.TimerGroups.FirstOrDefault(tg => tg.Id == dto.TimerGroupId);
            entity.StartTime = dto.StartTime;
            return entity;
        } 

        [HttpPost("")]
        public IActionResult Create([FromBody] TimerDto dto)
        {
            // when you hit the put route, you get {success: true} if it suceeded
            // http://localhost:5000/api/timer/f46020bc-e1c0-4225-a04f-20415156b3a5
            // private variables are camelCase
            var timerEntity = toEntity(dto);
            
            Context.Timers.Add(timerEntity);
            Context.SaveChanges();
            
            return CreatedAtRoute("", new { sucess = true }, timerEntity);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var entity = Context.Timers.First(t => t.Id == id);

            Context.Timers.Remove(entity);
            Context.SaveChanges();

            return new ObjectResult(new { Success = true });
        }

        [HttpPost("{id:guid}")]
        public IActionResult Update([FromBody] TimerDto dto)
        {
            var entity = Context.Timers.First(t => t.Id == dto.Id);
            
            entity = toEntity(dto, entity);
            Context.SaveChanges();

            return new ObjectResult(new { Success = true });
        }
    }
}
