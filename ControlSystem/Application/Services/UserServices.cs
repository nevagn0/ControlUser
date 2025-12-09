using ControlSystem.Application.Dto;
using ControlSystem.Domain.Repository;
using ControlSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.Application.Services;

public class UserServices
{
    private readonly IUserRepository _userRepository;
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var user = await _userRepository.GetAllAsync();
        return user.Select(user => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            Name = user.Name,
            DateCreate = user.DateCreate,
            DateUpdate = user.DateUpdate
        });
        
    }
}