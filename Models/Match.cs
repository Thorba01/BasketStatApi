namespace BasketStatsApi.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Opponent { get; set; } = string.Empty;
        public Player Player { get; set; }
        public Stat? Stat { get; set; }

    }
}
