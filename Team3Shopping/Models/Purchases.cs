using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class Purchases
    {
        public Purchases()
        {
            Id = new Guid();
            PurchaseProducts = new List<PurchaseProducts>();
        }
        public Guid Id { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }

        //one to many relationship with purchase products
        public virtual ICollection<PurchaseProducts> PurchaseProducts { get; set; }
    }
}
