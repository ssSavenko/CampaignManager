using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbInit.Seeders
{
    internal interface ISeeder
    {
        public void Seed();
    }
}
