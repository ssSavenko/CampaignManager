using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
         
        public int Age { get; set; }

        public int GenderDataId { get; set; }
        [ForeignKey("GenderDataId")]
        public Gender GenderData { get; set; }

        public int CityDataId { get; set; }
        [ForeignKey("CityDataId")]
        public City CityData { get; set; }

        public decimal Deposit {  get; set; }

        public bool NewCustomer {  get; set; } 
    }
}
