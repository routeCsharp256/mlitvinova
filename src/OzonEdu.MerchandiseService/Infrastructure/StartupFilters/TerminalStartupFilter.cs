using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OzonEdu.MerchandiseService.Infrastructure.Middlewares;

namespace OzonEdu.MerchandiseService.Infrastructure.StartupFilters
{
    public class TerminalStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                app.Map("/live", b => b.Run(
                    c => c.Response.WriteAsync(HttpStatusCode.OK.ToString())));
                app.Map("/ready", b => b.Run(
                    c => c.Response.WriteAsync(HttpStatusCode.OK.ToString())));
                app.UseMiddleware<RequestLoggingMiddleware>();
                next(app);
            };
        }
    }
}