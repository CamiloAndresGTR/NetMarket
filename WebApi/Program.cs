using BusinessLogic.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Creo una instancia en el Host que ejecute la aplicación WebApi
            var host = CreateHostBuilder(args).Build();
            //Creamos un using var scope
            using (var scope = host.Services.CreateScope())
            {
                //Declaro una instancia del service provider que permite ejecutar la migración
                var services = scope.ServiceProvider;
                //logger par aimprimir errores o manejar errores por medio de LoggerFactory
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    //Instanciamos el DbContext
                    var context = services.GetRequiredService<MarketDbContext>();
                    //Ejecutamos la migración como metodo asincrono
                    await context.Database.MigrateAsync();
                    //Se añade la siguiente línea para que al ejecutar el programa se cargue la información de
                    ////maestros a las tablas de acuerdo a la lógica implementada, solo si las tablas están vacías
                    await MarketDbContextData.LoadDataAsync(context, loggerFactory);
                }
                catch (Exception e)
                {
                    //Impimir el mensaje de error en caso de alguna excepción
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "Errores en el proceso de migración");
                }
            }
            //Saliendo del Using ejecuto el host.Run()
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
