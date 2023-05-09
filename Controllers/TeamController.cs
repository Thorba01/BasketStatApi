using BasketStatsApi.Dtos.Team;
using BasketStatsApi.Models;
using BasketStatsApi.Services.TeamService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketStatsApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceTeamResponse<List<GetTeamDto>>>> GetTeams()
        {
            return Ok(await _teamService.GetAllTeams());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceTeamResponse<GetTeamDto>>> GetSingleTeam(int id)
        {
            return Ok(await _teamService.GetTeamById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceTeamResponse<List<GetTeamDto>>>> AddTeam(AddTeamDto newTeam)
        {
            return Ok(await _teamService.AddTeam(newTeam));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceTeamResponse<List<GetTeamDto>>>> UpdateTeam(UpdateTeamDto updatedTeam)
        {
            var response = await _teamService.UpdateTeam(updatedTeam);
            if (response.teams is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceTeamResponse<GetTeamDto>>> DeleteTeam(int id)
        {
            var response = await _teamService.DeleteTeam(id);
            if (response.teams is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
