using ControlSystem.Infrastructure.Data;

namespace ControlSystem.Infrastructure.Repository;

public class UserRepository
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
}