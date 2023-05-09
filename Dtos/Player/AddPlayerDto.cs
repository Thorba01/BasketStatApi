using BasketStatsApi.Models;

namespace BasketStatsApi.Dtos.Player
{
    public class AddPlayerDto
    {
        public string Name { get; set; } = string.Empty;
        public int TeamId { get; set; }

    }
}
