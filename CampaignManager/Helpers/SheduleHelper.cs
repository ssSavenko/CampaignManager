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

namespace CampaignManager.Helpers
{
    public interface ISheduleHelper
    {
        Task<IList<RunnedCampaign>> SheduleCampaigns(); 
    }

    public class SheduleHelper : ISheduleHelper
    {
        private CampaignManagerContext dbContext;

        public SheduleHelper(CampaignManagerContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IList<RunnedCampaign>> SheduleCampaigns()
        {
            IList<Campaign> campaigns = dbContext.Campaigns.Include(x => x.TemplateData).Include(x => x.CampaignConditions).OrderBy(x => x.Priority).ToList();
            IList<RunnedCampaign> result = new List<RunnedCampaign>();
             

            JArray customers = JArray.FromObject(dbContext.Customers.Include(x => x.GenderData).Include(x => x.CityData).ToList());

            foreach (var campaign in campaigns)
            {
                if (customers.Count == 0)
                    break;

                var newCampaign = await SelectUsersForCampaign(customers, campaign);
                result.Add(newCampaign);
                var idsToExclude = newCampaign.Customers.Select(x => x[nameof(Customer.Id)]);

                customers = JArray.FromObject(customers.Where(x => !idsToExclude.Contains(x[nameof(Customer.Id)])));
            }

            //Saves Data To File 


            File.WriteAllText("ResultSchedule.json", JsonConvert.SerializeObject(result), Encoding.UTF8);

            //
            return result;
        } 


        private async Task<RunnedCampaign> SelectUsersForCampaign(JArray customers, Campaign campaign)
        {
            RunnedCampaign result = new RunnedCampaign()
            {
                TemplateName = campaign.TemplateData.Name,
                Time = campaign.CampaignTime,
            };

            JArray selection = JArray.FromObject(customers);

            foreach (var condition in campaign.CampaignConditions)
            {
                switch (condition.Condition)
                {
                    case Condition.Equal:
                        selection = JArray.FromObject(selection.Where(x => x[condition.FieldName]?.ToString() == condition.FieldValue));
                        break;
                    case Condition.NotEqual:
                        selection = JArray.FromObject(selection.Where(x => x[condition.FieldName]?.ToString() != condition.FieldValue));
                        break;
                    case Condition.GreaterThan:
                        selection = JArray.FromObject(selection.Where(x => double.Parse(x[condition.FieldName]?.ToString()) > double.Parse(condition.FieldValue)));
                        break;
                    case Condition.GreaterThanOrEqual:
                        selection = JArray.FromObject(selection.Where(x => double.Parse(x[condition.FieldName]?.ToString()) >= double.Parse(condition.FieldValue)));
                        break;
                    case Condition.LessThan:
                        selection = JArray.FromObject(selection.Where(x => double.Parse(x[condition.FieldName]?.ToString()) < double.Parse(condition.FieldValue)));
                        break;
                    case Condition.LessThanOrEqual:
                        selection = JArray.FromObject(selection.Where(x => double.Parse(x[condition.FieldName]?.ToString()) <= double.Parse(condition.FieldValue)));
                        break;
                }

            }
            result.Customers = selection;

            return result;
        }
    }
}
