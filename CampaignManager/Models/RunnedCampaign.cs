using DB.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace CampaignManager.Models
{
    public class RunnedCampaign
    {
        public string TemplateName { get; set; }

        public DateTime Time {  get; set; }

        public JArray Customers { get; set; }

    }
}
