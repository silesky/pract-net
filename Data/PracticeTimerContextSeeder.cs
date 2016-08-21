using System;
using System.Collections.Generic;
using System.Threading;
using Timer = PracticeTimer.Entities.Timer;

namespace PracticeTimer.Data {
    public static class PracticeTimerContextSeeder {
        public static void Seed(PracticeTimerContext context) {
            var timers = new List<Timer> {
                new Timer{Id = Guid.NewGuid(), Time=2, Title = "eat", Ticking = false, StartTime = 20, Paused = true},
                new Timer{Id = Guid.NewGuid(), Time=4, Title = "love", Ticking = false, StartTime = 20, Paused = true},
                new Timer{Id = Guid.NewGuid(), Time=6, Title = "pray", Ticking = false, StartTime = 20, Paused = true}
            };
        
            foreach (var t in timers) context.Timers.Add(t);
            context.SaveChanges();
             
        }
    }
}