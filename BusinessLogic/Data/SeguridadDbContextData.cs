using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {
       
        
        public static async Task SeedUserAsync(UserManager<Usuario> userManager) 
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario { 
                    Nombre = "Camilo",
                    Apellido = "Guevara",
                    UserName = "CamiloAndresGTR",
                    Email = "camiloandresgtr@gmail.com",
                    Direccion = new Direccion
                    {
                        Calle= "Calle Falsa 123",
                        Ciudad = "Springfield",
                        CodigoPostal = "1122587",
                        Departamento = "Humm"
                    }
                };
                
                //La contraseña debe cumplir con requisitos como Mayusculas, minusculas, números y caracteres especiales
                await userManager.CreateAsync(usuario,"Camilo123456$");

            }
        }
    }
}
