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
            Id = new Guid();
            Purchases = new List<Purchases>();
        }

        public Guid Id { get; set; }
        [Required]
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

        //Below line is required to maintain one to one relationship with Cart
        public virtual Cart Cart { get; set; }

        //Below line is required to maintain one to many relationship with Purchases
        public virtual ICollection<Purchases> Purchases { get; set; }

    }
}
