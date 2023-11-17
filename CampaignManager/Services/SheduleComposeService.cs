using CampaignManager.Models;
using DB;
using DB.Models;
using DB.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;

namespace CampaignManager.Services
{
    public interface ISheduleComposeService
    { 
        Task<IList<RunnedCampaignItem>> SheduleCustomerToCampaigns();
    }

    public class SheduleComposeService : ISheduleComposeService
    {
        private CampaignManagerContext dbContext;
        private IResultSaverService resultSaverService;
        private ICampaignPickService campaignPickService;

        public SheduleComposeService(CampaignManagerContext dbContext, IResultSaverService resultSaverService, ICampaignPickService campaignPickService)
        {
            this.dbContext = dbContext;
            this.resultSaverService = resultSaverService;
            this.campaignPickService = campaignPickService;
        }
         
        public async Task<IList<RunnedCampaignItem>> SheduleCustomerToCampaigns()
        { 
            var customers = dbContext.Customers.Include(x => x.GenderData).Include(x => x.CityData).AsNoTracking().ToList(); 

            var campaignRunDataList = await campaignPickService.PickCampaignsList(customers); 

            var result = campaignRunDataList.Select(x =>
            new RunnedCampaignItem()
            {
                Customer = JObject.FromObject(x.Key),
                TemplateName = x.Value.TemplateData.Name,
                Time = x.Value.CampaignTime,
            }).ToList();

            await resultSaverService.SaveToFile(JsonConvert.SerializeObject(result).ToString());
            return result;
        }
    }
}
