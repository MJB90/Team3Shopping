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
            SeedCart();
            SeedProductReviews();
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
            });

            dbContext.Add(new Product
            {
                ProductName = "Microsoft Office 360",
                ProductDesc = "Including Word, Excel, PowerPoint and etc.",
                ProductPrice = 300,
                ProductImage = "https://upload.wikimedia.org/wikipedia/commons/5/5f/Microsoft_Office_logo_%282019%E2%80%93present%29.svg?fbclid=IwAR28G4cNfQwhzu2y6bo5XVelgOQwvm0KHisUwJz3tVWdYDH2HE6pRG1XE4s",
                ProductCategory = "Software",
            });

            dbContext.Add(new Product
            {
                ProductName = "Zoom",
                ProductDesc = "Zoom's secure, reliable video platform powers all of your communication needs.",
                ProductPrice = 30,
                ProductImage = "https://chromeunboxed.com/wp-content/uploads/2020/12/zoom-feature-december-2020.png?ezimgfmt=ng%3Awebp%2Fngcb53%2Frs%3Adevice%2Frscb53-1&fbclid=IwAR18JrhDn3rf1ttZjY9bvd5ZvZ0oJ1ALxpkEzv7tKZ-lf_RCn2MAuwoJtY8",
                ProductCategory = "Software",
            });

            dbContext.Add(new Product
            {
                ProductName = "Overcooked 2",
                ProductDesc = "A cooperative cooking simulation video game.",
                ProductPrice = 20,
                ProductImage = "https://cdn.akamai.steamstatic.com/steam/apps/728880/header.jpg?t=1608812250&fbclid=IwAR2cgPgHeCl3Ac96j5LhiWTfkDloYEwmRSiayjBAplXLznqGea0OPcr95OI",
                ProductCategory = "Software",
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



            Cart newCartItem2 = new Cart { AddToCartProductQuantity = 2 };

            Product thisProduct2 = dbContext.Products.FirstOrDefault(x => x.ProductName == "Zoom");
            User thisUser2 = dbContext.Users.FirstOrDefault(x => x.Id == "test@hotmail.com");

            thisProduct2.Cart.Add(newCartItem2); //add new Cart row to the product's ICollection
            thisUser2.Cart.Add(newCartItem2); //add new Cart row to the users's ICollection

            dbContext.SaveChanges();
        }

        public void SeedProductReviews()
        {

            User thisUser = dbContext.Users.FirstOrDefault(x => x.Id == "test@hotmail.com");
            User thisUser2 = dbContext.Users.FirstOrDefault(x => x.Id == "shuern.chua@gmail.com");
            User thisUser3 = dbContext.Users.FirstOrDefault(x => x.Id == "diego@yahoo.com");

            Product thisProduct = dbContext.Products.FirstOrDefault(x => x.ProductName == "Microsoft Visual Studio");
            Product thisProduct2 = dbContext.Products.FirstOrDefault(x => x.ProductName == "Zoom");
            Product thisProduct3 = dbContext.Products.FirstOrDefault(x => x.ProductName == "Overcooked 2");


            //Seed all VS reviews

            ProductReview review_VS01 = new ProductReview
            {
                ReviewText = "VS is a great coding platform! Will buy again",
                Rating = 4,
                Timestamp = new DateTime(2021, 01, 23).ToString("dd MMM yyyy")
            };

            ProductReview review_VS02 = new ProductReview
            {
                ReviewText = "Visual Studio is perfect for my programming classes",
                Rating = 4,
                Timestamp = new DateTime(2021, 02, 21).ToString("dd MMM yyyy")
            };

            ProductReview review_VS03 = new ProductReview
            {
                ReviewText = "Functionalities in Microsoft VS is excellent for work",
                Rating = 5,
                Timestamp = new DateTime(2021, 03, 10).ToString("dd MMM yyyy")
            };

            //user 1 review
            thisProduct.ProductReview.Add(review_VS01); 
            thisUser.ProductReview.Add(review_VS01);            
            //user 2 review
            thisProduct.ProductReview.Add(review_VS02); 
            thisUser2.ProductReview.Add(review_VS02); 
            //user 3 review
            thisProduct.ProductReview.Add(review_VS03); 
            thisUser3.ProductReview.Add(review_VS03);

            //Seed all Zoom reviews

            ProductReview review_Zoom01 = new ProductReview
            {
                ReviewText = "Satisfied with Zoom for my online work meetings",
                Rating = 4,
                Timestamp = new DateTime(2021, 01, 14).ToString("dd MMM yyyy")
            };

            ProductReview review_Zoom02 = new ProductReview
            {
                ReviewText = "I have faced issues with screen sharing. It turns blank at times, and sounds cuts off",
                Rating = 2,
                Timestamp = new DateTime(2020, 12, 01).ToString("dd MMM yyyy")
            };

            thisProduct2.ProductReview.Add(review_Zoom01); //add new Cart row to the product's ICollection
            thisUser2.ProductReview.Add(review_Zoom01); //add new Cart row to the users's ICollection

            thisProduct2.ProductReview.Add(review_Zoom02); //add new Cart row to the product's ICollection
            thisUser3.ProductReview.Add(review_Zoom02); //add new Cart row to the users's ICollection


            // Seed 1 Overcooked review
            ProductReview review_OCooked01 = new ProductReview
            {
                ReviewText = "Super fun game. Instantly hooked!!",
                Rating = 5,
                Timestamp = new DateTime(2020, 12, 25).ToString("dd MMM yyyy")
            };

            thisProduct3.ProductReview.Add(review_OCooked01); //add new Cart row to the product's ICollection
            thisUser3.ProductReview.Add(review_OCooked01); //add new Cart row to the users's ICollection


            dbContext.SaveChanges();
        }


    }
}
