using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Data;
using api.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração de serviços, se necessário
        services.AddSingleton<Connection>(sp =>
        {
            // Configurar sua conexão com o banco de dados aqui
            var connection = new Connection("localhost", "root", "user123", "product");
            connection.CreateDatabase(); // Certifique-se de criar o banco de dados antes de usar a conexão
            return connection;
        });

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configurações de middleware, se necessário

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Bem-vindo à minha aplicação!");
            });

            endpoints.MapGet("/api/products", async context =>
            {
                // Lógica para obter produtos do banco de dados
                var connection = context.RequestServices.GetRequiredService<Connection>();
                var products = ObterProdutos(connection);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(products));
            });

            /*
            endpoints.MapPost("/api/products", async context =>
            {
                // Lógica para criar um novo produto
                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var newProduct = JsonConvert.DeserializeObject<ProductModel>(requestBody);
                CriarNovoProduto(newProduct);

                await context.Response.WriteAsync("Novo produto criado com sucesso!");
            });
            */
        });
    }

    private static List<ProductModel> ObterProdutos(Connection connection)
    {
        // Lógica para obter produtos do banco de dados
        using (var dbConnection = connection.GetConnection())
        {
            dbConnection.Open();
            // Implemente a lógica para obter produtos do banco de dados aqui
            // Exemplo usando Dapper:
            // var products = dbConnection.Query<ProductModel>("SELECT * FROM Products").ToList();
            return new List<ProductModel>(); // Substitua com a lista real de produtos
        }
    }

    private static void CriarNovoProduto(ProductModel newProduct, Connection connection)
    {
        // Lógica para criar um novo produto no banco de dados
        using (var dbConnection = connection.GetConnection())
        {
            dbConnection.Open();
            // Implemente a lógica para inserir um novo produto no banco de dados aqui
            // Exemplo usando Dapper:
            // dbConnection.Execute("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", newProduct);
        }
    }
}
