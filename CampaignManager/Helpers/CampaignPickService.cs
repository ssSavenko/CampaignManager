using DB;
using DB.Models;
using DB.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CampaignManager.Helpers
{
    public interface ICampaignPickService
    {
        Task<IDictionary<Customer, Campaign>> PickCampaignsList(IList<Customer> customers, IList<Campaign>? campaigns = null);
        Task<Campaign> PickCampaign(JToken Customer, IList<Campaign>? Campaigns = null);
        Task<Campaign> PickCampaign(Customer Customer, IList<Campaign>? Campaigns = null);
    }

    public class CampaignPickService : ICampaignPickService
    {
        private Func<CampaignManagerContext> dbFactory;
        public CampaignPickService(Func<CampaignManagerContext> dbFactory)
        {
            this.dbFactory = dbFactory;
        }


        public async Task<IDictionary<Customer, Campaign>> PickCampaignsList(IList<Customer> customers, IList<Campaign>? campaigns = null)
        {
            IDictionary<Customer, Campaign> result = new Dictionary<Customer, Campaign>();
            Object resultSyncObject  = new Object();
            IList<Task> threads = new List<Task>();


            IList<Campaign> campaignsToSearch;
            if (campaigns == null || campaigns.Count == 0)
            {
                using (var dbContext = dbFactory())
                {
                    campaignsToSearch = dbContext.Campaigns.Include(x => x.CampaignConditions).Include(x => x.TemplateData).AsNoTracking().OrderBy(x => x.Priority).ToList();
                }
            }
            else
            {
                campaignsToSearch = campaigns;
            }
             
            foreach (var customer in customers)
            {
                threads.Add(
                    Task.Factory.StartNew(() =>
                    {
                        var campaign = PickCampaign(customer);

                        lock (resultSyncObject)
                        {
                            result[customer] = campaign.Result;
                        }
                    }
                ));

            }

            Task.WaitAll(threads.ToArray());

            return result;
        }

        public async Task<Campaign> PickCampaign(JToken customer, IList<Campaign>? campaigns = null)
        {
            Campaign result = null;
            IList<Campaign> campaignsToSearch;
            if (campaigns == null || campaigns.Count == 0)
            {
                using (var dbContext = dbFactory())
                {
                    campaignsToSearch = dbContext.Campaigns.Include(x=> x.CampaignConditions).Include(x=>x.TemplateData).AsNoTracking().OrderBy(x => x.Priority).ToList();
                }
            }
            else
            {
                campaignsToSearch = campaigns;
            }

            foreach (var campaign in campaignsToSearch)
            {
                bool fits = true;
                foreach (var condition in campaign.CampaignConditions)
                {
                    switch (condition.Condition)
                    {
                        case Condition.Equal:
                            if (!(customer[condition.FieldName]?.ToString() == condition.FieldValue))
                                fits = false;
                            break;
                        case Condition.NotEqual:
                            if (!(customer[condition.FieldName]?.ToString() != condition.FieldValue))
                                fits = false;
                            break;
                        case Condition.GreaterThan:
                            if (!(double.Parse(customer[condition.FieldName]?.ToString() ?? "0") > double.Parse(condition.FieldValue)))
                                fits = false;
                            break;
                        case Condition.GreaterThanOrEqual:
                            if (!(double.Parse(customer[condition.FieldName]?.ToString() ?? "0") >= double.Parse(condition.FieldValue)))
                                fits = false;
                            break;
                        case Condition.LessThan:
                            if (!(double.Parse(customer[condition.FieldName]?.ToString() ?? "0") < double.Parse(condition.FieldValue)))
                                fits = false;
                            break;
                        case Condition.LessThanOrEqual:
                            if (!(double.Parse(customer[condition.FieldName]?.ToString() ?? "0") <= double.Parse(condition.FieldValue)))
                                fits = false;
                            break;
                    }
                    if (fits == false)
                        break;
                }
                if (fits)
                {
                    result = campaign;
                    break;
                }
            }
            return result;
        }

        public async Task<Campaign> PickCampaign(Customer customer, IList<Campaign>? campaigns = null)
        {
            return await PickCampaign(JObject.FromObject(customer), campaigns);
        }
    }
}
