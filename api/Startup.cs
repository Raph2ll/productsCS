using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using api.Model;
using api.Storage;
using api.Controller;
using Microsoft.AspNetCore.Mvc.Filters;
using api.Controller.Filters;
using api.Service;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<Connection>(sp =>
        {
            var connection = new Connection("localhost", "root", "user123", "store");
            connection.CreateDatabase();
            connection.InsertDatabase();
            return connection;
        });

        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IProductService, ProductService>();
        
        services.AddControllers(options =>
        {
            options.Filters.Add(new ModelStateValidationFilter());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
