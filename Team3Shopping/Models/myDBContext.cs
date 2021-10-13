using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Team3Shopping.Models
{
    public class myDBContext : DbContext
    {
        public myDBContext(DbContextOptions<myDBContext> options) : base (options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
