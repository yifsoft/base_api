using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext context;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }
    public async Task<List<OperationClaim>> GetClaims(User entity)
    {
        try
        {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == entity.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

            return result.ToList();
        }
        catch (Exception e)
        {
            return null;
        }
    }
}

