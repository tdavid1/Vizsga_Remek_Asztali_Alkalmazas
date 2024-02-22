using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Vizsga_Remek_Asztali_Alkalmazas
{
    public class ProductsService
    {
        MySqlConnection connection;
        public ProductsService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "vizsgaremek";

            connection = new MySqlConnection(builder.ConnectionString);
        }
        public List<Products> GetAll()
        {
            List<Products> products = new List<Products>();
            OpenConnection();
            string sql = "SELECT * FROM product";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Products product = new Products();
                    product.Id = reader.GetInt32("product_id");
                    product.Products_name = reader.GetString("product_name");
                    product.Price = reader.GetInt32("price");
                    product.Description = reader.GetString("description");
                    product.Type = reader.GetString("product_type");
                    products.Add(product);
                }
            }
            CloseConnection();
            return products;
        }
        public bool Create(Products product)
        {
            OpenConnection();
            string sql = "INSERT INTO product(product_name,price,description,product_pic,product_spectype,product_type) VALUES (@product_name,@price,@descreption,@product_pic,@spectype,@type)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@product_name", product.Products_name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@descreption",product.Description);
            command.Parameters.AddWithValue("@product_pic", product.Picture);
            command.Parameters.AddWithValue("@spectype", product.SpecType);
            command.Parameters.AddWithValue("@type", product.Type);

            int affectedRows = command.ExecuteNonQuery();

            CloseConnection();
            return affectedRows == 1;
        }
        public bool Delete(int id)
        {
            OpenConnection();
            string sql = "DELETE FROM product WHERE product_id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            return affectedRows == 1;
        }
        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
