
using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Jobs;
using MetricsManager.Repository;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddFluentMigratorCore()
               .ConfigureRunner(rb => rb
                   // добавляем поддержку SQLite 
                   .AddSQLite()
                   // устанавливаем строку подключения
                   .WithGlobalConnectionString(ConnectionString)
                   // подсказываем где искать классы с миграциями
                   .ScanIn(typeof(Startup).Assembly).For.Migrations()
               ).AddLogging(lb => lb
                   .AddFluentMigratorConsole());
            services.AddSingleton<IHddMetricRepository, HddMetricsRepository>();
            services.AddSingleton<ICpuMetricRepository, CpuMetricRepository>();
            services.AddSingleton<IDotNetMetricRepository, DotNetMetricRepository>();
            services.AddSingleton<INetworkMetricRepository, NetworkMetricRepository>();
            services.AddSingleton<IRamMetricRepository, RamMetricRepository>();
            services.AddSingleton<IAgentsrRepository, AgentsRepository>();
            services.AddSingleton<AllHddMetricsApiResponse>();
            services.AddSingleton<AllCpuMetricsApiResponse>();
            services.AddSingleton<AllDotNetMetricsApiResponse>();
            services.AddSingleton<AllNetworkMetricsApiRespodse>();
            services.AddSingleton<AllRamMetricsApiResponse>();
            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                 .AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<ConnectionManager>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();            
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<HddMetricsJob>();
            services.AddSingleton<CpuMetricsJob>();
            services.AddSingleton<DotNetMetricsJob>();
            services.AddSingleton<NetworkMetricsJob>();
            services.AddSingleton<RamMetricsJob>();
            services.AddSingleton(new JobSchedule(
                    jobType: typeof(HddMetricsJob),
                    cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                    jobType: typeof(CpuMetricsJob),
                    cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                    jobType: typeof(DotNetMetricsJob),
                    cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                    jobType: typeof(NetworkMetricsJob),
                    cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                    jobType: typeof(RamMetricsJob),
                    cronExpression: "0/5 * * * * ?"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "API сервиса агента сбора метрик",
                    Description = "Тут можно поиграть с api нашего сервиса",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kadyrov",
                        Email = string.Empty,
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "можно указать под какой лицензией все опубликовано",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса агента сбора метрик");
                    c.RoutePrefix = string.Empty;
                });
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
