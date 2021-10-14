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
       
}
