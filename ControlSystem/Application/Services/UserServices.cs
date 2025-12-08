using ControlSystem.Application.Dto;
using ControlSystem.Domain.Repository;
using ControlSystem.Infrastructure.Repository;
namespace ControlSystem.Application.Services;

public class UserServices
{
    private readonly IUserRepository _userRepository;
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> CreateUser(CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            HashPassword = HashPassword(dto.Password), 
            Role = dto.Role,
            DateCreate = DateTime.UtcNow,
            DateUpdate = DateTime.UtcNow 
        };
        
        await _userRepository.CreateUser(user);
        
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role,
            DateCreate = user.DateCreate,
            DateUpdate = user.DateUpdate
        };
    }
    
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}