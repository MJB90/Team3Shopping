using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class Cart
    {
        public virtual string UserId { get; set; }
        public virtual Guid ProductId { get; set; }

        public int AddToCartProductQuantity { get; set; }

    }
}
