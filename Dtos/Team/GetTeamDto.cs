using BasketStatsApi.Dtos.Player;

namespace BasketStatsApi.Dtos.Team
{
    public class GetTeamDto
    {
        public int id { get; set; }
        public string teamname { get; set; } = string.Empty;
        //public List<GetPlayerDto> Players { get; set; }

    }
}
