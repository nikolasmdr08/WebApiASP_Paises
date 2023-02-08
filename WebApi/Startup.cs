using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi
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
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseInMemoryDatabase("paisDB"));
            services.AddMvc().AddJsonOptions(configureJson);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void configureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            if (!context.provincia.Any())
            {
                context.provincia.AddRange(new List<Pais>() {
                    new Pais() { Nombre = "Republica Dominicana", Provincias = new List<Provincia>(){
                        new Provincia() { Nombre = "Azua" }
                    } },
                    new Pais() { Nombre = "Mexico", Provincias = new List<Provincia>(){
                        new Provincia() { Nombre = "Puebla" },
                        new Provincia() { Nombre = "Queretaro" }
                    } },
                    new Pais() { Nombre = "Argentina"}
                });
                context.SaveChanges();
            }
                
            
        }
    }
}
