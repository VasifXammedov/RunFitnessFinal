using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Week
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TrainerWeek> TrainerWeeks { get; set; }

    }
}
