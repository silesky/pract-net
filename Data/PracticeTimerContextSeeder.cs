using System;
using System.Collections.Generic;
using PracticeTimer.Data.Entities;
using Timer = PracticeTimer.Data.Entities.Timer;

namespace PracticeTimer.Data
{
    public static class PracticeTimerContextSeeder {
        public static void Seed(PracticeTimerContext context) {
            Console.WriteLine("Adding Seed Data...");
            
            var timerGroup = new TimerGroup {
                Id = 0,
                Title = "Sample Timer Group",
            };

            var timers = new List<Timer> {
                new Timer{Id = 1, Time=2, Title = "eat", Ticking = false, StartTime = 20, Paused = true, TimerGroup = timerGroup},
                new Timer{Id = 2, Time=4, Title = "love", Ticking = false, StartTime = 20, Paused = true, TimerGroup = timerGroup},
                new Timer{Id = 3, Time=6, Title = "pray", Ticking = false, StartTime = 20, Paused = true, TimerGroup = timerGroup}
            };
        
            foreach (var t in timers) context.Timers.Add(t);
            context.SaveChanges();
            
            Console.WriteLine("Added Seed data!");
        }
    }
}