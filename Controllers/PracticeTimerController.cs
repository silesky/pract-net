using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeTimer.Data;

namespace PracticeTimer.Controllers {
    public class PracticeTimerController : Controller {
        
        public PracticeTimerContext Context {get;set;} 

        public PracticeTimerController () {
            Context = new PracticeTimerContext(new DbContextOptions<PracticeTimerContext>());
        }

    }
}