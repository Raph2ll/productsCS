using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using api.Data;
using api.Model;
using System.Data.Common;

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
            using (var command = new MySqlCommand($"SELECT * FROM store.products", dbConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new ProductModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Price = reader["Price"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["Price"])
                        };
                        product.Price = Math.Round(product.Price, 2);

                        products.Add(product);
                    }
                }
            }
            dbConnection.Close();
        }

        return products;

    }
    public void CreateProducts(List<ProductModel> newProducts)
    {
        using (var dbConnection = _connection.GetConnection())
        {
            dbConnection.Open();
            foreach (var newProduct in newProducts)
            {
                using (var command = new MySqlCommand("INSERT INTO store.products(Name, Price) VALUES (@Name, @Price)", dbConnection))
                {
                    command.Parameters.AddWithValue("@Name", newProduct.Name);
                    command.Parameters.AddWithValue("@Price", newProduct.Price);

                    command.ExecuteNonQuery();
                }
            }
            dbConnection.Close();
        }
    }
    public void DeleteProductById(int productId)
    {
        using (var dbConnection = _connection.GetConnection())
        {
            dbConnection.Open();
            using (var command = new MySqlCommand("DELETE FROM store.products WHERE Id = @ProductId", dbConnection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateProduct(ProductModel updatedProduct)
    {
        using (var dbConnection = _connection.GetConnection())
        {
            dbConnection.Open();
            using (var command = new MySqlCommand("UPDATE store.products SET Name = @Name, Price = @Price WHERE Id = @Id", dbConnection))
            {
                command.Parameters.AddWithValue("@Id", updatedProduct.Id);
                command.Parameters.AddWithValue("@Name", updatedProduct.Name);
                command.Parameters.AddWithValue("@Price", updatedProduct.Price);

                command.ExecuteNonQuery();
            }
        }
    }

}