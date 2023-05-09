namespace BasketStatsApi.Dtos.Stat
{
    public class AddStatDto
    {
        public int Totalpoints { get; set; } = 0;
        public int Offrebound { get; set; } = 0;
        public int Defrebound { get; set; } = 0;
        public int Assists { get; set; } = 0;
        public int Interceptions { get; set; } = 0;
        public int Looseball { get; set; } = 0;
        public int Blocks { get; set; } = 0;
        public int Onepointmade { get; set; } = 0;
        public int Onepointmiss { get; set; } = 0;
        public int Twopointmade { get; set; } = 0;
        public int Twopointmiss { get; set; } = 0;
        public int Threepointmade { get; set; } = 0;
        public int Threepointmiss { get; set; } = 0;
        public int Offfalt { get; set; } = 0;
        public int Deffalt { get; set; } = 0;
        public int Technicalfalt { get; set; } = 0;
        public int Minplayed { get; set; } = 0;
        public string Homeaway { get; set; } = string.Empty;
        public int MatchId { get; set; }
    }
}
