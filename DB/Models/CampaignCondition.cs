using DB.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class CampaignCondition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Condition Condition { get; set; }

        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public int CampaignId { get; set; }
    }
}
