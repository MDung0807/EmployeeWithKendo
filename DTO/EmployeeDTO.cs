using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KendoUIApp2.DTO
{
    public class EmployeeDTO
    {
        [Required(ErrorMessage ="userId not null")]
        public string userId { get; set; }

        public string username { get; set; }

        [Required(ErrorMessage ="Password not null"), MinLength(8)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email not correct")]
        public string email { get; set; }
        [Required]
        [RegularExpression(@"^(0)?([0-9]{9})$", ErrorMessage ="Tel not correct")]
        public string tel { get; set; }

        public int disable {get; set; }
    }
}
