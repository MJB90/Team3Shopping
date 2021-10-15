using Microsoft.AspNetCore.Mvc;
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
        //This action will bring u to the Cart page 
        public IActionResult ToCart()
        {
            
            return View();
        }
    }
}
