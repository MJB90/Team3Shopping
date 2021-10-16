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
        private Utility utility;

        public CartController (myDBContext dbContext)
        {
            this.dbContext = dbContext;
            utility = new Utility(dbContext);
        }
        //Redirect to cart page 
        public IActionResult ToCart()
        {
            Session session = utility.GetSession(Request);
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //Select all products in the cart
            List<Cart> carts = dbContext.Carts.Where(x =>
                x.UserId == session.UserId && x.ProductId != null).ToList();
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
        [HttpPost]
        public IActionResult Remove(string UserId,string ProductId)
        {
            Session session = utility.GetSession(Request);
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Guid productId = Guid.Parse(ProductId);
            //Update carts database
            Cart cart = dbContext.Carts.FirstOrDefault(x =>
              x.ProductId == productId && x.UserId == UserId);
            cart.AddToCartProductQuantity = 0;

            dbContext.SaveChanges();
            return Json(new { successfullyUpdateDatabase = true });
        }
        //Update products and carts database
        [HttpPost]
        public IActionResult EditQuantity(string UserId, string ProductId, string ProductQuantity)
        {
            Session session = utility.GetSession(Request);
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Guid productId = Guid.Parse(ProductId);
            Cart cart = dbContext.Carts.FirstOrDefault(x =>
            x.UserId == UserId && x.ProductId == productId);
            int productQuantity = Int32.Parse(ProductQuantity);
            cart.AddToCartProductQuantity =productQuantity;

            dbContext.SaveChanges();
            return Json(new { data = true  });
        }

        //Redirect to the purhase is been made page

        public IActionResult PurchaseDone(string UserId, string ProductId)
        {
            Session session = utility.GetSession(Request);
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //Do some validations of the payment method

            //Update database
            List<Cart> carts = dbContext.Carts.Where(x =>
            x.UserId == session.UserId && x.AddToCartProductQuantity > 0).ToList();

            if (carts == null)
            {
                return View("Unsuccessful");
            }
   
            dbContext.Add(new Purchase
            {
                UserId = session.UserId,
                PurchaseDate = DateTime.Now
            });
            dbContext.SaveChanges();

            Purchase purchases = dbContext.Purchases.FirstOrDefault(x =>
            x.Id != null && x.UserId == session.UserId);
            List<Product> products = new List<Product>();
            foreach (Cart cart in carts)
            {
                Product product = dbContext.Products.FirstOrDefault(x => 
                x.Id == cart.ProductId);
                products.Add(product);
                cart.AddToCartProductQuantity = 0;
            }
            foreach (Product p in products)
            {
                dbContext.Add(new PurchaseProduct
                {
                    PurchaseId = purchases.Id,
                    ProductId = p.Id
                });
            }
            dbContext.SaveChanges();
            return View();
        }

        
    }
}
