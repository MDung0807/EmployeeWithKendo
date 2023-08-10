using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoUIApp2.DTO
{
    public class EmployeeUpdateDTO
    {
        [Required(ErrorMessage = "userId not null")]
        public string userId { get; set; }

        public string username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email not correct")]
        public string email { get; set; }
        [Required]
        [RegularExpression(@"^(0)?([0-9]{9})$", ErrorMessage = "Tel not correct")]
        public string tel { get; set; }
    }
}