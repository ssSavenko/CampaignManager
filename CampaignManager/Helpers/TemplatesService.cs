using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CampaignManager.Helpers
{
    public interface ITemplatesService
    {
        Task<IList<Template>> GetTemplates();
    } 

    public class TemplatesService : ITemplatesService
    {
        private CampaignManagerContext dbContext;
        public TemplatesService(CampaignManagerContext dbContext)
        {
            this.dbContext = dbContext;
        } 

        public async Task<IList<Template>> GetTemplates()
        {
            return dbContext.Template.AsNoTracking().ToList();
        }
    }
}
