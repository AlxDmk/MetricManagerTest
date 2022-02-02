using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricAgent.DAL.Models;
using MetricAgent.DAL.Interfaces;
using Core.DAL.Interfaces;
using AutoMapper;
using MetricAgent.Mappers;
using FluentMigrator.Runner;
using MetricAgent.DAL.Repositories;
using Quartz.Spi;
using MetricAgent.Jobs;
using Quartz;
using Quartz.Impl;

namespace MetricAgent
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
            services.AddControllers();
            ConfigureSqlLiteConnection(services);

            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("Sql"))
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb.AddFluentMigratorConsole());

            //services.AddSingleton<IJobFactory, SingletonJobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //services.AddSingleton<CpuMetricJob>();
            //services.AddSingleton(new JobSchedule(
            //    typeof(CpuMetricJob), "0/5 * * * * ?"));

            //services.AddHostedService<QuartzHostedService>();
        }

        private void ConfigureSqlLiteConnection (IServiceCollection services)
        {
            var connection = new SQLiteConnection(Configuration.GetConnectionString("Sql"));
            connection.Open ();
        }
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
