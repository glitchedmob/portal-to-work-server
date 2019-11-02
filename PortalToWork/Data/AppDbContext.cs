using System;
using Microsoft.EntityFrameworkCore;
using PortalToWork.Models;
using PortalToWork.Models.H4G;

namespace PortalToWork.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new DbConfiguration();

            optionsBuilder.UseNpgsql(config.ConnectionString);
        }
    }
}