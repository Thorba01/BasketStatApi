using BasketStatsApi.Dtos.Stat;
using BasketStatsApi.Models;

namespace BasketStatsApi.Services.StatService
{
    public interface IStatService
    {
        Task<ServiceStatResponse<List<GetStatDto>>> GetAllStats(int id);
        Task<ServiceStatResponse<GetStatDto>> GetStatsById(int id);
        Task<ServiceStatResponse<List<GetStatDto>>> AddStat(AddStatDto newStat);
        Task<ServiceStatResponse<GetStatDto>> UpdateStat(UpdateStatDto updatedStat);
        Task<ServiceStatResponse<List<GetStatDto>>> DeleteStat(int id);
    }
}
