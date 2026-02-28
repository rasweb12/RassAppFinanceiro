using System;
using System.Collections.Generic;
using System.Text;

namespace RassAppFinanceiro.RassApp.Finance.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    Success = false,
                    ErrorCode = "INTERNAL_ERROR",
                    ErrorMessage = "Erro interno no servidor.",
                    TraceId = context.TraceIdentifier
                });
            }
        }
    }
}
