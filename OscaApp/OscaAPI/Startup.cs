using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OscaFramework.MicroServices;

namespace OscaAPI
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

            //*********************** Serviços para acesso a dados vi ADO *******************
            //Serviço para acesso a metodos de conexão SQL no banco DATA
            //TODO:Falta implementar em todos os controllers, apenas usado na Ordem de Servico
            SqlGenericDataServices sqlData = new SqlGenericDataServices();
            services.AddSingleton<SqlGenericDataServices>(_ => sqlData);

            //Serviço para retornar dados que são comuns a todas empresas
            //TODO:Falta implementar em todaas chamadas, sendo usado apenas na Ordem de serviço
            SqlGenericServices sqlServices = new SqlGenericServices();
            services.AddSingleton<SqlGenericServices>(_ => sqlServices);
            //*********************** FIM Serviços para acesso a dados vi ADO *******************

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
