using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsDeleted { get; set; }

        public ServiceDetail ServiceDetail { get; set; }
    }
}
