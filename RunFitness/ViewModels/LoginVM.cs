using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewModels
{
    public class LoginVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; internal set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; internal set; }
    }
}
