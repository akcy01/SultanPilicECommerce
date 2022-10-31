using Microsoft.EntityFrameworkCore;
using SultanPilic.Models;

namespace SultanPilic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       public DbSet<Category> Categories { get; set; }

    }
}
