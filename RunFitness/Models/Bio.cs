﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Bio
    {
        public int Id { get; set; }
        [Required]
        public string LogoImage { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
