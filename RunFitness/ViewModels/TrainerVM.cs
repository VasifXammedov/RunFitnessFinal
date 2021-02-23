using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewModels
{
    public class TrainerVM
    {
        public Trainer Trainer { get; set; }
        public TrainerDetail TrainerDetail { get; set; }
        public Time Time { get; set; }
       
    }
}
