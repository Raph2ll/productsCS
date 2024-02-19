using System;
using MySql.Data.MySqlClient;
using api.Data;

public class Program
{
    static void Main()
    {
        Connection connection = new Connection("localhost", "root", "user123", "product");

        try
        {
            MySqlConnection mysqlConnection = connection.GetConnection();
            connection.CreateDatabase();

            mysqlConnection.Open();

            Console.WriteLine("Conexão bem-sucedida ao banco de dados MySQL.");

           

            mysqlConnection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }
    }
}
