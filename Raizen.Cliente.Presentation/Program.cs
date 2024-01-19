

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Raizen.Cliente.Application.Contracts;
using Raizen.Cliente.Application.Services;
using Raizen.Cliente.Common.InfraStructure.DbConnectionFactories;
using Raizen.Cliente.Domain.Repositories;
using Raizen.Cliente.InfraStrucuture.Repositories;
using Raizen.Cliente.Presentation.Validators;

internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews()
                                .AddRazorOptions(options =>
                                {
                                    options.ViewLocationFormats.Add("/{0}.cshtml");
                                });
       

        var appFileConfig = "appsettings.json";

        var configuration = new ConfigurationBuilder()
                           .AddJsonFile(appFileConfig)
                           .Build();

        var connectionString = configuration.GetValue<string>("ConnectionStrings:ConnectionStringDB");

        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(connectionString));

        ConfigRepositoriesDependencyInjection(builder);

        ConfigServicesDependencyInjection(builder);

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddHttpClient();

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.WriteIndented = true;
        });


        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddScoped<IValidator<ClienteModel>, ClienteModelValidator>();

      
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

       

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Cliente}/{action=Index}/{id?}");

        app.Run();



    }

    private static void ConfigRepositoriesDependencyInjection(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
    }

    private static void ConfigServicesDependencyInjection(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IClienteService, ClienteService>();
       
    }



}

