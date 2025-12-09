using ControlSystem.Domain.Repository;
using ControlSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly ControlSystemDB _controlSystemDB;

    public UserRepository(ControlSystemDB controlSystemDB, ILogger<UserRepository> logger)
    {
        _controlSystemDB = controlSystemDB;
    }

    public async Task CreateUser(User user)
    {
            await _controlSystemDB.Users.AddAsync(user);
            await _controlSystemDB.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    { 
        _controlSystemDB.Users.Update(user);
        await _controlSystemDB.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _controlSystemDB.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _controlSystemDB.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _controlSystemDB.Users.ToListAsync();
    }
}