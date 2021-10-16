using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class ProductReview
    {
        public ProductReview()
        {
            Id = new Guid();
            Timestamp = DateTime.Now.ToString("dd MMM yyyy"); //e.g. 03 Sep 2021
        }

        public Guid Id { get; set; }



        [Required]
        [MaxLength(500)]
        public string ReviewText { get; set; }
        
        [Required]
        public int Rating { get; set; } //int from 1-5

        [Required]
        public string Timestamp { get; set; }

        public virtual Guid ProductId {get; set;} //FK

        public virtual string UserId { get; set; } //FK

    }
}
