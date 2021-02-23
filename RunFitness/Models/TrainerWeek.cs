using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class TrainerWeek
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }
        public int WeekId { get; set; }
        public virtual Week Week { get; set; }
        public int TimeId { get; set; }
        public virtual Time Time { get; set; }

    }
}
