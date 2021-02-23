using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewModels
{
    public class ServiceVM
    {
        public List<ServiceDetail> ServiceDetails { get; set; }
        public List<Service> Services { get; set; }
        public List<Popular> Populars { get; set; }
        public List<PopularDetail> PopularDetails { get; set; }

    }
}
