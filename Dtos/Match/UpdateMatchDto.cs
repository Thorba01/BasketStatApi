namespace BasketStatsApi.Dtos.Match
{
    public class UpdateMatchDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Opponent { get; set; } = string.Empty;
    }
}
