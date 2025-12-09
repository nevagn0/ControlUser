namespace ControlSystem.Domain.Repository;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task UpdateUser(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
}