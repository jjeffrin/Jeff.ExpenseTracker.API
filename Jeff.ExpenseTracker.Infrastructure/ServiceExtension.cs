using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jeff.ExpenseTracker.Infrastructure
{
    public static class ServiceExtension
    {
        private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            return services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExpenseTracker;Trusted_Connection=True;TrustServerCertificate=True");
            });
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            return services
                .AddDbContext()
                .AddUnitOfWork();
        }
    }
}
