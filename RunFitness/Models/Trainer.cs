using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [NotMapped]
        public string Photo { get; set; }
       
        public string Profession { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }

        public TrainerDetail TrainerDetail { get; set; }
        public List<TrainerWeek> TrainerWeeks { get; set; }

    }
}
