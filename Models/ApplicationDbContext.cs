using System.Data.Entity;

namespace MvcApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor specifying the connection string name.
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        // DbSet for your application entities, marked as virtual for testing.
        public virtual DbSet<Product> Products { get; set; }
    }
}
