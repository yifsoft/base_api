using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<OperationClaim>> GetClaims(User entity);
}
