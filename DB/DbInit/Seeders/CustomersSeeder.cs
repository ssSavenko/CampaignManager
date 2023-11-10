using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbInit.Seeders
{
    internal class CustomersSeeder : ISeeder
    {

        private int cityId = 1;
        private int genderId = 1;

        private IDictionary<string, Gender> GenderDict = new Dictionary<string, Gender>();
        private IDictionary<string, City> CityDict = new Dictionary<string, City>();
        private ModelBuilder modelBuilder;
        public CustomersSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            IList<string> contents = File.ReadAllText(@"Resources/customers.csv").Split("\n");

            bool firstLine = true;
            foreach (string line in contents)
            {
                if (firstLine)
                {
                    firstLine = false;
                    continue;
                }
                ProcessRow(ParseRow(line));
            }
            InsertData();
        }

        private IList<string> ParseRow(string stringToParse)
        {
            IList<string> row;
            if (stringToParse != "")
                row = stringToParse.Split(',').ToList();
            else
                row = new List<string>();
            return row;
        }

        private void ProcessRow(IList<string> dataToSeed)
        {
            if (dataToSeed.Count != 0)
            {
                City city;
                Gender gender;

                if (GenderDict.ContainsKey(dataToSeed[2]))
                    gender = GenderDict[dataToSeed[2]];
                else
                {
                    gender = new Gender() { Id = genderId, Name = dataToSeed[2] };
                    GenderDict[dataToSeed[2]] = gender;
                    genderId++;
                }


                if (CityDict.ContainsKey(dataToSeed[3]))
                    city = CityDict[dataToSeed[3]];
                else
                {
                    city = new City() { Id = cityId, Name = dataToSeed[3] };
                    CityDict[dataToSeed[3]] = city;
                    cityId++;
                }

                Customer customer = new Customer()
                {
                    Id = int.Parse(dataToSeed[0]),
                    Age = int.Parse(dataToSeed[1]),
                    CityDataId = city.Id,
                    GenderDataId = gender.Id,
                    Deposit = decimal.Parse(dataToSeed[4]),
                    NewCustomer = (dataToSeed[5][0] == '1' ? true : false)
                };

                modelBuilder.Entity<Customer>().HasData(customer);
            }
        }

        private void InsertData()
        {
            foreach (var gender in GenderDict.Values)
            {
                modelBuilder.Entity<Gender>().HasData(gender);
            }
            foreach (var city in CityDict.Values)
            {
                modelBuilder.Entity<City>().HasData(city);
            }
        }
    }
}
