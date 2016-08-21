using Microsoft.EntityFrameworkCore;
using PracticeTimer.Data.Entities;

namespace PracticeTimer.Data {
    public class PracticeTimerContext : DbContext {
        
        public PracticeTimerContext (DbContextOptions<PracticeTimerContext> options) 
            : base(options)
        {
            
        }
        public DbSet<Timer> Timers {get;set;}
        public DbSet<TimerGroup> TimerGroups {get;set;}
        
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("FileName=./practicetimer.db");
        }
        protected override void OnModelCreating(ModelBuilder builder) {
                base.OnModelCreating(builder);
        }

    }
}