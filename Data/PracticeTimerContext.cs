using Microsoft.EntityFrameworkCore;
using PracticeTimer.Entities;

namespace PracticeTimer.Data {
    public class PracticeTimerContext : DbContext {
        
        public DbSet<Timer> Timers {get;set;}

        public override void OnModelCreating(ModelBuilder builder) {
                
        }

    }
}