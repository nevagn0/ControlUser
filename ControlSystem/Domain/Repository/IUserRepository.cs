namespace ControlSystem.Domain.Repository;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<bool> ExistAsync(Guid id);
    Task UpdateUser(User user);
}