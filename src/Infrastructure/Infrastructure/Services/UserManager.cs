using Application.Dto;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.Utilities.Results;
using Application.Utilities.Security.Hashing;
using Domain.Common.Models;
using Domain.Entities;
using Microsoft.Extensions.Localization;
using Resource;

namespace Infrastructure.Services;

public class UserManager : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;
    private readonly IStringLocalizer<SharedResource> localizer;

    public UserManager(IUserRepository userRepository, ITokenService tokenService, IStringLocalizer<SharedResource> localizer)
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
        this.localizer = localizer;
    }


    public async Task<IDataResult<User>> Login(LoginDto model)
    {
        try
        {
            var user = await userRepository.FirstOrDefaultAsync(x => x.EmailHash == HashingHelper.Encrypt(model.Email));

            if (user != null)
                if (HashingHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                    return new SuccessDataResult<User>(user);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<User>(e.Message);
        }

        return new ErrorDataResult<User>(localizer[Messages.Error.WrongCredential]);
    }


    public async Task<IDataResult<AccessToken>> GetAccessToken(User model)
    {
        try
        {
            var claims = await userRepository.GetClaims(model);
            var result = tokenService.CreateToken(model, claims);

            return new SuccessDataResult<AccessToken>(result);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<AccessToken>(e.Message);
        }
    }


}

