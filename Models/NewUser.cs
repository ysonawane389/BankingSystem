using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class NewUser
    {
        [Key]
        public long AccNo { get; set; }
        public string AccHolderName { get; set; }
        public long MobNo { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
        public long AvlBalance { get; set; }
    }
}
