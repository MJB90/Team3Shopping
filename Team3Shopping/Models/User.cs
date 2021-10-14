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
            Cart = new List<Cart>();
            Purchase = new List<Purchase>();
            Session = new List<Session>();
        }

        public Guid Id { set; get; }
        [Required]
        [MaxLength(32)]
        public string Username { get; set;}
        [Required]
        public byte[] PassHash { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
        
}
