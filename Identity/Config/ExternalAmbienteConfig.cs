using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Identity.Config
{
    public static class ExternalAmbienteConfig
    {
        public static void RegistroExternoAmbiente(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appsettings = new();
            new ConfigureFromConfigurationOptions<AppSettings>(configuration.GetSection("conexao")).Configure(appsettings);

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(appsettings.DATADB));
        }
    }
}
