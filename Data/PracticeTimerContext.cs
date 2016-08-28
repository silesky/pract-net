using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeTimer.Data.Entities;

namespace PracticeTimer.Data {
    public class PracticeTimerContext : DbContext {
        
        public PracticeTimerContext (DbContextOptions<PracticeTimerContext> options) 
            : base(options)
        {
            
        }

        // consumes the timerEntity Object, which is an object we constructed from values in the db
        public DbSet<Timer> Timers {get;set;}
        public DbSet<TimerGroup> TimerGroups {get;set;}
        
        public DbSet<DbNeedsSeeding> NeedsSeedingSet {get;set;}

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("FileName=./practicetimer.db");
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

        public bool NeedsSeeding () { 
            var record = NeedsSeedingSet.FirstOrDefault();
            
            if (record != null && record.HasBeenSeeded == true) return false;

            return true;
        }

        public void NeedsSeeding (bool needsSeeding) {
            var record = NeedsSeedingSet.FirstOrDefault();

            if (record == null) {
                record = new DbNeedsSeeding();
                NeedsSeedingSet.Add(record);
            }

            record.HasBeenSeeded = !needsSeeding; 

            SaveChanges();
        }

    }
}