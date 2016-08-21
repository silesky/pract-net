using System.Threading;
using Timer = PracticeTimer.Entities.Timer;

namespace PracticeTimer.Data {
    public static class PracticeTimerContextSeeder {
        public static void Seed(PracticeTimerContext context) {
            context.Timers.Add(new Timer{Title = "", })
             
        }
    }
}