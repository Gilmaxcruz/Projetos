using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mantimentos.App.Configurations;
using ExternalAmbienteConfig;
using Mantimentos.App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Mantimentos.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //Classe de conexão
            services.RegistroExternoAmbiente(Configuration);
            services.AddAutoMapper(typeof(Startup));
            //services.AddAuthorizationConfig();
            //Classe registra dependecias
            services.Depedencias();
            services.AddControllersWithViews();



            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));

            //    options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
            //    options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));
            //});


            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddDbContext<MantimentosAppContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("DATADB")));


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MantimentosAppContext>();



            //Classe registra todo contexto do MVC incluindo a passagem do Fluent por ele.
            services.MvcConfig();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseGlobalizationConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); //Nunca esqueça disso quando estiver trabalhando com Identity manual
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
