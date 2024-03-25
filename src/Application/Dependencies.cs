using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public class Dependencies
{
    public static IServiceCollection ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped<ITimeRecordUseCase, TimeRecordUseCase>();

        return services;
    }
}

