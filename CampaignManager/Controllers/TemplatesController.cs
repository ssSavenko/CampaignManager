using CampaignManager.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CampaignManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplatesController : ControllerBase
    { 
        private ITemplatesHelper templateHelper;
        public TemplatesController(ITemplatesHelper templateHelper)
        {
            this.templateHelper = templateHelper;
        }

        [HttpGet, Route("list")]
        public async Task<IActionResult> GetTemplates()
        {
            return Ok(await templateHelper.GetTemplates());
        }

    }
}
