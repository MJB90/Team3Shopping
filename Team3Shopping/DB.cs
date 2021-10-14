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
        private myDBContext dBContext;
        public DB(myDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Seed()
        {
            SeedUsers();
            SeedProducts();
        }

        public void SeedUsers()
        {
           
            dBContext.Add(new User
            {
                Id = "test@hotmail.com",
                Password = "password",
                FirstName = "testfirstname",
                LastName = "testlastname"
            });
            dBContext.Add(new User
            {
                Id = "tcw38@hotmail.com",
                Password = "password",
                FirstName = "Cher Wah",
                LastName = "Tan"
            });
            dBContext.Add(new User
            {
                Id = "tinandcopper55@hotmail.com",
                Password = "password",
                FirstName = "Tri Tin",
                LastName = "Nguyen"
            });
          
            dBContext.Add(new User
            {
                Id = "shuern.chua@gmail.com",
                Password = "password",
                FirstName = "Shu Ern",
                LastName = "Chua"
            });
            dBContext.Add(new User
            {
                Id = "diego@yahoo.com",
                Password = "password",
                FirstName = "Diego",
                LastName = "Alpin"
            });

            dBContext.SaveChanges();
        }

        public void SeedProducts()
        {

        }
    }
}
