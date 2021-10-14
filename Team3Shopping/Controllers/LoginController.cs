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
        public IActionResult Index()
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

            // no Session ID; show Login page
            return View();
        }
        public IActionResult Login(IFormCollection form)
        {
            string username = form["username"];
            string password = form["password"];


            using (SqlConnection conn = new SqlConnection("Server=localhost; Database=Team3Shoppingdb;Integrated Security=true"))
            {
                conn.Open();
                string query = @"SELECT COUNT(1)
                               FROM Users
                               WHERE Id=@username AND Password = @password";
                SqlCommand sqlCmd = new SqlCommand(query, conn);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                //if count == 1,user input is valid
                if (count == 1)
                {   //Create session to tag user
                    
                    //redirect to Manny Diego landing page
                    return RedirectToAction("Index", "Product");

                }
                else
                {
                    return RedirectToAction("Login");
                }
                
            }

          
        }
        /*public IActionResult MakeSession()
        {
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

            return RedirectToAction("ProductAction", "Product");
        }*/
    }
}
