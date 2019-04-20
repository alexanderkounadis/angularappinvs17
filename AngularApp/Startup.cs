using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AngularApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // redirect any non-api call to the Angular app
            // so the routing there is handled by our app

            app.Use(async (ctx, next) =>
            {
                await next();
                if(ctx.Response.StatusCode == 404 && 
                    !Path.HasExtension(ctx.Request.Path.Value) && 
                    !ctx.Request.Path.Value.StartsWith("/api/"))
                {
                    ctx.Request.Path = "/index.html";
                    await next();
                }
            });

            // configure app for usage as API
            // with default route of '/api/[Controller]'
            app.UseMvcWithDefaultRoute();

            // configure app to serve index.html file from /wwwroot
            // when you access the server from a web browser
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
