namespace BasketStatsApi.Models
{
    public class ServicePlayerResponse<T>
    {
        public T? players { get; set; }
        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
    }
}
