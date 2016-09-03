using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PracticeTimer.Data.Entities;
using PracticeTimer.DataTransferObjects;

namespace PracticeTimer.Controllers {
    
    [Route("api/[controller]")]
    public class TimerGroupController : PracticeTimerController {

        [HttpGet("")]
        public IActionResult GetAll() {
            var timerGroupDtos = Context.TimerGroups
                .Select(tg => new OutputTimerGroupDto() {
                    Id = tg.Id,
                    Title = tg.Title,
                    Timers = tg.Timers
                        .Select(t => new OutputTimerDto(){
                            Id = t.Id,
                            Order = t.Order,
                            Time = t.Time,
                            StartTime = t.StartTime,
                            Paused = t.Paused,
                            Ticking = t.Ticking,
                            TimerGroupId = t.TimerGroupId,
                        }).ToList(),       
                })
                .ToList();

                return new ObjectResult(timerGroupDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = Context.TimerGroups.FirstOrDefault(tg => tg.Id == id);

            return new ObjectResult(entity);
        }

        [HttpPut("")]
        public IActionResult Create(RequestToCreateTimerGroupDto dto) {
            var entity = new TimerGroup() {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Timers = dto.Timers.Select(t => new Timer() {
                    Id = Guid.NewGuid(),
                    Title = t.Title,
                    Order = t.Order,
                    Time = t.Time,
                    Ticking = t.Ticking,
                    Paused = t.Paused,
                    StartTime = t.StartTime,
                }).ToList()
            };

            Context.TimerGroups.Add(entity);
            Context.SaveChanges();

            return new ObjectResult(new {Success = true});
        }
        
        // getTotalTime
    }

}