using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dendle404
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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Map("/healthz", act =>
            {
                act.Use(HealthCheck);
            });

            app.Map("", act =>
            {
                act.Use(NotFound);
            });

        }

        private RequestDelegate HealthCheck(RequestDelegate arg)
        {
            return HealthAllGood;

        }

        private Task HealthAllGood(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.WriteAsync("(200) Healthy AF");

            return Task.CompletedTask;
        }

        private RequestDelegate NotFound(RequestDelegate arg)
        {
            return NotFoundDelegate;
        }

        private Task NotFoundDelegate(HttpContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.WriteAsync("(404) Not Found");

            return Task.CompletedTask;
        }
    }
}
