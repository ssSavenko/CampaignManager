using Newtonsoft.Json.Linq;

namespace CampaignManager.Models
{
    public class RunnedCampaignItem
    { 
        public string? TemplateName { get; set; }

        public DateTime Time { get; set; }

        public JObject Customer { get; set; }
    }
}
