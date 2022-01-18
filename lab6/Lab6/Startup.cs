using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Lab6
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
            services.AddHttpClient();
            services.AddAuthentication(config =>
                    {
                        config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        config.DefaultChallengeScheme = "oidc";

                    })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect("oidc", config =>
                {
                    config.ClientId = "client_id_lab";
                    config.ClientSecret = "client_secret_lab";
                    config.SaveTokens = true;
                    config.Authority = "http://localhost:2000";
                    config.ResponseType = "code";
                    config.RequireHttpsMetadata = false;
                });
            services.AddOpenTelemetryTracing(builder =>
            {
                builder.AddHttpClientInstrumentation();
                builder.AddAspNetCoreInstrumentation(opts =>
                {
                    opts.RecordException = true;

                });
                builder.AddSource("Front_Lab6");
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Front_Lab6"));
                builder.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:4317");
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
