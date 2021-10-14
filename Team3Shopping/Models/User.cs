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
            Cart = new List<Cart>();
            Purchase = new List<Purchase>();
            Session = new List<Session>();
        }
        [Required]
        [MaxLength(32)]
        [MinLength(6)]
        [EmailAddress]
        public string Id { set; get; }
       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
        
}
