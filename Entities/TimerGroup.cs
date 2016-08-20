using System;
using System.Collections.Generic;

namespace PracticeTimer.Entities
{
    public class TimerGroup {

        public Guid Id {get ;set;}
        public List<Timer> Timers {get;set;}

        public TimerGroup () {
            Timers = new List<Timer>();
        }
    }
}