using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> ReadByIdAsync(Guid id);
}