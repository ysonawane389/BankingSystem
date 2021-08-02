

using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class NewUserViewModel
    {
        [Key]
       




       [Required(ErrorMessage ="Account Number Should be 12 digit")]
        public long AccNo { get; set; }

        [Required(ErrorMessage = "Please Enter AccHolderName")]
        public string AccHolderName { get; set; }

        [Required( ErrorMessage = "MobNo Number Should be 10 digit")]
        public long MobNo { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter EmailId")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter DateOfBirth")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Enter City")]
        public string City { get; set; }

        [Range(5000, 5000000,ErrorMessage ="please Credit Minimum Balance 5000")]
        public long AvlBalance { get; set; }
    }
}
