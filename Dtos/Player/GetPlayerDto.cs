using BasketStatsApi.Dtos.Stat;
using BasketStatsApi.Dtos.Team;

namespace BasketStatsApi.Dtos.Player
{
    public class GetPlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public List<GetStatDto> Stats { get; set; }

    }
}
