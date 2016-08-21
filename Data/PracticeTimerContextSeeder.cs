using System;
using System.Collections.Generic;
using Timer = PracticeTimer.Entities.Timer;

namespace PracticeTimer.Data
{
    public static class PracticeTimerContextSeeder {
        public static void Seed(PracticeTimerContext context) {
            var timers = new List<Timer> {
                new Timer{Id = Guid.NewGuid(), Title = "", StartTime = 20, }
            };

            foreach (var t in timers) context.Timers.Add(t);
            context.SaveChanges();
             
        }
    }
}