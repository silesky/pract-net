using Microsoft.AspNetCore.Mvc;
using PracticeTimer.Data;

namespace PracticeTimer.Controllers {
    public class PracticeTimerController : Controller {
        
        public PracticeTimerContext Context {get;set;} 

    }
}