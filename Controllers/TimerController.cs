using System;
using System.Linq;
using System.Reflection;
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
            Console.WriteLine("### individual timer hit!");
            
            return new ObjectResult(timerDto);
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            Console.WriteLine("### hit get all timers!");
            var timers = Context.Timers
                        .Select((t) => new {
                            Id = t.Id,
                            Order = t.Order,
                            Paused = t.Paused,
                            Time = t.Time,
                            Ticking = t.Ticking,
                            Title = t.Title,
                            TimerGroupId = t.TimerGroupId,
                            StartTime = t.StartTime,
                        }).ToList();

            return new ObjectResult(timers);
        }

        private OutputTimerDto toDto(Timer timerEntity) {
            // we should break down toDto 
            var dto = new OutputTimerDto();
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

        private Timer toEntity(RequestToCreateTimerDto dto) {
            var entity = new Timer(){
            Id = Guid.NewGuid(),
            Order = dto.Order,
            Paused = dto.Paused,
            Time = dto.Time,
            Ticking = dto.Ticking,
            Title = dto.Title,
            TimerGroup = Context.TimerGroups.FirstOrDefault(tg => tg.Id == dto.TimerGroupId),
            StartTime = dto.StartTime
            }; 
            return entity;
        } 

        [HttpPost("")]
        public IActionResult Create([FromBody] RequestToCreateTimerDto dto)
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

        [HttpPut()]
        public IActionResult Update([FromBody] RequestToUpdateTimerDto dto)
        {
            var entity = Context.Timers.First(t => t.Id == dto.Id);
            
            entity.Title = dto.Title;
            entity.StartTime = dto.StartTime;
            entity.Order = dto.Order;
            entity.Paused = dto.Paused;
            entity.Ticking = dto.Ticking;

            Context.SaveChanges();

            return new ObjectResult(new { Success = true });
        }
   
    }
}


