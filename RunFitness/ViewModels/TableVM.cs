using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewModels
{
    public class TableVM
    {
        public List<Trainer> Trainers { get; set; }
        public List<Time> Times { get; set; }
        public List<Week> Weeks { get; set; }
        public List<TrainerWeek> trainerWeeks { get; set; }
    }
}
