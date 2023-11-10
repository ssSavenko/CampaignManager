using DB.DbInit.Seeders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbInit
{
    public class DbInitializer
    { 
        private readonly ModelBuilder modelBuilder;
        private CustomersSeeder customersSeeder;
        private TemplateSeeder templateSeeder;


        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
            customersSeeder = new CustomersSeeder(modelBuilder);
            templateSeeder = new TemplateSeeder(modelBuilder);
        }

        public void Seed()
        {
            customersSeeder.Seed();
            templateSeeder.Seed();
        }
    }
}
