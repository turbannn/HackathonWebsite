using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;
using HackathonWebsite.DAL.Entities;
using HackathonWebsite.DAL.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AutoMapper;
using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.Services
{
    public class HackathonTaskService
    {
        private readonly HackathonDbContext _context;
        private readonly IValidator<ITaskTransferObject> _validator;
        private readonly IMapper _mapper;

        public HackathonTaskService(HackathonDbContext context, IValidator<ITaskTransferObject> taskValidator, IMapper mapper)
        {
            _context = context;
            _validator = taskValidator;
            _mapper = mapper;
        }

        public async Task<TaskProfileReadDto?> GetProfileTaskByIdAsync(int id)
        {
            if (id < 0) return null;

            var task = await _context.HackathonTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return null;

            var taskDTO = _mapper.Map<TaskProfileReadDto>(task);

            return taskDTO;
        }

        public async Task<bool> AddTaskAsync(TaskCreateDto hackathonDto)
        {
            var validationResult = await _validator.ValidateAsync(hackathonDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return false;
            }

            try
            {
                var task = _mapper.Map<HackathonTask>(hackathonDto);

                await _context.HackathonTasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateTaskAsync(TaskUpdateDto hackathonDto)
        {
            var validationResult = await _validator.ValidateAsync(hackathonDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return false;
            }

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