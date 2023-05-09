namespace BasketStatsApi.Models
{
    public class ServiceMatchResponse<T>
    {
        public T? matchs { get; set; }
        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
    }
}
