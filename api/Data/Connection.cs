using System;
using MySql.Data.MySqlClient;

namespace api.Data;
public class Connection
{
    private readonly string Host;
    private readonly string Username;
    private readonly string Password;
    private readonly string Database;

    public Connection(string host,  string username, string password, string database)
    {
        Host = host;
        Username = username;
        Password = password;
        Database = database;
    }

    public MySqlConnection GetConnection()
    {
        string connectionString = $"Server={Host};Database={Database};User ID={Username};Password={Password};";
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
                CREATE TABLE IF NOT EXISTS Products (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    Name VARCHAR(255) NOT NULL,
                    Price DECIMAL(10, 2) NOT NULL
                )";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
