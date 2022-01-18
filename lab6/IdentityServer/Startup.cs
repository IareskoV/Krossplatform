using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace IdentityServer
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
            var connectionString = Configuration.GetValue<string>("DbConnection");
            services.AddDbContext<AuthDbContext>(options => options.UseSqlite(connectionString));
            services.AddIdentity<AppUser, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(config =>
                {
                    config.UserInteraction.LoginUrl = "/Auth/Login";
                })
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryClients(IdentityServer.Configuration.GetClients())
                .AddInMemoryApiResources(IdentityServer.Configuration.GetApiResources())
                .AddInMemoryApiScopes(IdentityServer.Configuration.GetApiScopes())
                .AddInMemoryIdentityResources(IdentityServer.Configuration.GetIdentityResources())
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

            services.AddOpenTelemetryTracing(builder =>
            {
                builder.AddHttpClientInstrumentation();
                builder.AddAspNetCoreInstrumentation(opts =>
                {
                    opts.RecordException = true;

                });
                builder.AddSource("Identity_Server");
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Identity_Server"));
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

            app.UseIdentityServer();

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
