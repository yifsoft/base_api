using Application.Dto;
using Application.Utilities.Results;
using Domain.Common.Models;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<IDataResult<User>> Login(LoginDto model);
    Task<IDataResult<AccessToken>> GetAccessToken(User model);
}
