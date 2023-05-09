namespace BasketStatsApi.Models
{
    public class Team
    {
        public int id { get; set; }
        public string teamname { get; set; } = string.Empty;
        public User User { get; set; }
        public List<Player>? Players { get; set; }

    }
}
