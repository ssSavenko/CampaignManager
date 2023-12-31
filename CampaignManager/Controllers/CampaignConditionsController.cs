﻿using CampaignManager.Services;
using CampaignManager.Models;
using DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampaignManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignConditionsController : ControllerBase
    {
        private ICampaignConditionsService campaignConditionsHelper;
        public CampaignConditionsController(ICampaignConditionsService campaignConditionsHelper)
        {
            this.campaignConditionsHelper = campaignConditionsHelper;
        }
         
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetCampaignCondition(int id)
        {
            return Ok(await campaignConditionsHelper.GetCampaignCondition(id));
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> AddCampaignCondition(CampaignConditionInputData campaignCondition)
        {
            return Ok(await campaignConditionsHelper.AddCampaignCondition(campaignCondition));
        } 

        [HttpPost, Route("remove")]
        public async Task<IActionResult> RemoveCampaignCondition(int campaignConditionId)
        {
            return Ok(await campaignConditionsHelper.RemoveCondition(campaignConditionId));
        }
    }
}
