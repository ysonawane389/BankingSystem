using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class AdminLoginViewModel
    {
        [Key]
        [Required(ErrorMessage = "AdminId is Required")]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "AdminName is Required")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Pasword is Required")]
        public string Password { get; set; }
    }
}
