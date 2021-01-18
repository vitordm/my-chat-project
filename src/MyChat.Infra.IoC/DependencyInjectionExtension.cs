using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyChat.Application.Service.Contracts;
using MyChat.Application.Service.Services;
using System;
using System.Linq;

namespace MyChat.Infra.IoC
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddServices();

            return services;
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
        }
    }
}
