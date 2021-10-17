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

        //--- manas code
        private Utility utility;

        public GalleryController(myDBContext dBContext)
        {
            this.dBContext = dBContext;

            //--- manas code
            utility = new Utility(dBContext);
        }

        public IActionResult Index(string searchString)
        {
            //--- manas code
            Session thisSession = utility.GetSession(Request);
            if (thisSession == null)
            {
                return RedirectToAction("Index", "Login");
            }


            if (searchString == null) { searchString = ""; }

            User thisUser = dBContext.Users.FirstOrDefault(x => x.Id == thisSession.UserId); //get the user object in DB from current session object


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
            //ViewData["cartCounter"] = currCount;

            Debug.WriteLine("1. returning view on landing");
            return View();
        }


        public IActionResult Product(string id) // --> localhost/Gallery/Product/{id?}
        {
            Session session = utility.GetSession(Request);
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }


            Product thisProduct = dBContext.Products.FirstOrDefault(x => x.ProductName == id); //get this product info via its object

            List<ProductReview> reviewList = dBContext.ProductReviews.Where(x => x.ProductId == thisProduct.Id).ToList(); //get list of reviews

            int reviewCount = reviewList.Count(); //get number of reviews
            double avgRatings = 0;
            if (reviewCount > 0)
                avgRatings = reviewList.Average(x => x.Rating); //get average ratings


            ViewData["reviewList"] = reviewList;
            ViewData["reviewCount"] = reviewCount;
            ViewData["avgRatings"] = Math.Round(avgRatings,1);
            ViewData["thisProduct"] = thisProduct;

            return View();
        }

        public IActionResult AddToCart([FromBody] Product addProduct)
        {
            string sessionId = HttpContext.Request.Cookies["SessionId"]; 
            
            
            //retrieve the sessionId from the context (assuming already authenticated)

            //get current User, currently set to null.
            Session thisSession = dBContext.Sessions.FirstOrDefault(x => x.Id == Guid.Parse(sessionId)); //get the session object in DB from current sessionId
            User thisUser = dBContext.Users.FirstOrDefault(x => x.Id == thisSession.UserId); //get the user object in DB from current session object



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
