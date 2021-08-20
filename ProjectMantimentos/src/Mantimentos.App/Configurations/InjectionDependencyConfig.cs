using Mantimentos.App.Business.Interfaces;
using Mantimentos.App.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mantimentos.App.Configurations
{
    public static class InjectionDependencyConfig
    {
        public static IServiceCollection Depedencias(this IServiceCollection services)
        {
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<ITpMantimentoRepository, TpMantimentoRepository>();
            services.AddScoped<IMantimentoRepository, MantimentoRepository>();
            services.AddScoped<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
            return services;
        }
    }

    
}
