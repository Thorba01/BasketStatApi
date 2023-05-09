using BasketStatsApi.Dtos.Player;
using BasketStatsApi.Dtos.Stat;

namespace BasketStatsApi.Dtos.Match
{
    public class GetMatchDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Opponent { get; set; } = string.Empty;
        //public GetPlayerDto Player { get; set; }
        //public GetStatDto Stat { get; set; }

    }
}
