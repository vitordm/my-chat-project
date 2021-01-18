using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyChat.Application.Service.Contracts;
using MyChat.Application.Service.Services;
using MyChat.Domain.Auth;
using MyChat.Infra.Data.Context;
using System;

namespace MyChat.Infra.IoC
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyChatDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                );
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //Password Rules
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

                //User Rules
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<MyChatDbContext>();

            services.AddServices();

            return services;
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static IApplicationBuilder EnsureSeedData(this IApplicationBuilder app, IServiceProvider provider)
        {
            provider.GetRequiredService<MyChatDbContext>().Database.Migrate();

            return app;
        }
    }
}
