using Identity.Data;
using Identity.Extensions;
using KissLog;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Identity.Extensions.PermissaoNecessaria;

namespace Identity.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection  ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
             .AddEntityFrameworkStores<IdentityContext>(); //Ver com Larisssa não entendi isso muito bem, acho que carrega apenas drivers caso o projeto esteja utilizando identity

            services.AddScoped<AuditoriaFilter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });
            return services;
        }
    }
}
