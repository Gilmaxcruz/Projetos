using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mantimentos.App.Data.Context;
using System;
using System.Linq;

namespace ExternalAmbienteConfig
{
    public static class ExternalAmbienteConfig
    {
        public static void RegistroExternoAmbiente(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appsettings = new();
            new ConfigureFromConfigurationOptions<AppSettings>(configuration.GetSection("ConnectionStrings")).Configure(appsettings);

            services.AddDbContext<MantimentoDbContext>(options => options.UseSqlServer(appsettings.DATADB));
        }
    }
}
