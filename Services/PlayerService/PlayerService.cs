using AutoMapper;
using BasketStatsApi.Data;
using BasketStatsApi.Dtos.Player;
using BasketStatsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BasketStatsApi.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PlayerService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServicePlayerResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer)
        {
            var serviceResponse = new ServicePlayerResponse<List<GetPlayerDto>>();
            Player player = _mapper.Map<Player>(newPlayer);
            //player.Team.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            player.Team = await _context.Teams.FirstOrDefaultAsync(t => t.id == newPlayer.TeamId);

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            serviceResponse.players =
                await _context.Players.Where(p => p.Team.User.Id == GetUserId()).Select(p => _mapper.Map<GetPlayerDto>(p)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServicePlayerResponse<List<GetPlayerDto>>> DeletePlayer(int id)
        {
            var serviceResponse = new ServicePlayerResponse<List<GetPlayerDto>>();

            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id && p.Team.User.Id == GetUserId());
                if (player is null)
                {
                    throw new Exception($"Player with Id '{id}' not found.");
                }

                _context.Players.Remove(player);

                await _context.SaveChangesAsync();

                serviceResponse.players = await _context.Players
                    .Where(p => p.Team.User.Id == GetUserId())
                    .Select(p => _mapper.Map<GetPlayerDto>(p)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServicePlayerResponse<List<GetPlayerDto>>> GetAllPlayer(int id)
        {
            var serviceResponse = new ServicePlayerResponse<List<GetPlayerDto>>();
            var dbPlayers = await _context.Players
                .Where(p => p.Team.User.Id == GetUserId() && p.Team.id == id).ToListAsync();
            serviceResponse.players = dbPlayers.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();

            return serviceResponse;
        }

        public async Task<ServicePlayerResponse<GetPlayerDto>> GetPlayerById(int id)
        {
            var serviceResponse = new ServicePlayerResponse<GetPlayerDto>();
            var dbPlayers = await _context.Players
                .Include(p => p.Matchs)
                .FirstOrDefaultAsync(p => p.Id == id && p.Team.User.Id == GetUserId());
            serviceResponse.players = _mapper.Map<GetPlayerDto>(dbPlayers);

            return serviceResponse;
        }

        public async Task<ServicePlayerResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            var serviceResponse = new ServicePlayerResponse<GetPlayerDto>();

            try
            {
                var player = await _context.Players
                    .Include(p => p.Team.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedPlayer.Id);
                if (player is null)
                {
                    throw new Exception($"Player with Id '{updatedPlayer.Id}' not found.");
                }
                if (player.Team.User.Id == GetUserId())
                {
                    player.Name = updatedPlayer.Name;

                    await _context.SaveChangesAsync();

                    serviceResponse.players = _mapper.Map<GetPlayerDto>(player);
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.message = $"Player with Id '{updatedPlayer.Id}' not found.";
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
