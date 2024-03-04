using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vizsga_Remek_Asztali_Alkalmazas
{
    /// <summary>
    /// Interaction logic for PictureAddForm.xaml
    /// </summary>
    public partial class PictureAddForm : Window
    {
        MySqlConnection connection;
        int id;
        public PictureAddForm(string id)
        {
            InitializeComponent();
            this.id=int.Parse(id);
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "vizsgaremek";

            connection = new MySqlConnection(builder.ConnectionString);
        }

        private void picture_upload(object sender, RoutedEventArgs e)
        {
            OpenConnection();
            string sql = "INSERT INTO productpictures(image,productId) VALUES (@image,@productId)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@image", Picture.Text.ToString());
            command.Parameters.AddWithValue("@productId", id);

            int affectedRows = command.ExecuteNonQuery();

            CloseConnection();
            this.Close();
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
