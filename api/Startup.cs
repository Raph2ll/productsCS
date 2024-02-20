using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<Connection>(sp =>
        {
            // Configurar sua conexão com o banco de dados aqui
            var connection = new Connection("localhost", "root", "user123", "store");
            connection.CreateDatabase(); // Certifique-se de criar o banco de dados antes de usar a conexão
            connection.InsertDatabase();
            return connection;
        });

        services.AddSingleton<ProductService>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome");
            });

            endpoints.MapGet("/api/products", async context =>
            {
                var productService = context.RequestServices.GetRequiredService<ProductService>();
                var products = productService.GetAllProducts();
                await context.Response.WriteAsync(JsonConvert.SerializeObject(products));
            });

            endpoints.MapControllers();
        });
    }
}
