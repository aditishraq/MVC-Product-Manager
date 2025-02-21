using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MvcApp.Models
{
	public class ApplicationDbContext : DbContext
    {
        // Constructor that specifies the connection string name
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        // DbSet for products
        public DbSet<Product> Products { get; set; }
    }
}