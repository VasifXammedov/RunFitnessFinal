using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewModels
{
    public class AboutVM
    {
        public Fitness Fitness { get; set; }
        public FitnessDetail FitnessDetail { get; set; }
        public About About { get; set; }
        public Success Success { get; set; }
        public SuccessDetail SuccessDetail { get; set; }
    }
}
