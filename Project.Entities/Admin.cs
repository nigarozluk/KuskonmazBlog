using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class Admin
    {
        public int adminID { get; set; }
        [Required]
        public string adminName { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string adminMail { get; set; }
        [Required]
        public string adminpassword { get; set; }

    }
}
