using HackathonWebsite.DAL.Entities;
using HackathonWebsite.DAL.Data;
using HackathonWebsite.BLL.DtoEntities.UserDtos;
using HackathonWebsite.BLL.Interfaces;
using FluentValidation;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.BLL.Services
{
    public class UserService
    {
        private readonly HackathonDbContext _context;
        private readonly IValidator<IUserTransferObject> _validator;
        private readonly IMapper _mapper;

        public UserService(HackathonDbContext dbContext, IValidator<IUserTransferObject> expenseValidator, IMapper mapper)
        {
            _context = dbContext;
            _validator = expenseValidator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = _mapper.Map<List<UserReadDto>>(users);

            return userDtos;
        }

        public async Task<UserReadDto?> GetUserByIdAsync(int id)
        {
            if (id < 0) return null;

            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            var userReadDto = _mapper.Map<UserReadDto>(user);

            return userReadDto;
        }

        public async Task<UserReadDto?> GetTeacherByIdAsync(int id)
        {
            if (id < 0) return null;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role.Equals("Teacher"));
            if (user == null) return null;

            var tasks = await _context.HackathonTasks.Where(h => h.TeacherIdRatedBy == user.Id).ToListAsync();

            user.Tasks = tasks;

            var userReadDto = _mapper.Map<UserReadDto>(user);

            return userReadDto;
        }

        public async Task<UserReadDto?> GetUserByNameAndPasswordAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user is null) return null;

            return _mapper.Map<UserReadDto>(user);
        }
        public async Task<UserReadDto?> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user is null) return null;

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> AddUserAsync(UserCreateDto userCreateDto)
        {
            var validationResult = await _validator.ValidateAsync(userCreateDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return false;
            }

            var user = _mapper.Map<User>(userCreateDto);

            user.Role = userCreateDto.Role;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var validationResult = await _validator.ValidateAsync(userUpdateDto);
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
                await _context.Users
                    .Where(u => u.Id == userUpdateDto.Id)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.Username, userUpdateDto.Username)
                    .SetProperty(u => u.Role, userUpdateDto.Role));
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine("ERROR: User Update Failed");
                Console.WriteLine(exception.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id < 0) return false;

            await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
            return true;
        }
    }
}
