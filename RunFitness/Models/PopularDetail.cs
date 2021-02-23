using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class PopularDetail
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Popular")]
        public int PopularId { get; set; }
        public Popular Popular { get; set; }
    }
}
