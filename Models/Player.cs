namespace BasketStatsApi.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Team Team { get; set; }
        public List<Match>? Matchs { get; set; }

    }
}
