using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizsga_Remek_Asztali_Alkalmazas
{
    class CostumerService
    {
        MySqlConnection connection;
        public CostumerService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "vizsgaremek";

            connection = new MySqlConnection(builder.ConnectionString);
        }
        public List<Costumer> GetAll()
        {
            List<Costumer> costumers = new List<Costumer>();
            OpenConnection();
            string sql = "SELECT * FROM user";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Costumer costumer = new Costumer();
                    costumer.Id_Costumer = reader.GetInt32("user_id");
                    costumer.Name = reader.GetString("username");
                    costumer.Email = reader.GetString("email");
                    costumer.Password = reader.GetString("password");
                    if (reader.GetInt32("Privilages_priv_id") == 20)
                    {
                        costumer.Privilage = "User";
                    }
                    else if(reader.GetInt32("Privilages_priv_id") == 10)
                    {
                        costumer.Privilage = "Admin";
                    }
                    costumers.Add(costumer);
                }
            }
            CloseConnection();
            return costumers;
        }
        public bool Delete(int id)
        {
            OpenConnection();
            string sql = "DELETE FROM user WHERE user_id = @id";
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
