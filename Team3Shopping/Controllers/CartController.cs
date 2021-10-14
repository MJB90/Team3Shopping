﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team3Shopping.Models;

namespace Team3Shopping.Controllers
{
    public class CartController : Controller
    {
        private myDBContext dbContext;
        public CartController (myDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Landing action 
        public IActionResult ToCart()
        {
            List<Cart> carts = dbContext.Carts.Where(x =>
                x.UserId != null && x.ProductId != null).ToList();
            List<Product> products = new List<Product>();
            foreach (Cart c in carts)
            {
                Product product = dbContext.Products.FirstOrDefault(x =>
                  x.Id == c.ProductId);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            List<User> users = new List<User>();
            foreach (Cart c in carts)
            {
                User user = dbContext.Users.FirstOrDefault(x =>
                  x.Id == c.UserId);
                if (user != null)
                {
                    users.Add(user);
                }
            }


            ViewData["product"] = products;
            ViewData["user"] = users;
            ViewData["cart"] = carts;
            return View();
        }
    }
}