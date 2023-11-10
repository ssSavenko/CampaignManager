using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    [Index(nameof(Priority), IsUnique = true)]
    public class Campaign
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public DateTime CampaignTime { get; set; }

        public int Priority { get; set; }
         
        public int TemplateId {  get; set; }
        [ForeignKey(nameof(TemplateId))]
        public Template TemplateData { get; set; }

        public ICollection<CampaignCondition> CampaignConditions { get; set; }
    }
}
