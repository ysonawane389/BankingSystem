using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Deposit
    {
        public long Amount { get; set; }
        [Key]
        public string Password { get; set; }
    }
}
