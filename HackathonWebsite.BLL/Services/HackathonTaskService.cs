using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;
using HackathonWebsite.DAL.Entities;
using HackathonWebsite.DAL.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AutoMapper;

namespace HackathonWebsite.BLL.Services
{
    public class HackathonTaskService
    {
        private readonly HackathonDbContext _context;
        private readonly IValidator<HackathonTaskDto> _validator;
        private readonly IMapper _mapper;

        public HackathonTaskService(HackathonDbContext context, IValidator<HackathonTaskDto> expenseValidator, IMapper mapper)
        {
            _context = context;
            _validator = expenseValidator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HackathonTaskDto>> GetAllExpensesAsync()
        {
            var expenses = await _context.HackathonTasks.ToListAsync();

            var expenseDtos = _mapper.Map<List<HackathonTaskDto>>(expenses);

            return expenseDtos;
        }

        public async Task<HackathonTaskDto?> GetTaskByIdAsync(int id)
        {
            if (id < 0) return null;

            var expense = await _context.HackathonTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (expense == null) return null;

            var expenseDTO = _mapper.Map<HackathonTaskDto>(expense);

            return expenseDTO;
        }

        public async Task<bool> AddTaskAsync(HackathonTaskDto hackathonDto)
        {
            var validationResult = await _validator.ValidateAsync(hackathonDto);
            if (!validationResult.IsValid) return false;

            var expense = _mapper.Map<HackathonTask>(hackathonDto);

            await _context.HackathonTasks.AddAsync(expense);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTaskAsync(HackathonTaskDto hackathonDto)
        {
            var validationResult = await _validator.ValidateAsync(hackathonDto);

            if (!validationResult.IsValid) return false;

            var expense = _mapper.Map<HackathonTask>(hackathonDto);

            await _context.HackathonTasks
                .Where(h => h.Id == hackathonDto.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.Name, hackathonDto.Name)
                    .SetProperty(h => h.Rating, hackathonDto.Rating)
                    .SetProperty(h => h.Description, hackathonDto.Description));

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            if (id < 0) return false;

            await _context.HackathonTasks.Where(h => h.Id == id).ExecuteDeleteAsync();
            return true;
        }
    }
}