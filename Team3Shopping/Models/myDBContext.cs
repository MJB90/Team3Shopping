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
        {
            model.Entity<Cart>().HasKey(sc => new { sc.UserId, sc.ProductId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
