using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbInit.Seeders
{
    internal class TemplateSeeder : ISeeder
    {
        private int templateId = 1;
        private ModelBuilder modelBuilder;
        private const string dir = @"Resources/Templates/";
        public TemplateSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            IList<string> list = GetTemplates();
            InsertData(ProcessData(list));
        }

        private IList<string> GetTemplates()
        {
            return Directory.GetFiles(dir).ToList();
        }

        private IList<Template> ProcessData(IList<string> data) 
        { 
            IList<Template> templates = new List<Template>();

            foreach (string item in data) { 
                string name = item.Split('.')[0].Split('/').Last();
                templates.Add(new Template() { Id = templateId, Name = name, Url = dir + item });
                templateId++;
            }

            return templates;
        }

        private void InsertData(IList<Template> templates)
        {
            foreach (Template template in templates)
            {
                modelBuilder.Entity<Template>().HasData(template);
            }
        }
    }
}
