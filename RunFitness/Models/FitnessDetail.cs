using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class FitnessDetail
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Fitness")]
        public int FitnessId { get; set; }
        public Fitness Fitness { get; set; }
    }
}
