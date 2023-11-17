using CampaignManager.Services;
using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    { 
        private CampaignManagerContext dbContext;
        public CustomersController(CampaignManagerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet, Route("list")]
        public async Task<IActionResult> GetCampaigns()
        {
            return Ok(dbContext.Customers.Include(x=> x.CityData).Include(x => x.GenderData).ToList());
        }
    }
}
