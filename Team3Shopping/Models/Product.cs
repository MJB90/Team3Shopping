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
            ProductId = new Guid();
        }

        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(32)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProductDesc { get; set; }

        [Required]
        public float ProductPrice { get; set; }

        [Required]
        [MaxLength(32)]
        public string ProductImageFileName { get; set; }

        [Required]
        [MaxLength(32)]
        public string ProductCategory { get; set; }
    }
}
