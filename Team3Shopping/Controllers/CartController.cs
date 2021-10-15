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
        //Redirect to cart page 
        public IActionResult ToCart()
        {

            //Select all products in the cart
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

            //Select all users who have added some products to the cart
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

        public IActionResult Remove( string UserId , string ProductId , string AddToCartProductQuantity)
        {
            Guid productId = Guid.Parse(ProductId);
            //Update carts database
            Cart cart = dbContext.Carts.FirstOrDefault(x =>
              x.ProductId == productId && x.UserId == UserId);
            cart.AddToCartProductQuantity -= Convert.ToInt32(AddToCartProductQuantity);

            //Update products database
            Product product = dbContext.Products.FirstOrDefault(x =>
              x.Id == productId);
            product.ProductQuantity += Convert.ToInt32(AddToCartProductQuantity);

            dbContext.SaveChanges();
            return Json(new { successfullyUpdateDatabase = true });
        }
        //Update products and carts database
        public IActionResult EditQuantity(string UserId, string ProductId, string ProductQuantity)
        {
            Product product = dbContext.Products.FirstOrDefault(x => x.Id == Guid.Parse(ProductId));

            Cart cart = dbContext.Carts.FirstOrDefault(x =>
            x.UserId == UserId && x.ProductId == Guid.Parse(ProductId));

            product.ProductQuantity += (cart.AddToCartProductQuantity - Convert.ToInt32(ProductQuantity));
            cart.AddToCartProductQuantity -= (cart.AddToCartProductQuantity - Convert.ToInt32(ProductQuantity));

            dbContext.SaveChanges();
            return Json(new { data = true  });
        }

        //Redirect to the purhase is been made page
        public IActionResult PurchaseDone()
        {
            //Do some validations of the payment
            //Omitted for brevity
            return View();
        }

        private Session GetSession()
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }
            Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);
            Session session = dbContext.Sessions.FirstOrDefault(x =>
              x.Id == sessionId);
            return session;
        }
    }
}
