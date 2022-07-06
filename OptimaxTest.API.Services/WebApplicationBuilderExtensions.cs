using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using OptimaxTest.API.Data.Model;
using OptimaxTest.API.Services.Interfaces;
using OptimaxTest.API.Services.Services;

namespace OptimaxTest.API.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OptimaxDeveloperTestContext>(options =>
              options.UseSqlServer(connectionString));

            services.AddScoped<IUserService, UserService>();
        }
    }
}
