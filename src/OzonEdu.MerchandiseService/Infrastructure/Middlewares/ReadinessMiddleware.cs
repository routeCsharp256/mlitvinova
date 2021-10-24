using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MerchandiseService.Infrastructure.Middlewares
{
    public class ReadinessMiddleware
    {
        public ReadinessMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(HttpStatusCode.OK.ToString());
        }
    }
}