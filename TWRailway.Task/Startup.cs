using System;
using System.IO;
using System.Linq;
using AutoMapper;
using CoreProfiler.Web;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TWRailway.Task.Infrastructure.DI;
using TWRailway.Task.Infrastructure.Extension;
using TWRailway.Task.Infrastructure.Hangfire;

namespace TWRailway.Task
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private HangfireSettings HangfireSettings { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region hangfire

            var hangfireSettings = new HangfireSettings();
            this.Configuration
                .GetSection("HangfireSettings")
                .Bind(hangfireSettings);
            this.HangfireSettings = hangfireSettings;

            var hangfireConnection = this.Configuration.GetConnectionString("Hangfire");
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage
                    (
                        nameOrConnectionString: hangfireConnection,
                        options: new SqlServerStorageOptions
                        {
                            SchemaName = hangfireSettings.SchemaName,
                            PrepareSchemaIfNecessary = hangfireSettings.PrepareSchemaIfNecessary,
                            JobExpirationCheckInterval = TimeSpan.FromSeconds(60)
                        }
                    )
                    .UseConsole()
                    .UseDashboardMetric(SqlServerStorage.ActiveConnections)
                    .UseDashboardMetric(SqlServerStorage.TotalConnections)
                    .UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount)
                    .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                    .UseDashboardMetric(DashboardMetrics.FailedCount)
                    .UseDashboardMetric(DashboardMetrics.SucceededCount);
            });

            #endregion hangfire

            #region swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Taiwan Railway Task",
                    Description = "This is Taiwan Railway ASP.NET Core 3.1 RESTful API."
                });

                var basePath = AppContext.BaseDirectory;
                var xmlFiles = Directory.EnumerateFiles(basePath, $"*.xml", SearchOption.TopDirectoryOnly);

                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile, true);
                }
            });

            #endregion swagger

            services.AddElasticsearch(Configuration);

            services.AddDependencyInjection(Configuration);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCoreProfiler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core 3.1 Taiwan Railway v1.0.0");
            });

            var queues = this.HangfireSettings.Queues.Any()
                ? this.HangfireSettings.Queues
                : new[] { "default" };

            app.UseHangfireServer
            (
                options: new BackgroundJobServerOptions
                {
                    ServerName = $"{Environment.MachineName}:{HangfireSettings.ServerName}",
                    WorkerCount = this.HangfireSettings.WorkerCount,
                    Queues = queues
                }
            );

            app.UseHangfireDashboard
            (
                pathMatch: "/hangfire",
                options: new DashboardOptions
                {
                    IgnoreAntiforgeryToken = true
                }
            );
        }
    }
}