using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class NewUserViewModel
    {
        [Key]
        [Required(ErrorMessage ="Please Enter AccNo")]
        [MaxLength(8)]
        public int AccNo { get; set; }

        [Required(ErrorMessage = "Please Enter AccHolderName")]
        public string AccHolderName { get; set; }

        [Required(ErrorMessage = "Please Enter MobNo")]
        [MaxLength(10)]
        public int MobNo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Please Deposite Minimum 5000 ")]
        [Range(5000,5000000)]
        public int AvlBalance { get; set; }
    }
}
