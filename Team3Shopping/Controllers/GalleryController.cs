using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team3Shopping.Models;
using System.Diagnostics;


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
            //get current User, currently set to null.
            User thisUser = dBContext.Users.FirstOrDefault(x => x.Id == "test@hotmail.com");

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

            int currCount = CountCartItems(thisUser);
            //int currCount = 0;

            //pushing the data into gallery view
            ViewData["searchString"] = searchString;
            ViewData["products"] = products;
            ViewData["cartCounter"] = currCount;

            Debug.WriteLine("1. returning view on landing");
            return View();
        }


        public IActionResult Product(Guid thisProductId) // --> localhost/Gallery/Product/{id?}
        {
            Product thisProduct = dBContext.Products.FirstOrDefault(x => x.Id == thisProductId);
            ViewData["thisProduct"] = thisProduct;
            //--- query product and send to View()
            return View();
        }

        public IActionResult AddToCart([FromBody] Product addProduct)
        {
            Debug.WriteLine("1. Reached AddToCart Action Method");

            //get current User, currently set to null.
            User thisUser = dBContext.Users.FirstOrDefault(x => x.Id == "test@hotmail.com");


            Guid addProductId = addProduct.Id; //extract the current product ID to be added
            //alternative: Guid addProductId = Guid.Parse(addProduct.Id) to parse into GUID
            Debug.WriteLine(addProductId);

            Product thisProduct = dBContext.Products.FirstOrDefault(x => x.Id == addProductId);// Query for product with given id


            if (thisProduct != null) //if product exists in Product inventory
            {
                Cart sameProductinCart = dBContext.Carts.FirstOrDefault(x
                    => x.ProductId == thisProduct.Id && x.UserId == thisUser.Id); //Query if same product is in cart

                if (sameProductinCart == null) //if product does not exist in cart, add in a new Cart row
                {
                    Debug.WriteLine("3. Adding new item");
                    Cart newCartItem = new Cart { AddToCartProductQuantity = 1 };
                    thisProduct.Cart.Add(newCartItem); //add new Cart row to the product's ICollection
                    thisUser.Cart.Add(newCartItem); //add new Cart row to the users's ICollection
                }
                else //if product exists, increment the AddToCartProductQuantity by 1
                {
                    Debug.WriteLine("3. Incrementing");
                    sameProductinCart.AddToCartProductQuantity += 1;
                }

                Debug.WriteLine("4. Saving Changes");
                dBContext.SaveChanges(); //persist all changes to DB
            }

            int currCount = CountCartItems(thisUser);

            Debug.WriteLine("6. GotCartItems");

            return Json(new { cartCounter = currCount }); //send Json object in AJAX response 
        }




        public int CountCartItems(User thisUser)
        {
            Debug.WriteLine("5. Counting Cart Items");
            List<Cart> allCartItems = dBContext.Carts.Where(x => x.UserId == thisUser.Id).ToList(); //now get all current Cart items
            int currCount = allCartItems.Sum(x => x.AddToCartProductQuantity); //sum all the Cart Products

            return currCount;
        }

    }
}
