﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using wcindicator.api.Models;
using wcindicator.api.Services;

namespace wcindicator.api
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
            services
                .AddDbContext<WCIndicatorContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("WCIndicatorDbContext")))
                .AddMvc();

            services.AddTransient<IWCStatusService, WCStatusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
                {
                    appBuilder.UseStatusCodePagesWithReExecute("/apierror/{0}");
                    appBuilder.UseExceptionHandler("/apierror/500");
                });
                app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), appBuilder =>
                {
                    app.UseExceptionHandler("/Home/Error");
                });
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
