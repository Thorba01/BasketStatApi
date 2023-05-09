using AutoMapper;
using BasketStatsApi.Data;
using BasketStatsApi.Dtos.Stat;
using BasketStatsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BasketStatsApi.Services.StatService
{
    public class StatService : IStatService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public StatService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceStatResponse<List<GetStatDto>>> AddStat(AddStatDto newStat)
        {
            var serviceResponse = new ServiceStatResponse<List<GetStatDto>>();
            Stat stat = _mapper.Map<Stat>(newStat);
            stat.Match = await _context.Matchs.FirstOrDefaultAsync(m => m.Id == newStat.MatchId);
            //stat.Player = await _context.Players.FirstOrDefaultAsync(p => p.Id == newStat.PlayerId);

            _context.Stats.Add(stat);
            await _context.SaveChangesAsync();

            serviceResponse.stat =
                await _context.Stats.Where(s => s.Match.Player.Team.User.Id == GetUserId()).Select(s => _mapper.Map<GetStatDto>(s)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceStatResponse<List<GetStatDto>>> DeleteStat(int id)
        {
            var serviceResponse = new ServiceStatResponse<List<GetStatDto>>();

            try
            {
                var stat = await _context.Stats.FirstOrDefaultAsync(s => s.Id == id && s.Match.Player.Team.User.Id == GetUserId());
                if (stat is null)
                {
                    throw new Exception($"Character with Id '{id}' not found.");
                }

                _context.Stats.Remove(stat);

                await _context.SaveChangesAsync();

                serviceResponse.stat = await _context.Stats
                    .Where(s => s.Match.Player.Team.User.Id == GetUserId())
                    .Select(s => _mapper.Map<GetStatDto>(s)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceStatResponse<List<GetStatDto>>> GetAllStats(int id)
        {
            var serviceResponse = new ServiceStatResponse<List<GetStatDto>>();
            var dbStat = await _context.Stats
                .Where(s => s.Match.Player.Team.User.Id == GetUserId() && s.Match.Id == id).ToListAsync();
            serviceResponse.stat = dbStat.Select(s => _mapper.Map<GetStatDto>(s)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceStatResponse<GetStatDto>> GetStatsById(int id)
        {
            var serviceResponse = new ServiceStatResponse<GetStatDto>();
            var dbStat = await _context.Stats
                .FirstOrDefaultAsync(s => s.Id == id && s.Match.Player.Team.User.Id == GetUserId());
            serviceResponse.stat = _mapper.Map<GetStatDto>(dbStat);

            return serviceResponse;
        }

        public async Task<ServiceStatResponse<GetStatDto>> UpdateStat(UpdateStatDto updatedStat)
        {
            var serviceResponse = new ServiceStatResponse<GetStatDto>();

            try
            {
                var stat = await _context.Stats
                    .Include(s => s.Match.Player.Team.User)
                    .FirstOrDefaultAsync(s => s.Id == updatedStat.Id);
                if (stat is null)
                {
                    throw new Exception($"Stat with Id '{updatedStat.Id}' not found.");
                }
                if (stat.Match.Player.Team.User.Id == GetUserId())
                {
                    stat.Totalpoints = updatedStat.Totalpoints;
                    stat.Offrebound = updatedStat.Offrebound;
                    stat.Defrebound = updatedStat.Defrebound;
                    stat.Assists = updatedStat.Assists;
                    stat.Interceptions = updatedStat.Interceptions;
                    stat.Looseball = updatedStat.Looseball;
                    stat.Blocks = updatedStat.Blocks;
                    stat.Onepointmade = updatedStat.Onepointmade;
                    stat.Onepointmiss = updatedStat.Onepointmiss;
                    stat.Twopointmade = updatedStat.Twopointmade;
                    stat.Twopointmiss = updatedStat.Twopointmiss;
                    stat.Threepointmade = updatedStat.Threepointmade;
                    stat.Threepointmiss = updatedStat.Threepointmiss;
                    stat.Offfalt = updatedStat.Offfalt;
                    stat.Deffalt = updatedStat.Deffalt;
                    stat.Technicalfalt = updatedStat.Technicalfalt;
                    stat.Minplayed = updatedStat.Minplayed;
                    stat.Homeaway = updatedStat.Homeaway;


                    await _context.SaveChangesAsync();

                    serviceResponse.stat = _mapper.Map<GetStatDto>(stat);
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.message = $"Stat with Id '{updatedStat.Id}' not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;

            }
            return serviceResponse;
        }
    }
}
