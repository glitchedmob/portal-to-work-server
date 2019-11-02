using Microsoft.EntityFrameworkCore;

namespace PortalToWork.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new DbConfiguration();
        }
    }
}