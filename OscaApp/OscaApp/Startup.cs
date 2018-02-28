using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
 
using OscaApp.framework;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using OscaFramework.MicroServices;
using OscaFramework.Data;

namespace OscaApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
             
        }

        public IConfiguration Configuration { get; }  
         

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            

            //// Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //*********Carrega informações do banco de dados via ENTITY_FRAMEWORD
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("databaseService")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<ContexDataService>(options => options.UseSqlServer(Configuration.GetConnectionString("databaseService")));
            services.AddDbContext<ContexDataManager>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //********* FIMCarrega informações do banco de dados via ENTITY_FRAMEWORD
                    

            //*********************** Serviços para acesso a dados vi ADO *******************
            //Serviço para acesso a metodos de conexao SQL no banco Manager
            services.AddSingleton<SqlGenericManager,  SqlGenericManager>();
            //Serviço para acesso a metodos de conexão SQL no banco DATA      
            SqlGenericData sqlData = new SqlGenericData();
            services.AddSingleton<SqlGenericData>(_ => sqlData);
            //Serviço para retornar dados que são comuns a todas empresas        
            SqlGeneric sqlServices = new SqlGeneric();
            services.AddSingleton<SqlGeneric>(_ => sqlServices);
            //*********************** FIM Serviços para acesso a dados vi ADO *******************


            services.AddMvc(config =>
            {
                config.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider());
            });

            services.AddMvc();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(100000000);
                options.Cookie.HttpOnly = true;
            });                       
 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {        

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var supportedCultures = new[]{new CultureInfo("pt-BR")};

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),             
                SupportedCultures = supportedCultures,            
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}");
            });
        }
    }
}
