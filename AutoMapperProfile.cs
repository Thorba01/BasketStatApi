using AutoMapper;
using BasketStatsApi.Dtos.Match;
using BasketStatsApi.Dtos.Player;
using BasketStatsApi.Dtos.Stat;
using BasketStatsApi.Dtos.Team;
using BasketStatsApi.Models;

namespace BasketStatsApi
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<Team, GetTeamDto>();
            CreateMap<AddTeamDto, Team>();
            CreateMap<Player, GetPlayerDto>();
            CreateMap<AddPlayerDto, Player>();
            CreateMap<Match, GetMatchDto>();
            CreateMap<AddMatchDto, Match>();
            CreateMap<Stat, GetStatDto>();
            CreateMap<AddStatDto, Stat>();  
        }
    }
}
