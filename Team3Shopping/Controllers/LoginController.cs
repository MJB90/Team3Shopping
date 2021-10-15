using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Team3Shopping.Models;

namespace Team3Shopping.Controllers
{
    public class LoginController : Controller
    {
        private myDBContext dBContext;

        public LoginController(myDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index(string username,string error_message)
        {
            if (Request.Cookies["SessionId"] != null)
            {
                Guid sessionId = Guid.Parse(Request.Cookies["sessionId"]);
                Session session = dBContext.Sessions.FirstOrDefault(x =>
                    x.Id == sessionId
                );

                if (session == null)
                {
                    // invalid Session ID; route to Logout
                    return RedirectToAction("Index", "Logout");
                }

                // valid Session ID; route to landing page "Action Method", "Controller"
                return RedirectToAction("Index", "Product");
            }

            if(username != null)
            {
                ViewData["username"] = username;
                ViewData["error_message"] = error_message;
            }
            // no Session ID; show Login page
            return View();
        }
        public IActionResult Login(IFormCollection form)
        {
            string username = form["username"];
            string password = form["password"];

            User user = dBContext.Users.FirstOrDefault(x =>
               x.Id == username &&
               x.Password == password);

            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { username = username , error_message = "Please enter valid email/password" } );
            }

            // create a new session and tag to user
            Session session = new Session()
            {
                
                User = user
            };
            dBContext.Sessions.Add(session);
            dBContext.SaveChanges();
            

            // ask browser to save and send back these cookies next time
            Response.Cookies.Append("SessionId", session.Id.ToString());
            Response.Cookies.Append("Username", user.Id);
            return RedirectToAction("Index", "Product");





        }


    }
    }
