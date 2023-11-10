using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CampaignManager.Helpers
{
    public interface ITemplatesHelper
    {
        Task<IList<Template>> GetTemplates();
    } 

    public class TemplatesHelper : ITemplatesHelper
    {
        private CampaignManagerContext dbContext;
        public TemplatesHelper(CampaignManagerContext dbContext)
        {
            this.dbContext = dbContext;
        } 

        public async Task<IList<Template>> GetTemplates()
        {
            return dbContext.Template.AsNoTracking().ToList();
        }
    }
}
