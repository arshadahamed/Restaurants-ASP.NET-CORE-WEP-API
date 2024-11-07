using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssemby = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssemby));
        services.AddAutoMapper(applicationAssemby);
        services.AddValidatorsFromAssembly(applicationAssemby)
            .AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();
    }
}
