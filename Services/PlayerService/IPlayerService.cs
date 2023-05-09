using BasketStatsApi.Dtos.Player;
using BasketStatsApi.Models;

namespace BasketStatsApi.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServicePlayerResponse<List<GetPlayerDto>>> GetAllPlayer(int id);
        Task<ServicePlayerResponse<GetPlayerDto>> GetPlayerById(int id);
        Task<ServicePlayerResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer);
        Task<ServicePlayerResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer);
        Task<ServicePlayerResponse<List<GetPlayerDto>>> DeletePlayer(int id);

    }
}
