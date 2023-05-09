namespace BasketStatsApi.Dtos.Match
{
    public class AddMatchDto
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public string Opponent { get; set; } = string.Empty;
        public int PlayerId { get; set; }

    }
}
