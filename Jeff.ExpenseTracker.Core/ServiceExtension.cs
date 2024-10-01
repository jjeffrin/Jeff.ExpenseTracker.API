using FluentValidation;
using Jeff.ExpenseTracker.Core.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jeff.ExpenseTracker.Core
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services
                .AddValidatorsFromAssemblyContaining<CreateTransactionDTOValidator>()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
