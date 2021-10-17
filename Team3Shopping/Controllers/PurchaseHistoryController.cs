using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            // initialize list of dicts to store prod data, key = product object, value = quantity
            List<Dictionary<Product, int>> allProdList = new List<Dictionary<Product, int>>();                     

            List<PurchaseProduct> purProdList = new List<PurchaseProduct>();            
            List<string> activationCodeList = new List<string>();
            List<string> purchaseDatesList = new List<string>();

            // to track which transaction (purchase) we are looking at in allProdList during iteration of purchaseList
            int i = 0;

            foreach (Purchase p in purchaseList)
            {
                purProdList = dbContext.PurchaseProducts.Where(x =>
                 x.PurchaseId == p.Id).ToList();

                allProdList.Add(new Dictionary<Product, int>());

                foreach (PurchaseProduct pp in purProdList)
                {
                    // if key (ie product) found, increment value by 1
                    if (allProdList[i].ContainsKey(dbContext.Products.FirstOrDefault(x =>
                      x.Id == pp.ProductId)))
                    {
                        allProdList[i][dbContext.Products.FirstOrDefault(x =>
                      x.Id == pp.ProductId)] += 1;
                    }
                    else
                    {
                        // if key (ie product) not found, add product object to dictionary, with value 1
                        allProdList[i].Add(dbContext.Products.FirstOrDefault(x =>
                      x.Id == pp.ProductId), 1);
                    }

                    activationCodeList.Add(pp.ProductActivationCode.ToString());
                    purchaseDatesList.Add(p.PurchaseDate.ToString());
                }
                i++;                
            }

            ViewData["allProdList"] = allProdList;            
            ViewData["activationCodeList"] = activationCodeList;
            ViewData["purchaseDatesList"] = purchaseDatesList;
            //ViewData["quantity"] = activationCodeList.Count;
            return View();
        }
      
    }
}
