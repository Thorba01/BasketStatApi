namespace BasketStatsApi.Models
{
    public class ServiceStatResponse<T>
    {
        public T? stat { get; set; }
        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
    }
}
