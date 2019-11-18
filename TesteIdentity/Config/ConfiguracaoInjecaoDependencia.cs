using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteIdentity.Models;

namespace TesteIdentity.Config
{
    public static class ConfiguracaoInjecaoDependencia
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection servicesCollection)
        {
            //Aqui você adiciona todas as suas dependencias
            //servicesCollection.AddSingleton<IAuthorizationHandler>
            return servicesCollection;
        }

        public static IServiceCollection IdentityConfig(this IServiceCollection servicesCollection, IConfiguration Configuration)
        {

            servicesCollection.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            servicesCollection.AddDbContext<TesteIdentityContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("TesteIdentityContextConnection")));

            servicesCollection.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(Microsoft.AspNetCore.Identity.UI.UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<TesteIdentityContext>();

            return servicesCollection;
        }
    }  
}
