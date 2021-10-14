using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class Purchase
    {
        public Purchase()
        {
            Id = new Guid();
            PurchaseProduct = new List<PurchaseProduct>();
        }
        public Guid Id { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual ICollection<PurchaseProduct> PurchaseProduct { get; set; }
    }
}
