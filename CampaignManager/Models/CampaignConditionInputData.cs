using DB.Models.Enums;

namespace CampaignManager.Models
{
    public class CampaignConditionInputData
    {
        public int CampaignId { get; set; }

        public Condition Condition { get; set; }

        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }
}
