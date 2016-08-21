using Microsoft.EntityFrameworkCore;
using PracticeTimer.Entities;

namespace PracticeTimer.Data {
    public class PracticeTimerContext : DbContext {
        
        public PracticeTimerContext (DbContextOptions<PracticeTimerContext> options) 
            : base(options)
        {

        }
        public DbSet<Timer> Timers {get;set;}
        public DbSet<TimerGroup> TimerGroups {get;set;}
        
        protected override void OnModelCreating(ModelBuilder builder) {
                base.OnModelCreating(builder);
        }

    }
}