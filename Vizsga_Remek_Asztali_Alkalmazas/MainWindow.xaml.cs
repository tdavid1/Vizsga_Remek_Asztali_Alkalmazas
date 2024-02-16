using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vizsga_Remek_Asztali_Alkalmazas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Products> products;
        public MainWindow()
        {
            InitializeComponent();
            products = new List<Products>();
            products.Add(new Products(1, "1090", "igen", 1000, "egy jó videokártya"));
            produtcTable.ItemsSource = products;
        }
        //Oldal nagyitása és kicsinyitése
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool Maximum = false;
        private void Border_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if (Maximum)
                {
                    this.WindowState = WindowState.Normal;
                    this.Height = 720;
                    this.Width = 1080;

                    Maximum = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    Maximum = true;
                }
            }
        }
        //--------------------------------------------
        //Oldalsávok közti mozgás és elrejtése
        private bool Logged = false;
        private void main_page(object sender, RoutedEventArgs e)
        {
            if (!Logged)
            {
                MessageBox.Show("Be kell jelentkezni");
            }
            else
            {
                Main_page.Visibility = Visibility.Visible;
                Login_page.Visibility = Visibility.Collapsed;
            }
        }
        private void login(object sender, RoutedEventArgs e)
        {
            if (!Logged)
            {
                Main_page.Visibility = Visibility.Collapsed;
                Login_page.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Be vagy jelentkezve");
            }
        }
        private void slide_show(object sender, RoutedEventArgs e)
        {
            if (slide.Visibility == Visibility.Visible)
            {
                slide.Visibility = Visibility.Collapsed;
            }
            else 
            { 
                slide.Visibility = Visibility.Visible;
            }
        }
        //-----------------------------------------------------
        //A táblák közöti mozgás
        private void move_products_table(object sender, RoutedEventArgs e)
        {
            Brush color= new SolidColorBrush(Color.FromArgb(255, 11, 58, 188));
            products_button.BorderBrush = color;
            costumer_button.BorderBrush = Brushes.Transparent;
            produtcTable.Visibility = Visibility.Visible;
            costumerTable.Visibility = Visibility.Collapsed;
            counter.Text=products.Count.ToString()+" Termék Van";
        }

        private void move_costumer_table(object sender, RoutedEventArgs e)
        {
            Brush color = new SolidColorBrush(Color.FromArgb(255, 11, 58, 188));
            costumer_button.BorderBrush = color;
            products_button.BorderBrush = Brushes.Transparent;
            produtcTable.Visibility = Visibility.Collapsed;
            costumerTable.Visibility = Visibility.Visible;
            counter.Text = "-" + " Felhasználó Van";
        }
    }
}