using BasketStatsApi.Dtos.Team;
using BasketStatsApi.Models;

namespace BasketStatsApi.Services.TeamService
{
    public interface ITeamService
    {
        Task<ServiceTeamResponse<List<GetTeamDto>>> GetAllTeams();
        Task<ServiceTeamResponse<GetTeamDto>> GetTeamById(int id);
        Task<ServiceTeamResponse<List<GetTeamDto>>> AddTeam(AddTeamDto newTeam);
        Task<ServiceTeamResponse<GetTeamDto>> UpdateTeam(UpdateTeamDto updatedTeam);
        Task<ServiceTeamResponse<List<GetTeamDto>>> DeleteTeam(int id);

    }
}
