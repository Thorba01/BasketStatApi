using BasketStatsApi.Dtos.Match;
using BasketStatsApi.Models;

namespace BasketStatsApi.Services.MatchService
{
    public interface IMatchService
    {
        Task<ServiceMatchResponse<List<GetMatchDto>>> GetAllMatchs(int playerId);
        Task<ServiceMatchResponse<GetMatchDto>> GetMatchById(int id);
        Task<ServiceMatchResponse<List<GetMatchDto>>> AddMatch(AddMatchDto newMatch);
        Task<ServiceMatchResponse<GetMatchDto>> UpdateMatch(UpdateMatchDto updatedMatch);
        Task<ServiceMatchResponse<List<GetMatchDto>>> DeleteMatch(int id);
    }
}
