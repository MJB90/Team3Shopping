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
            // obtain userId from session
            string sessionId = HttpContext.Request.Cookies["SessionId"];
            Session thisSession = dbContext.Sessions.FirstOrDefault(x => x.Id == Guid.Parse(sessionId)); //get the session object in DB from current sessionId
            User thisUser = dbContext.Users.FirstOrDefault(x => x.Id == thisSession.UserId);

            string userId = thisUser.Id;


            // obtain list of all purchases            
            List<Purchase> purchaseList = dbContext.Purchases.Where(x =>
                x.UserId == userId).ToList();

            List<List<Product>> allProductList = new List<List<Product>>();
            List<PurchaseProduct> purProdList = new List<PurchaseProduct>();
            //List<Product> productList = new List<Product>();
            List<List<string>> allActCodeList = new List<List<string>>();
            List<string> activationCodeList = new List<string>();
            int i = 0;

            foreach (Purchase p in purchaseList)
            {
                purProdList = dbContext.PurchaseProducts.Where(x =>
                 x.PurchaseId == p.Id).ToList();

                allProductList.Add(new List<Product>());
                allActCodeList.Add(new List<string>());

                foreach (PurchaseProduct pp in purProdList)
                {                    
                    allProductList[i].Add(dbContext.Products.FirstOrDefault(x =>
                   x.Id == pp.ProductId));
                    allActCodeList[i]. Add(pp.ProductActivationCode.ToString());
                    
                }
                i++;

                
                //if (purProdList != null)
                //{
                //    purProdList.Add(purProd);
                //}
            }

            ViewData["allProductList"] = allProductList;
            ViewData["allActCodeList"] = allActCodeList;
            //ViewData["productList"] = productList;
            ViewData["purchases"] = purchaseList;
            ViewData["activationCodes"] = activationCodeList;
            ViewData["quantity"] = activationCodeList.Count;
            return View();
        }

        public IActionResult Test()
        {
            ViewData["Title"] = "My Purchase";
            return View();
        }
    }
}
