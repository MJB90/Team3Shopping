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
            Timestamp = DateTime.Now.ToString("dd MMMM yyyy, hh:mm tt"); //e.g. 03 September 2021, 09:30 AM (12 hr clock)
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
