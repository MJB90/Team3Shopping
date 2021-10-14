using System;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class PurchaseProducts
    {
        public PurchaseProducts()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
    }
}
