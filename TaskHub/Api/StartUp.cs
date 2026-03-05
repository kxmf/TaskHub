using System.Diagnostics;
using Api.Extensions;
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

        services.AddBusinessLogic();

        services.AddAppCors();

        services.AddSwagger();
    }

    /// <summary>
    /// Конфигурация middleware пайплайна
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
            app.UseAppSwagger();

        app.UseRouting();

        app.UseResponseTimeHeader();
        app.UseStudentHeaders("Mannapov Kamil Aidarovich", "RI-240946");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}