using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Team3Shopping.Models;

namespace Team3Shopping
{
    public class DB
    {
        private myDBContext dbContext;
        public DB(myDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            SeedUsers();
            SeedProducts();
        }

        public void SeedUsers()
        {
           
            dbContext.Add(new User
            {
                Id = "test@hotmail.com",
                Password = "password",
                FirstName = "testfirstname",
                LastName = "testlastname"
            });
            dbContext.Add(new User
            {
                Id = "tcw38@hotmail.com",
                Password = "password",
                FirstName = "Cher Wah",
                LastName = "Tan"
            });
            dbContext.Add(new User
            {
                Id = "tinandcopper55@hotmail.com",
                Password = "password",
                FirstName = "Tri Tin",
                LastName = "Nguyen"
            });
          
            dbContext.Add(new User
            {
                Id = "shuern.chua@gmail.com",
                Password = "password",
                FirstName = "Shu Ern",
                LastName = "Chua"
            });
            dbContext.Add(new User
            {
                Id = "diego@yahoo.com",
                Password = "password",
                FirstName = "Diego",
                LastName = "Alpin"
            });

            dbContext.SaveChanges();
        }
        //Feel free to change the products, I just added this dummy products to test my code out
        public void SeedProducts()
        {
            dbContext.Add(new Product
            {
                ProductName = "Microsoft Visual Studio",
                ProductDesc = "An IDE used to develop computer programs, as well as websites, web apps, web services and mobile apps.",
                ProductPrice = 200,
                ProductImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Visual_Studio_Icon_2019.svg/1920px-Visual_Studio_Icon_2019.svg.png?fbclid=IwAR0S3eTMNDMYfnYsAe24caF7o-ctj4kgC4YMku9yo-KDEWe0pS6mnzDEw50",
                ProductCategory = "Software",
                ProductQuantity = 100
            });

            dbContext.Add(new Product
            {
                ProductName = "Microsoft Office 360",
                ProductDesc = "Including Word, Excel, PowerPoint and etc.",
                ProductPrice = 300,
                ProductImage = "https://upload.wikimedia.org/wikipedia/commons/5/5f/Microsoft_Office_logo_%282019%E2%80%93present%29.svg?fbclid=IwAR28G4cNfQwhzu2y6bo5XVelgOQwvm0KHisUwJz3tVWdYDH2HE6pRG1XE4s",
                ProductCategory = "Software",
                ProductQuantity = 100
            });

            dbContext.Add(new Product
            {
                ProductName = "Zoom",
                ProductDesc = "Zoom's secure, reliable video platform powers all of your communication needs.",
                ProductPrice = 30,
                ProductImage = "https://chromeunboxed.com/wp-content/uploads/2020/12/zoom-feature-december-2020.png?ezimgfmt=ng%3Awebp%2Fngcb53%2Frs%3Adevice%2Frscb53-1&fbclid=IwAR18JrhDn3rf1ttZjY9bvd5ZvZ0oJ1ALxpkEzv7tKZ-lf_RCn2MAuwoJtY8",
                ProductCategory = "Software",
                ProductQuantity = 100
            });

            dbContext.Add(new Product
            {
                ProductName = "Overcooked 2",
                ProductDesc = "A cooperative cooking simulation video game.",
                ProductPrice = 20,
                ProductImage = "https://cdn.akamai.steamstatic.com/steam/apps/728880/header.jpg?t=1608812250&fbclid=IwAR2cgPgHeCl3Ac96j5LhiWTfkDloYEwmRSiayjBAplXLznqGea0OPcr95OI",
                ProductCategory = "Software",
                ProductQuantity = 100
            });
            dbContext.SaveChanges();
        }

        //seeding cart items to test my page
        public void SeedCart()
        {
            
            Cart newCartItem = new Cart { AddToCartProductQuantity = 1 };

            Product thisProduct = dbContext.Products.FirstOrDefault(x => x.ProductName == "Microsoft Visual Studio");
            User thisUser = dbContext.Users.FirstOrDefault(x => x.Id == "test@hotmail.com");

            thisProduct.Cart.Add(newCartItem); //add new Cart row to the product's ICollection
            thisUser.Cart.Add(newCartItem); //add new Cart row to the users's ICollection



            Cart newCartItem = new Cart { AddToCartProductQuantity = 2 };

            Product thisProduct = dbContext.Products.FirstOrDefault(x => x.ProductName == "Zoom");
            User thisUser = dbContext.Users.FirstOrDefault(x => x.Id == "test@hotmail.com");

            thisProduct.Cart.Add(newCartItem); //add new Cart row to the product's ICollection
            thisUser.Cart.Add(newCartItem); //add new Cart row to the users's ICollection

            dbContext.SaveChanges();
        }

    }
}
