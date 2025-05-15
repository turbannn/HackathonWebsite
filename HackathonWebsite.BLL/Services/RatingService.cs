using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;
using HackathonWebsite.DAL.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HackathonWebsite.BLL.Services
{
    public class RatingService
    {
        private readonly HackathonDbContext _context;
        private readonly IMapper _mapper;

        public RatingService(HackathonDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskRatingReadDto>> GetAllRatingTasksAsync()
        {
            var users = await _context.HackathonTasks
                .AsNoTracking()
                .Include(h => h.User)
                .ToListAsync();

            var userDtos = _mapper.Map<List<TaskRatingReadDto>>(users);

            return userDtos;
        }
    }
}
