using System;
// this is our viewmodel for our entities. It's going 

// 
namespace PracticeTimer.DataTransferObjects {
    public class TimerDto {
        public int Id {get;set;}
        public string Title {get;set;}
        public int Time {get;set;}
        public bool Ticking {get;set;}
        public int StartTime {get;set;}
        public bool Paused {get;set;}
        public int Order {get;set;}
        
        public int TimerGroupId {get; set;}


    
        }
}