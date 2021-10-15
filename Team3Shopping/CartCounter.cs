using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Team3Shopping.Models;

namespace Team3Shopping
{
    public class CartCounter
    {
        private readonly RequestDelegate next;

        public CartCounter(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, myDBContext dBContext)
        {
            
            string sessionId = context.Request.Cookies["SessionId"]; //retrieve the sessionId from the context (assuming already authenticated)
            
           int cartCount = 0;
            
            if (sessionId != null)
            {
                Session thisSession = dBContext.Sessions.FirstOrDefault(x => x.Id == Guid.Parse(sessionId)); //get the session object in DB from current sessionId
                User thisUser = dBContext.Users.FirstOrDefault(x => x.Id == thisSession.UserId); //get the user object in DB from current session object
                
                
                List <Cart> allCartItems = dBContext.Carts.Where(x => x.UserId == thisUser.Id).ToList(); //now get all current Cart items
            
                if (allCartItems.Count() > 0 )
                {
                    cartCount = allCartItems.Sum(x => x.AddToCartProductQuantity); //sum all the Cart Products
                }
            }

            context.Items.Add("cartCount", (int) cartCount);

            await next(context);
        }
    }
}
