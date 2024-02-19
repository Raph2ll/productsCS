using MySql.Data.MySqlClient;

public class Product
{
    private readonly string Host;
    private readonly string Database;
    private readonly string Username;
    private readonly string Password;

    public Product(string host, string database, string username, string password)
    {
        Host = host;
        Database = database;
        Username = username;
        Password = password;
    }

    public MySqlConnection GetConnection()
    {
        string connectionString = $"Server={Host};Database={Database};User ID={Username};Password={Password};";
        return new MySqlConnection(connectionString);
    }

    public void CreateDatabase()
    {
        using (MySqlConnection connection = new MySqlConnection($"Server={Host};User ID={Username};Password={Password};"))
        {
            connection.Open();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {Database}";
                cmd.ExecuteNonQuery();
            }
        }
    }

   public void CreateProductTable()
    {
        using (MySqlConnection connection = GetConnection())
        {
            connection.Open();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Products (
                                        Id INT PRIMARY KEY AUTO_INCREMENT,
                                        Name VARCHAR(255) NOT NULL,
                                        Price DECIMAL(10, 2) NOT NULL
                                    )";
                cmd.ExecuteNonQuery();

                Console.WriteLine("Tabela Product criada com sucesso.");
            }
        }
    }
}


