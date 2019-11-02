using System;
using Microsoft.EntityFrameworkCore;
using PortalToWork.Models;

namespace PortalToWork.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new DbConfiguration();

            Console.WriteLine($"Connection String: {config.ConnectionString}");

            optionsBuilder.UseNpgsql(config.ConnectionString);
        }
    }
}