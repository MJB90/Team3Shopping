using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class User
    {
        public User()
        {
        }

        [Required]
        [Key]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(32)]
        public string Email { get; set; }

    }
}
