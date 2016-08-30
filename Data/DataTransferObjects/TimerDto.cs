using System;

namespace PracticeTimer.DataTransferObjects {
    
    public class OutputTimerDto : BaseTimerDto {
        public Guid Id {get;set;}
    }

    public class RequestToCreateTimerDto : BaseTimerDto {
    }
    public abstract class BaseTimerDto {
      
        public string Title {get;set;}
        public int Time {get;set;}
        public bool Ticking {get;set;}
        public int StartTime {get;set;}
        public bool Paused {get;set;}
        public int Order {get;set;}
        
        public Guid TimerGroupId {get; set;}


    
        }
}