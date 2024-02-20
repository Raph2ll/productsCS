using System;
using MySql.Data.MySqlClient;

namespace api.Data;
public class Connection
{
    private readonly string Host;
    private readonly string Username;
    private readonly string Password;
    private readonly string Database;

    public Connection(string host, string username, string password, string database)
    {
        Host = host;
        Username = username;
        Password = password;
        Database = database;
    }

    public MySqlConnection GetConnection()
    {
        string connectionString = $"Server={Host};User ID={Username};Password={Password};";
        return new MySqlConnection(connectionString);
    }
    public void CreateDatabase()
    {
        MySqlConnection mysqlConnection = GetConnection();
        {
            mysqlConnection.Open();

            using (MySqlCommand cmd = mysqlConnection.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {Database}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"USE {Database}";
                cmd.ExecuteNonQuery();

                // Products
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS products (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    Name VARCHAR(255) NOT NULL,
                    Price FLOAT(10, 2) NOT NULL
                )";
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertDatabase()
    {
        MySqlConnection mysqlConnection = GetConnection();
        {
            mysqlConnection.Open();

            using (MySqlCommand cmd = mysqlConnection.CreateCommand())
            {

                cmd.CommandText = $"USE {Database}";
                cmd.ExecuteNonQuery();

                // Products
                cmd.CommandText = @"
                    INSERT INTO products (Name, Price) VALUES
                        ('Abacate', 2.49),
                        ('Cadeira de Escritório', 79.99),
                        ('Teclado Mecânico', 69.99),
                        ('Uva', 3.99),
                        ('Impressora Multifuncional', 129.99),
                        ('Tablet ', 199.99),
                        ('Pêra', 1.79),
                        ('Cafeteira Elétrica', 39.99),
                        ('Caixa de Som Bluetooth', 34.99),
                        ('Melancia', 5.99),
                        ('Monitor 24 polegadas', 149.99),
                        ('Mochila para Notebook', 29.99),
                        ('Morango', 2.99),
                        ('Ventilador de Torre', 59.99),
                        ('Mousepad Gamer', 9.99),
                        ('Kiwi', 3.49),
                        ('Aspirador de Pó', 89.99),
                        ('Headset Gamer', 79.99),
                        ('Melão', 4.99),
                        ('Cadeira Gamer', 199.99),
                        ('Câmera de Segurança', 49.99),
                        ('Pêssego', 2.29),
                        ('Liquidificador', 29.99),
                        ('Fone de Ouvido com Cancelamento de Ruído', 149.99),
                        ('Ameixa', 1.99),
                        ('Console de Videogame', 299.99),
                        ('Tapete de Yoga', 19.99),
                        ('Notebook ', 799.99),
                        ('Coco', 3.29)";
                cmd.ExecuteNonQuery();

            }
        }
    }
}
