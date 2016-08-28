using System;

namespace PracticeTimer.Data.Entities {

    public class DbNeedsSeeding {

        public Guid Id { get; set; }
        public bool HasBeenSeeded { get; set; }

    }

}