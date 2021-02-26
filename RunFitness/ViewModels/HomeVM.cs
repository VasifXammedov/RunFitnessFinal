using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public FitnessDetail FitnessDetail { get; set; }
        public List<ServiceDetail> ServiceDetails { get; set; }
        public List<Service> Services { get; set; }
        public List<Popular> Populars { get; set; }
        public List<PopularDetail> PopularDetails { get; set; }
        public Success Success { get; set; }
        public SuccessDetail SuccessDetail { get; set; }
        public FooterBottom FooterBottom { get; set; }
        public List<Trainer> Trainers { get; set; }
        public Contact Contact { get; set; }


    }
}
