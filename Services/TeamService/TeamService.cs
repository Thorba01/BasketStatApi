using AutoMapper;
using BasketStatsApi.Data;
using BasketStatsApi.Dtos.Team;
using BasketStatsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BasketStatsApi.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TeamService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceTeamResponse<List<GetTeamDto>>> AddTeam(AddTeamDto newTeam)
        {
            var serviceResponse = new ServiceTeamResponse<List<GetTeamDto>>();
            Team team = _mapper.Map<Team>(newTeam);
            team.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            serviceResponse.teams = await _context.Teams.Where(t => t.User.Id == GetUserId()).Select(t => _mapper.Map<GetTeamDto>(t)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceTeamResponse<List<GetTeamDto>>> DeleteTeam(int id)
        {
            var serviceResponse = new ServiceTeamResponse<List<GetTeamDto>>();

            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.id == id && t.User.Id == GetUserId());
                if (team is null)
                {
                    throw new Exception($"Team with Id '{id}' not found.");
                }

                _context.Teams.Remove(team);

                await _context.SaveChangesAsync();

                serviceResponse.teams = await _context.Teams
                    .Where(t => t.User.Id == GetUserId())
                    .Select(t => _mapper.Map<GetTeamDto>(t)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceTeamResponse<List<GetTeamDto>>> GetAllTeams()
        {
            var serviceResponse = new ServiceTeamResponse<List<GetTeamDto>>();
            var dbCharacters = await _context.Teams
                .Include(t => t.Players)
                .Where(t => t.User.Id == GetUserId()).ToListAsync();
            serviceResponse.teams = dbCharacters.Select(c => _mapper.Map<GetTeamDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceTeamResponse<GetTeamDto>> GetTeamById(int id)
        {
            var serviceResponse = new ServiceTeamResponse<GetTeamDto>();
            var dbCharacter = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.id == id && t.User.Id == GetUserId());
            serviceResponse.teams = _mapper.Map<GetTeamDto>(dbCharacter);

            return serviceResponse;
        }

        public async Task<ServiceTeamResponse<GetTeamDto>> UpdateTeam(UpdateTeamDto updatedTeam)
        {
            var serviceResponse = new ServiceTeamResponse<GetTeamDto>();

            try
            {
                var team = await _context.Teams
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.id == updatedTeam.Id);
                if (team is null)
                {
                    throw new Exception($"Character with Id '{updatedTeam.Id}' not found.");
                }
                if (team.User.Id == GetUserId())
                {
                    team.teamname = updatedTeam.Teamname;

                    await _context.SaveChangesAsync();

                    serviceResponse.teams = _mapper.Map<GetTeamDto>(team);
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.message = $"Character with Id '{updatedTeam.Id}' not found.";
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
