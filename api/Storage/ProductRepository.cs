using System;
using System.Data.Common;
using api.Data;
using api.Model;
using MySql.Data.MySqlClient;


namespace api.Storage
{
    public class ProductRepository : IProductRepository
    {
        private readonly Connection _connection;

        public ProductRepository(Connection connection)
        {
            _connection = connection;
        }

        public List<ProductModel> GetAll()
        {
            var products = new List<ProductModel>();

            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand($"SELECT Id, Name, Price FROM store.products", dbConnection))
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
            }

            return products;

        }

        public ProductModel GetById(int productId)
        {
            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();

                using (var command = new MySqlCommand($"SELECT Id, Name, Price FROM store.products WHERE Id = @ProductId", dbConnection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.IsDBNull(reader["Price"]) ? 0.0 : Convert.ToDouble(reader["Price"])
                            };
                        }
                    }
                }
            }

            return null;

        }

        public void Create(List<ProductModel> newProducts)
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
            }
        }

        public void Update(ProductModel updatedProduct)
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

        public void Delete(int productId)
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
    }
}