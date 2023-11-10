using CampaignManager.Models;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CampaignManager.Helpers
{

    public interface ICampaignsHelper
    {
        public Task<Campaign> AddCampaign(CampaignInputData campaign);
        public Task<Campaign> GetCampaign(int id);
        public Task<IList<Campaign>> GetCampaigns();
        public Task<bool> RemoveCampaign(int id);
        public Task<bool> UpdateCampaign(CampaignInputDataExtended campaign);
    }

    public class CampaignsHelper : ICampaignsHelper
    {
        private CampaignManagerContext dbContext;
        public CampaignsHelper(CampaignManagerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Campaign> AddCampaign(CampaignInputData campaign)
        {

            int priority;
            try
            {
                priority = dbContext.Campaigns.Max(x => x.Priority) + 1;
            }
            catch
            {
                priority = 0;
            }
            Campaign newItem = new Campaign()
            {
                TemplateData = dbContext.Template.FirstOrDefault(x => x.Id == campaign.TemplateId),
                CampaignTime = new DateTime(2000, 1, 1, campaign.CampaignTime.Hour, campaign.CampaignTime.Minute, 0),
                Priority = priority
            };
            dbContext.Campaigns.Add(newItem);
            dbContext.SaveChanges();
            return newItem;
        }

        public async Task<Campaign> GetCampaign(int campaignId)
        {
            return dbContext.Campaigns.Include(x => x.CampaignConditions).Include(x=> x.TemplateData).FirstOrDefault(x => x.Id == campaignId);
        }

        public async Task<IList<Campaign>> GetCampaigns()
        {
            return dbContext.Campaigns.Include(x => x.CampaignConditions).Include(x => x.TemplateData).AsNoTracking().ToList();
        }

        public async Task<bool> RemoveCampaign(int campaignId)
        {
            bool result = false;
            var itemToDelete = dbContext.Campaigns.FirstOrDefault(x => x.Id == campaignId);
            if (itemToDelete != null)
            {
                var itemsToUpdate = dbContext.Campaigns.Where(x => x.Priority > itemToDelete.Priority);
                dbContext.Campaigns.Remove(itemToDelete);
                await MovePriority(itemsToUpdate);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public async Task<bool> UpdateCampaign(CampaignInputDataExtended campaign)
        {
            bool result = false;
            var itemToUpdate = dbContext.Campaigns.FirstOrDefault(x => x.Id == campaign.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.TemplateId = campaign.TemplateId;
                itemToUpdate.CampaignTime = campaign.CampaignTime;

                itemToUpdate.CampaignTime = new DateTime(2000, 1, 1, itemToUpdate.CampaignTime.Hour, itemToUpdate.CampaignTime.Minute, 0);

                if (itemToUpdate.Priority != campaign.Priority)
                {
                    var maxPriority = dbContext.Campaigns.Max(x => x.Priority);
                     

                    if (itemToUpdate.Priority > campaign.Priority)
                        await MovePriority(dbContext.Campaigns.Where(x => x.Priority < itemToUpdate.Priority && x.Priority >= campaign.Priority), false);
                    else
                        await MovePriority(dbContext.Campaigns.Where(x => x.Priority > itemToUpdate.Priority && x.Priority <= campaign.Priority));

                    itemToUpdate.Priority = -1; 
                    dbContext.SaveChanges();

                    if (campaign.Priority > maxPriority)
                        itemToUpdate.Priority = maxPriority;
                    else
                        itemToUpdate.Priority = campaign.Priority; 
                } 
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        //moves priority up if increasePriority == true, down in other cases
        private async Task MovePriority(IEnumerable<Campaign> listToMove, bool increasePriority = true)
        {
            foreach (var item in listToMove)
            {
                if (increasePriority)
                    item.Priority -= 1;
                else
                    item.Priority += 1;
                 
            } 
        }
    }
}
