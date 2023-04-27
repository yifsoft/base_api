using Domain.Common.Models;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
}