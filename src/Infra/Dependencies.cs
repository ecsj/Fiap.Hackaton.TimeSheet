using Application.Interfaces;
using Domain.Repositories.Base;
using Infra.Data.Repositories;
using Infra.Data;
using Infra.MessageQueue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;

namespace Infra;
public class Dependencies
{
    public static IServiceCollection ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        bool useOnlyInMemoryDatabase = configuration.GetValue<bool?>("UseOnlyInMemoryDatabase") ?? false;

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseInMemoryDatabase("TechChallengeTotem"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddSingleton<IMessageQueueService, RabbitMQService>();


        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();

        return services;
    }
}