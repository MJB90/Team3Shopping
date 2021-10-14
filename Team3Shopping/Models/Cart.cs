using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class Cart
    {
        public Cart()
        {
          Id = new Guid();
        }
        public Guid Id { get; set; }
        public int quantity;

        //Both line 20 & 21 are required to establish one to one relationship with user
        public virtual Guid UserId { get; set; } 
        public virtual User User { get; set; }

    }
}
