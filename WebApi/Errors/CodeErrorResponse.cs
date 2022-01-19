using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Errors
{
    public class CodeErrorResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public CodeErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? getDefaultMessageStatusCode(statusCode);

        }

        private string getDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request enviado tiene errores",
                401 => "No tiene autorización para este recusro",
                404 => "El recurso no fue encontrado",
                500 => "Se produjeron errores en el servidor",
                _ => null

            };
        }
    }
}
