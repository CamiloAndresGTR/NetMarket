using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Middleware;

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
            //Crear la variable builder para añadir el servicio de identityCore basado en la clase Usuario
            var builder = services.AddIdentityCore<Usuario>();
            //Crear una nueva instancia para IdentityBuilder
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            //Se añade el EntityFrameworkStores basado en la clase SeguridadDbContext
            builder.AddEntityFrameworkStores<SeguridadDbContext>();
            //Añadir el signInManager basado en la clase Usuario
            builder.AddSignInManager<SignInManager<Usuario>>();

            services.AddAuthentication();
            //Esto hace que cuando arranque la aplicación WebApi en ese momento se genere un objeto de tipo
            ////IGenericRepository en cada request del cliente
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            // Instanciamos el DBContext en Startup
            services.AddDbContext<MarketDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<SeguridadDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("IdentitySeguridad"));
            });
            //Agregar el service de automapper
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddControllers();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors","?code={0}");

            app.UseRouting();
            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
