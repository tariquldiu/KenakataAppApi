using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace KenakataApp.Models
{
    public class DiscountDbContext: DbContext
    {
        public DiscountDbContext() : base("DiscountDbContext") { }
        public DbSet<AdminLogin> AdminLogin { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Shopkeeper> Shopkeeper { get; set; } 
    }
}