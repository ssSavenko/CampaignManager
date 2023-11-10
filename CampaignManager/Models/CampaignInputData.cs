namespace CampaignManager.Models
{
    public class CampaignInputData
    {
        public DateTime CampaignTime { get; set; }

        public int TemplateId { get; set; }
    }

    public class CampaignInputDataExtended : CampaignInputData
    {
        public int Id { get; set; }
        public int Priority { get; set; }

    }
}
