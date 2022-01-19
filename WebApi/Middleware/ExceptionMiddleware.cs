using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Errors;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        //Cada vez que se haga una transacción en el backend van a entrar por este InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Si la transacción tiene resultado exitoso, ejecutamos el siguiente paso(Continuar con la ejecución)
                await _next(context);
            }
            catch (Exception e)
            {
                //Hacemos un log con la excepción y el mensaje de la misma
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //evaluar si está en ambiente de desarrollo
                var response = _env.IsDevelopment()
                    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace.ToString())
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError);

                //Mantener propiedades en minuscula en el json
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                //Convertir la respuesta a formato json
                var json = JsonSerializer.Serialize(response);
                //Enviar la respuesta
                await context.Response.WriteAsync(json);
            }
        }

    }
}
