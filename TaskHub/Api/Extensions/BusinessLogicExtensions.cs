using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Dal;
using Logic;

namespace Api.Extensions;

public static class BusinessLogicExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddDal();
        services.AddLogic();

        services.AddScoped<IManageUserUseCase, ManageUserUseCase>();

        return services;
    }
}