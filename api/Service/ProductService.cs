using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using api.Data;
using api.Model;

public class ProductService
{
    private readonly Connection _connection;

    public ProductService(Connection connection)
    {
        _connection = connection;
    }

    public List<ProductModel> GetAllProducts()
    {
        var products = new List<ProductModel>();

        using (var dbConnection = _connection.GetConnection())
        {
            dbConnection.Open();

            using (var command = new MySqlCommand("SELECT * FROM Products", dbConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new ProductModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Price = reader["Price"] == DBNull.Value ? 0.0 : (double)reader["Price"]
                        };

                        products.Add(product);
                    }
                }
            }
        }

        return products;
    }
}
