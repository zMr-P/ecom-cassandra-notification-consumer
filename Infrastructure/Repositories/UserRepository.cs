using Cassandra.Mapping;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class UserRepository(IMapper sessionMapper) : IUserRepository
{
    private readonly IMapper _sessionMapper = sessionMapper;

    public async Task<User?> ReadByIdAsync(Guid id)
    {
        return await _sessionMapper.FirstOrDefaultAsync<User>("WHERE id = ?", id);
    }
}