using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class TrainerDetail
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Achievement { get; set; }
        public string Experience { get; set; }
        public string Hobbie { get; set; }
        public string Facuilt { get; set; }
        public string IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

    }
}
