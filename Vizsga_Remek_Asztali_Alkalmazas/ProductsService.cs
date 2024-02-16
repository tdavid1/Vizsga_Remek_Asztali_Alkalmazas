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
    class ProductsService
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
                    product.Description = reader.GetString("descreption");
                    products.Add(product);
                }
            }
            CloseConnection();
            return products;
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
