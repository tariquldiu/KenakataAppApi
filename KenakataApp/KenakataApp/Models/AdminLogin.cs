using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KenakataApp.Models
{
    public class AdminLogin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be 6-15 characters")]
        public string Password { get; set; }
        public string UserRoles { get; set; }
    }
}