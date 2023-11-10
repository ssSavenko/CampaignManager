using CampaignManager.Models;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CampaignManager.Helpers
{
    public interface IFieldHelper
    {
        public Task<IList<FieldInfo>> GetCustomerFields();
    }

    public class FieldHelper : IFieldHelper
    {
        private CampaignManagerContext dbContext; 

        public FieldHelper(CampaignManagerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<FieldInfo>> GetCustomerFields()
        {
            IList<FieldInfo> result = new List<FieldInfo>()
            {
                new FieldInfo(){ FieldName=nameof(Customer.Age), ValueType = "int"},
                new FieldInfo(){ FieldName=nameof(Customer.Deposit), ValueType = "decimal"},
                new FieldInfo(){ FieldName=nameof(Customer.NewCustomer), ValueType = "bool"},
                new FieldInfo(){ FieldName=nameof(Customer.CityData), ValueType = "string", ResultList = dbContext.Cities.AsNoTracking().Select(x=> x.Name).ToList()},
                new FieldInfo(){ FieldName=nameof(Customer.GenderData), ValueType = "string", ResultList = dbContext.Gender.AsNoTracking().Select(x=> x.Name).ToList()},

            }; 
            return result;
        }
    }
}
