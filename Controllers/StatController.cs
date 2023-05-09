using BasketStatsApi.Dtos.Stat;
using BasketStatsApi.Models;
using BasketStatsApi.Services.StatService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketStatsApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StatController : ControllerBase 
    {
        private readonly IStatService _statService;

        public StatController(IStatService statService)
        {
            _statService = statService;
        }

        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult<ServiceStatResponse<List<GetStatDto>>>> GetStats(int id)
        {
            return Ok(await _statService.GetAllStats(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceStatResponse<GetStatDto>>> GetSingleStat(int id)
        {
            return Ok(await _statService.GetStatsById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceStatResponse<List<GetStatDto>>>> AddStat(AddStatDto newStat)
        {
            return Ok(await _statService.AddStat(newStat));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceStatResponse<List<GetStatDto>>>> UpdateStat(UpdateStatDto updatedStat)
        {
            var response = await _statService.UpdateStat(updatedStat);
            if (response.stat is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceStatResponse<GetStatDto>>> DeleteStat(int id)
        {
            var response = await _statService.DeleteStat(id);
            if (response.stat is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
