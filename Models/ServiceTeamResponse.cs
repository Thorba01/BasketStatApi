namespace BasketStatsApi.Models
{
    public class ServiceTeamResponse<T>
    {
        public T? teams { get; set; }
        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
    }
}
