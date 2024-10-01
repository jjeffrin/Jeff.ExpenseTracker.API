using Jeff.ExpenseTracker.Infrastructure;
using Jeff.ExpenseTracker.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Jeff.ExpenseTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfraServices();
            builder.Services.AddCoreServices();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(o => o.AddPolicy("ETPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));

            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://securetoken.google.com/authentication-eed69";
                options.Audience = "authentication-eed69";
                options.TokenValidationParameters.ValidIssuer = "https://securetoken.google.com/authentication-eed69";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("ETPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
