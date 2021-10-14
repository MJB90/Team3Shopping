using System;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class PurchaseProduct
    {
        public PurchaseProduct()
        {
            ProductActivationCode = new Guid();
        }
        [Key]
        public Guid ProductActivationCode { get; set; }

        public virtual Guid ProductId { get; set; }

        public virtual Guid PurchaseId { get; set; }
    }
}
