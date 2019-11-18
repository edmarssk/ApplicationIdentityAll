using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteIdentity.Config;

namespace TesteIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region CONFIGURE SERVICE

        public void ConfigureServices(IServiceCollection services)
        {
            //Todas as configuracoes do Identity estão na classe ConfiguracaoInjecaoDependencia
            services.IdentityConfig(Configuration);

            //Todas as configuracoes do Dependencias estão na classe ConfiguracaoInjecaoDependencia
            services.ResolveDependencies();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "PodeExcluir", configurePolicy: policy => policy.RequireClaim("PodeExcluir"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        #endregion

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
