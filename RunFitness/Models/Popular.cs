using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Popular
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        public PopularDetail PopularDetail { get; set; }
    }
}
