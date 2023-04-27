using Application.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserManager>();
        services.AddTransient<ICategoryService, CategoryManager>();
        services.AddTransient<IPostService, PostManager>();
        services.AddTransient<ITokenService, TokenManager>();
        services.AddTransient<IFileService, FileManager>();
        services.AddHttpContextAccessor();


        return services;
    }
}
