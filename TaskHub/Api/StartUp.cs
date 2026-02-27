using System.Diagnostics;
using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Dal;
using Logic;
using Microsoft.OpenApi.Models;

namespace Api;

/// <summary>
/// Конфигурация приложения
/// </summary>
public sealed class Startup
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Окружение приложения
    /// </summary>
    private IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDal();
        services.AddLogic();
        
        services.AddScoped<IManageUserUseCase, ManageUserUseCase>();
        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskHub Api",
                Version = "v1"
            });
        });
    }

    /// <summary>
    /// Конфигурация middleware пайплайна
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskHub API v1");
            });
        }

        app.UseRouting();

        app.Use(async (content, next) =>
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            content.Response.OnStarting(() =>
            {
                stopwatch.Stop();

                content.Response.Headers.Append("X-Response-time-Ms", stopwatch.ElapsedMilliseconds.ToString());

                return Task.CompletedTask;
            });

            await next(); 
        });

        app.Use(async (content, next) =>
        {
            content.Response.Headers.Append("X-Student-Name", "Mannapov Kamil Aidarovich");
            content.Response.Headers.Append("X-Student-Group", "RI-240946");
            await next();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}