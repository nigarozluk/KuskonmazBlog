using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class User
    {
        public int userID { get; set; }
        [Required]
        public string userName { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string userMail { get; set; }
        
        public List<Comment> Comments { get; set; }//Her userın birden fazla yorumu olabilir
    }
}
