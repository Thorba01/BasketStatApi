using AutoMapper;
using BasketStatsApi.Data;
using BasketStatsApi.Dtos.Match;
using BasketStatsApi.Dtos.Player;
using BasketStatsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BasketStatsApi.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public MatchService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceMatchResponse<List<GetMatchDto>>> AddMatch(AddMatchDto newMatch)
        {
            var serviceResponse = new ServiceMatchResponse<List<GetMatchDto>>();
            Match match = _mapper.Map<Match>(newMatch);
            match.Player = await _context.Players.FirstOrDefaultAsync(p => p.Id == newMatch.PlayerId);

            _context.Matchs.Add(match);
            await _context.SaveChangesAsync();

            serviceResponse.matchs =
                await _context.Matchs.Where(m => m.Player.Team.User.Id == GetUserId()).Select(m => _mapper.Map<GetMatchDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceMatchResponse<List<GetMatchDto>>> DeleteMatch(int id)
        {
            var serviceResponse = new ServiceMatchResponse<List<GetMatchDto>>();

            try
            {
                var match = await _context.Matchs.FirstOrDefaultAsync(m => m.Id == id && m.Player.Team.User.Id == GetUserId());
                if (match is null)
                {
                    throw new Exception($"Match with Id '{id}' not found.");
                }

                _context.Matchs.Remove(match);

                await _context.SaveChangesAsync();

                serviceResponse.matchs = await _context.Matchs
                    .Where(m => m.Player.Team.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetMatchDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceMatchResponse<List<GetMatchDto>>> GetAllMatchs(int playerId)
        {
            var serviceResponse = new ServiceMatchResponse<List<GetMatchDto>>();
            var dbMatchs = await _context.Matchs
                //.Include(m => m.Player)
                .Where(m => m.Player.Team.User.Id == GetUserId() && m.Player.Id == playerId).ToListAsync();
            serviceResponse.matchs = dbMatchs.Select(m => _mapper.Map<GetMatchDto>(m)).ToList();

            return serviceResponse;

            //var serviceResponse = new ServicePlayerResponse<List<GetPlayerDto>>();
            //var dbPlayers = await _context.Players
            //    .Where(p => p.Team.User.Id == GetUserId() && p.Team.id == id).ToListAsync();
            //serviceResponse.players = dbPlayers.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();

            //return serviceResponse;
        }

        public async Task<ServiceMatchResponse<GetMatchDto>> GetMatchById(int id)
        {
            var serviceResponse = new ServiceMatchResponse<GetMatchDto>();
            var dbMatchs = await _context.Matchs
                .Include(m => m.Player)
                //.Include(m => m.Stat)
                .FirstOrDefaultAsync(m => m.Id == id && m.Player.Team.User.Id == GetUserId());
            serviceResponse.matchs = _mapper.Map<GetMatchDto>(dbMatchs);

            return serviceResponse;
        }

        public async Task<ServiceMatchResponse<GetMatchDto>> UpdateMatch(UpdateMatchDto updatedMatch)
        {
            var serviceResponse = new ServiceMatchResponse<GetMatchDto>();

            try
            {
                var match = await _context.Matchs
                    .Include(m => m.Player.Team.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedMatch.Id);
                if (match is null)
                {
                    throw new Exception($"Match with Id '{updatedMatch.Id}' not found.");
                }
                if (match.Player.Team.User.Id == GetUserId())
                {
                    match.Date = updatedMatch.Date;
                    match.Opponent = updatedMatch.Opponent;

                    await _context.SaveChangesAsync();

                    serviceResponse.matchs = _mapper.Map<GetMatchDto>(match);
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.message = $"Match with Id '{updatedMatch.Id}' not found.";
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
