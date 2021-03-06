using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Team3Shopping.Models
{
    public class Product
    {
        public Product()
        {
            Id = new Guid();
            Cart = new List<Cart>();
            PurchaseProduct = new List<PurchaseProduct>();
            ProductReview = new List<ProductReview>();
        }
        public Guid Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(256)]
        public string ProductDesc { get; set; }

        [Required]
        public float ProductPrice { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required]
        [MaxLength(32)]
        public string ProductCategory { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<PurchaseProduct> PurchaseProduct { get; set; }

        public virtual ICollection<ProductReview> ProductReview { get; set; }
    }
}
