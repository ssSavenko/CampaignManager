using DB.DbInit;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 


namespace DB
{
    public class CampaignManagerContext : DbContext
    {
        public CampaignManagerContext()
        { 
        }
        public CampaignManagerContext(DbContextOptions<CampaignManagerContext> options) : base(options) { Database.EnsureCreated(); }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignCondition> CampaignConditions { get; set; } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Template> Template { get; set; }
    }
}
