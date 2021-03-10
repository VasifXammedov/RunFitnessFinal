using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public string Message { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
