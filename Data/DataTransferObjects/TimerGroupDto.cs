using System;
using System.Collections.Generic;

namespace PracticeTimer.DataTransferObjects {

    public class OutputTimerGroupDto : BaseTimerGroupDto {
        public Guid Id { get; set; }

        public List<OutputTimerDto> Timers {get;set;}
    }

    public class RequestToCreateTimerGroupDto : BaseTimerGroupDto {

        public List<RequestToCreateTimerDto> Timers { get; set; }

        public RequestToCreateTimerGroupDto() {
            Timers = new List<RequestToCreateTimerDto>();
        }
    }

    public abstract class BaseTimerGroupDto {

        public string Title { get; set; }

        

    }

}