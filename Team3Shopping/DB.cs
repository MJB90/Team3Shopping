using System;
using System.Collections.Generic;
using System.Linq;
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

        }

        public void SeedProducts()
        {

        }
    }
}
