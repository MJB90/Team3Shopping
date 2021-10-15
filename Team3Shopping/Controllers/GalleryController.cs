using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team3Shopping.Models;


namespace Team3Shopping.Controllers
{
    public class GalleryController : Controller
    {
        private myDBContext dBContext;

        public GalleryController(myDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Index(string searchString)
        {
            //For first access, to change null into empty string to show all product
            if (searchString == null) { searchString = ""; }

            //check if the session is valid-->need to be checked when merge with login
           /*
            * string thisUsername = HttpContext.Session.GetString("username");
            string thisSessionId = HttpContext.Session.GetString("sessionId");
            if (thisSessionId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            */
            //to make a product list which contain search string in either name, desc, category
            List<Product> products = dBContext.Products.Where(x =>
               x.ProductName.Contains(searchString)||
               x.ProductDesc.Contains(searchString)||
               x.ProductCategory.Contains(searchString)
               ).ToList();

            //pushing the data into gallery view
            ViewData["searchString"] = searchString;
            ViewData["products"] = products;

            return View();
        }
    }
}
