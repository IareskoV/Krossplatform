using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Api.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Database.Api
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
            switch (Configuration.GetValue(typeof(string), "DbType"))
            {
                case "sqlite":
                    services.AddTransient<AbstractContext, SqliteDbContext>();
                    break;
                case "postgresql":
                    services.AddTransient<AbstractContext, PostgresDbContext>();
                    break;
                case "sqlserver":
                    services.AddTransient<AbstractContext, SqlServerDbContext>();
                    break;
                case "inmem":
                    services.AddSingleton<AbstractContext, InMemoryDbContext>();
                    break;
                default:
                    throw new ArgumentException("Wrong db type.");

            }
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.Authority = "http://localhost:2000";
                config.Audience = "LabResource";
                config.RequireHttpsMetadata = false;
            });

            services.AddHostedService<GcEventsCollector>();

            services.AddOpenTelemetryMetrics(builder =>
            {
                builder.AddHttpClientInstrumentation();
                builder.AddAspNetCoreInstrumentation();
                builder.AddMeter("DatabaseAPI.Gen0Collections");
                builder.AddMeter("DatabaseAPI.Gen1Collections");
                builder.AddMeter("DatabaseAPI.Gen2Collections");

                builder.AddMeter("DatabaseAPI.TotalMemory");
                builder.AddMeter("DatabaseAPI.AllocatedMemory");
                builder.AddMeter("DatabaseAPI.Ge0Size");
                builder.AddMeter("DatabaseAPI.Ge1Size");
                builder.AddMeter("DatabaseAPI.Ge2Size");

                builder.AddMeter("DatabaseAPI.LoHSize");
                builder.AddMeter("DatabaseAPI.Gen0Promoted");
                builder.AddMeter("DatabaseAPI.Gen1Promoted");

                builder.AddMeter("DatabaseAPI.Gen2Survived");
                builder.AddMeter("DatabaseAPI.LoHSurvived");

                builder.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:4317");
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
            });

            services.AddOpenTelemetryTracing(builder =>
            {
                builder.AddHttpClientInstrumentation();
                builder.AddAspNetCoreInstrumentation(opts =>
                {
                    opts.RecordException = true;

                });
                builder.AddSqlClientInstrumentation(opts =>
                {
                    opts.SetDbStatementForText = true;
                    opts.RecordException = true;
                });
                builder.AddSource("DatabaseAPI");
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DatabaseAPI"));
                builder.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:4317");
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Database.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Database.Api v1"));
            }
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
