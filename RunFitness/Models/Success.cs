using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Success
    {
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public SuccessDetail SuccessDetail { get; set; }
    }
}
