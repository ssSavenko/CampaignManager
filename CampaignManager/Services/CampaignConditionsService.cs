using CampaignManager.Models;
using DB;
using DB.Models;
using DB.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CampaignManager.Services
{
    public interface ICampaignConditionsService
    {
        Task<CampaignCondition> AddCampaignCondition(CampaignConditionInputData campaignCondition);
        Task<List<CampaignCondition>> GetCampaignConditionsByCampaignId(int campaignId);
        Task<CampaignCondition> GetCampaignCondition(int conditionId);
        Task<bool> RemoveCondition(int conditionId); 
    }

    public class CampaignConditionsService : ICampaignConditionsService
    {
        private CampaignManagerContext dbContext;

        private ICampaignsService campaignHelper;
        public CampaignConditionsService(CampaignManagerContext dbContext, ICampaignsService campaignHelper)
        {
            this.dbContext = dbContext;
            this.campaignHelper = campaignHelper;
        }

        public async Task<CampaignCondition> AddCampaignCondition(CampaignConditionInputData campaignCondition)
        {
            CampaignCondition newItem = new CampaignCondition() { 
                Condition = campaignCondition.Condition,
                FieldName = campaignCondition.FieldName,
                FieldValue = campaignCondition.FieldValue,
                CampaignId = campaignCondition.CampaignId
            };


            dbContext.Add(newItem); 
            dbContext.SaveChanges();
            return newItem;
        }

        public async Task<List<CampaignCondition>> GetCampaignConditionsByCampaignId(int campaignId)
        {
            return (await campaignHelper.GetCampaign(campaignId)).CampaignConditions.ToList();
        }

        public async Task<CampaignCondition> GetCampaignCondition(int conditionId)
        {
            return dbContext.CampaignConditions.FirstOrDefault(x => x.Id == conditionId);
        }

        public async Task<bool> RemoveCondition(int conditionId)
        {
            bool result = false;

            var itemToDelete = dbContext.CampaignConditions.FirstOrDefault(x => x.Id == conditionId);
            if (itemToDelete != null)
            {
                dbContext.CampaignConditions.Remove(itemToDelete);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        } 
    }
}
