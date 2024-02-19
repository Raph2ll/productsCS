using MySql.Data.MySqlClient;
using System;

public class Program
{
    static void Main()
    {
        string host = "localhost";
        string database = "product";
        string username = "root";
        string password = "user123";

        Product product = new Product(host, database, username, password);

        try
        {
            product.CreateDatabase();

            MySqlConnection mysqlConnection = product.GetConnection();
            mysqlConnection.Open();

            Console.WriteLine("Conexão bem-sucedida ao banco de dados MySQL.");

            // Criação da tabela
            product.CreateProductTable();

            mysqlConnection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }
    }
}
