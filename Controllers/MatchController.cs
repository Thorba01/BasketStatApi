using BasketStatsApi.Dtos.Match;
using BasketStatsApi.Models;
using BasketStatsApi.Services.MatchService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketStatsApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase 
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult<ServiceMatchResponse<List<GetMatchDto>>>> GetMatchs(int id)
        {
            return Ok(await _matchService.GetAllMatchs(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceMatchResponse<GetMatchDto>>> GetSingleMatch(int id)
        {
            return Ok(await _matchService.GetMatchById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceMatchResponse<List<GetMatchDto>>>> AddMatch(AddMatchDto newMatch)
        {
            return Ok(await _matchService.AddMatch(newMatch));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceMatchResponse<List<GetMatchDto>>>> UpdateMatch(UpdateMatchDto updatedMatch)
        {
            var response = await _matchService.UpdateMatch(updatedMatch);
            if (response.matchs is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceMatchResponse<GetMatchDto>>> DeleteMatch(int id)
        {
            var response = await _matchService.DeleteMatch(id);
            if (response.matchs is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
