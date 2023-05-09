using BasketStatsApi.Dtos.Player;
using BasketStatsApi.Models;
using BasketStatsApi.Services.PlayerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketStatsApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase 
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult<ServicePlayerResponse<List<GetPlayerDto>>>> GetPlayers(int id)
        {
            return Ok(await _playerService.GetAllPlayer(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePlayerResponse<GetPlayerDto>>> GetSinglePlayer(int id)
        {
            return Ok(await _playerService.GetPlayerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServicePlayerResponse<List<GetPlayerDto>>>> AddPlayer(AddPlayerDto newPlayer)
        {
            return Ok(await _playerService.AddPlayer(newPlayer));
        }

        [HttpPut]
        public async Task<ActionResult<ServicePlayerResponse<List<GetPlayerDto>>>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            var response = await _playerService.UpdatePlayer(updatedPlayer);
            if (response.players is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServicePlayerResponse<GetPlayerDto>>> DeletePlayer(int id)
        {
            var response = await _playerService.DeletePlayer(id);
            if (response.players is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
