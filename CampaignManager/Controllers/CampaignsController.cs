using CampaignManager.Services;
using CampaignManager.Models;
using DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace CampaignManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignsController : ControllerBase
    {
        private ICampaignsService campaignHelper;
        private ICampaignConditionsService campaignConditionsHelper;
        private ISheduleComposeService sheduleHelper;
        public CampaignsController(ICampaignsService campaignHelper, ICampaignConditionsService campaignConditionsHelper, ISheduleComposeService sheduleHelper)
        {
            this.campaignHelper = campaignHelper;
            this.campaignConditionsHelper = campaignConditionsHelper;
            this.sheduleHelper = sheduleHelper;
        }

        [HttpGet, Route("list")]
        public async Task<IActionResult> GetCampaigns()
        {
            return Ok(await campaignHelper.GetCampaigns());
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetCampaign(int id)
        {
            return Ok(await campaignHelper.GetCampaign(id));
        }

        [HttpGet, Route("conditions/{id}")]
        public async Task<IActionResult> GetCampaignConditions(int id)
        {
            return Ok(await campaignConditionsHelper.GetCampaignConditionsByCampaignId(id));
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> AddCampaign(CampaignInputData campaign)
        { 
            return Ok(await campaignHelper.AddCampaign(campaign));
        }


        [HttpPost, Route("update")]
        public async Task<IActionResult> UpdateCampaign(CampaignInputDataExtended campaign)
        { 
            return Ok(await campaignHelper.UpdateCampaign(campaign));
        }


        [HttpPost, Route("remove")]
        public async Task<IActionResult> RemoveCampaign(int id)
        { 
            return Ok(await campaignHelper.RemoveCampaign(id));
        }


        [HttpGet, Route("schedule")]
        public async Task<IActionResult> GetScheduledCampaigns()
        {
            var schedule = (await sheduleHelper.SheduleCustomerToCampaigns()); 
            return Ok(JsonConvert.SerializeObject(schedule));
        }

    }
}
