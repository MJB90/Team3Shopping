using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team3Shopping.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Team3Shopping.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        private myDBContext dbContext;
        public PurchaseHistoryController(myDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // overview of purchase history
        public IActionResult Index()
        {
            // check session first and get user
            // To be added :)
            // placeholder
            //Session get user id
            //-> use user id to get purchase
            //-> use purchase id to get purchaseProduct
            //-> use purchaseProduct to get product + activationCode? 

            // 1 user id has many sessions
            // login -> session generated.
                        
            // obtain list of sessions.
            List<Session> sessions = dbContext.Sessions.Where(x =>
                x.Id != null).ToList();
            User userId = null;

            // query Sessions and get User where UserId == session userId
            foreach (Session s in sessions)
            {
                userId = dbContext.Users.FirstOrDefault(x => x.Id == s.UserId);
            }
            
        
            // obtain list of all purchases
            // tbc: how to pass in userId object into Where method?
            List<Purchase> purchaseList = dbContext.Purchases.Where(x =>
                x.UserId == Guid.Parse(userId.Id)).ToList();
            

            List<PurchaseProduct> purProdList = new List<PurchaseProduct>();            
            foreach (Purchase p in purchaseList)
            {
                PurchaseProduct purProd = dbContext.PurchaseProducts.FirstOrDefault(x =>
                 x.PurchaseId == p.Id);
                if (purProdList != null)
                {
                    purProdList.Add(purProd);                  
                }
            }
          
            List<Product> productList = new List<Product>();
            List<string> activationCodeList = new List<string>();
            foreach (PurchaseProduct pp in purProdList)
            {
                Product product = dbContext.Products.FirstOrDefault(x =>
                 x.Id == pp.ProductId);
                if (productList != null)
                {
                    productList.Add(product);
                    activationCodeList.Add(pp.ProductActivationCode.ToString());
                }
            }

            ViewData["productList"] = productList;
            ViewData["purchases"] = purchaseList;
            ViewData["activationCodes"] = activationCodeList;
            ViewData["quantity"] = activationCodeList.Count;
            return View();
        }
    }
}
