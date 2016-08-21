using System;

namespace PracticeTimer.Entities {
    public class Timer {
        public Guid Id {get;set;}
        public string Title {get;set;}
        public int Time {get;set;}
        public bool Ticking {get;set;}
        public int StartTime {get;set;}
        public bool Paused {get;set;}
        public int Order {get;set;}
        
        public Guid TimerGroupId {get; set;}

        public virtual TimerGroup TimerGroup {get; set;}
    
        }
}