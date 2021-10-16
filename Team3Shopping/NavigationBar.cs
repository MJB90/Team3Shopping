using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Team3Shopping.Models;

namespace Team3Shopping
{
    public class NavigationBar
    {
        private readonly RequestDelegate next;

        public NavigationBar(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, myDBContext dBContext)
        {

            Utility thisUtility = new Utility(dBContext);
            Session thisSession = thisUtility.GetSession(context.Request);

            if (thisSession != null)
            {
                User thisUser = dBContext.Users.FirstOrDefault(x => x.Id == thisSession.UserId); //get the user object in DB from current session object
                context.Items.Add("thisUser", (User) thisUser); // give this to _layout to display name


                List<Cart> allCartItems = dBContext.Carts.Where(x => x.UserId == thisUser.Id).ToList(); //now get all current Cart items
                
                int cartCount = 0; //set as zero for default
                if (allCartItems.Count() > 0 ) //if there are unique Products in the cart for thisUser (e.g. 2 seats of Zoom is considered 1 unique Product)
                {
                    cartCount = allCartItems.Sum(x => x.AddToCartProductQuantity); //sum all the seats of all Cart products
                    

                }
                context.Items.Add("cartCount", (int) cartCount);//give this to _layout.cs to display cart (0 if no cart items, >=1 otherwise)

            }


            await next(context);
        }
    }
}
