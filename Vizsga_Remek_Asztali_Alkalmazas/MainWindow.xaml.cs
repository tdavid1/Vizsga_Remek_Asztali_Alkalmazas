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
using System.Drawing;
using Org.BouncyCastle.Asn1.X509;
using System.Security;
using SimpleHashing.Net;
using System.Windows.Controls.Primitives;

namespace Vizsga_Remek_Asztali_Alkalmazas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        ProductsService productsService;
        CostumerService costumerService;
        int actual_page;
        ISimpleHash simpleHash;
        bool isloged= false;
        public MainWindow()
        {
            InitializeComponent();
            this.productsService = new ProductsService();
            this.costumerService = new CostumerService();
            this.simpleHash = new SimpleHash();
            actual_page = 1;
            counter.Text = productsService.GetAll().Count + " Termék Van";
            products_Read(actual_page);
            costumer_Read(actual_page);
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
        //-------------------------------------------------------------
        //Oldalsávok közti mozgás és elrejtése
        private void main_page(object sender, RoutedEventArgs e)
        {
            if (!isloged)
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
            if (!isloged)
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
        //-------------------------------------------------------------
        //A táblák közöti mozgás
        private void move_products_table(object sender, RoutedEventArgs e)
        {
            Brush color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 11, 58, 188));
            products_button.BorderBrush = color;
            costumer_button.BorderBrush = Brushes.Transparent;
            produtcTable.Visibility = Visibility.Visible;
            costumerTable.Visibility = Visibility.Collapsed;
            counter.Text=productsService.GetAll().Count+" Termék Van";
            title.Text = "Termékek";
        }
        private void move_costumer_table(object sender, RoutedEventArgs e)
        {
            Brush color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 11, 58, 188));
            costumer_button.BorderBrush = color;
            products_button.BorderBrush = Brushes.Transparent;
            produtcTable.Visibility = Visibility.Collapsed;
            costumerTable.Visibility = Visibility.Visible;
            counter.Text = costumerService.GetAll().Count + " Felhasználó Van";
            title.Text = "Felhasználók";
        }
        //-------------------------------------------------------------
        //Táblák Feltöltése
        private void products_Read(int page_number)
        {
            List<Products> list = productsService.GetAll();
            List<Products> completlist = new List<Products>();
            int number = page_number * 10;
            int i = 1;
            foreach (Products product in list)
            {
                if (i > number - 10)
                {
                    completlist.Add(product);
                }
            }
            produtcTable.ItemsSource = completlist;
        }
        private void costumer_Read(int page_number)
        {
            List<Costumer> list = costumerService.GetAll();
            List<Costumer> completlist = new List<Costumer>();
            int number = page_number * 10;
            int i = 1;
            foreach (Costumer costumer in list)
            {
                if (i > number - 10)
                {
                    completlist.Add(costumer);
                }
            }
            costumerTable.ItemsSource = completlist;
        }
        //---------------------------------------------------------------
        //Alsó sáv gombok
        private void Move_Button(object sender, RoutedEventArgs e)
        {
            
            Brush color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 11, 58, 188));
            Button button = (sender as Button);
            string content = button.Content.ToString();
            int helper = int.Parse(content);
            if (content == "1")
            {
                actual_page = 1;
                products_Read(helper);
                costumer_Read(helper);
                Button_1.Content = "1";
                Button_1.Background = color;
                Button_2.Content = "2";
                Button_2.Background = Brushes.Transparent;
                Button_3.Content = "3";
                Button_3.Background = Brushes.Transparent;
                Button_4.Content = "4";
                Button_4.Background = Brushes.Transparent;
                Button_5.Content = "5";
                Button_5.Background = Brushes.Transparent;
            }
            else if (content== "2")
            {
                actual_page = 2;
                products_Read(helper);
                costumer_Read(helper);
                Button_1.Content = "1";
                Button_1.Background = Brushes.Transparent;
                Button_2.Content = "2";
                Button_2.Background = color;
                Button_3.Content = "3";
                Button_3.Background = Brushes.Transparent;
                Button_4.Content = "4";
                Button_4.Background = Brushes.Transparent;
                Button_5.Content = "5";
                Button_5.Background = Brushes.Transparent;
            }
            else
            {
                actual_page = helper;
                products_Read(helper);
                costumer_Read(helper);
                Button_1.Content = (helper-2).ToString();
                Button_1.Background = Brushes.Transparent;
                Button_2.Content = (helper -1).ToString();
                Button_2.Background = Brushes.Transparent;
                Button_3.Content = content;
                Button_3.Background = color;
                Button_4.Content = (helper + 1).ToString();
                Button_4.Background = Brushes.Transparent;
                Button_5.Content = (helper + 2).ToString();
                Button_5.Background = Brushes.Transparent;
            }
        }
        private void Page_minusz(object sender, RoutedEventArgs e)
        {
            Brush color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 11, 58, 188));
            if (actual_page-1 == 0)
            {
                MessageBox.Show("Az oldalszám nem lehet 0 vagy kissebb 0-nál.");
            }
            else
            {
                actual_page--;
                products_Read(actual_page);
                costumer_Read(actual_page);
                if(actual_page == 1)
                {
                    Button_1.Content = "1";
                    Button_1.Background = color;
                    Button_2.Content = "2";
                    Button_2.Background = Brushes.Transparent;
                    Button_3.Content = "3";
                    Button_3.Background = Brushes.Transparent;
                    Button_4.Content = "4";
                    Button_4.Background = Brushes.Transparent;
                    Button_5.Content = "5";
                    Button_5.Background = Brushes.Transparent;
                }
                else if (actual_page == 2)
                {
                    Button_1.Content = "1";
                    Button_1.Background = Brushes.Transparent;
                    Button_2.Content = "2";
                    Button_2.Background = color;
                    Button_3.Content = "3";
                    Button_3.Background = Brushes.Transparent;
                    Button_4.Content = "4";
                    Button_4.Background = Brushes.Transparent;
                    Button_5.Content = "5";
                    Button_5.Background = Brushes.Transparent;
                }
                else
                {
                    Button_1.Content = (actual_page - 2).ToString();
                    Button_1.Background = Brushes.Transparent;
                    Button_2.Content = (actual_page - 1).ToString();
                    Button_2.Background = Brushes.Transparent;
                    Button_3.Content = actual_page.ToString();
                    Button_3.Background = color;
                    Button_4.Content = (actual_page + 1).ToString();
                    Button_4.Background = Brushes.Transparent;
                    Button_5.Content = (actual_page + 2).ToString();
                    Button_5.Background = Brushes.Transparent;
                }
            }
        }
        private void Page_plusz(object sender, RoutedEventArgs e)
        {
            Brush color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 11, 58, 188));
            actual_page++;
            products_Read(actual_page);
            costumer_Read(actual_page);
            if (actual_page == 1)
            {
                Button_1.Content = "1";
                Button_1.Background = color;
                Button_2.Content = "2";
                Button_2.Background = Brushes.Transparent;
                Button_3.Content = "3";
                Button_3.Background = Brushes.Transparent;
                Button_4.Content = "4";
                Button_4.Background = Brushes.Transparent;
                Button_5.Content = "5";
                Button_5.Background = Brushes.Transparent;
            }
            else if (actual_page == 2)
            {
                Button_1.Content = "1";
                Button_1.Background = Brushes.Transparent;
                Button_2.Content = "2";
                Button_2.Background = color;
                Button_3.Content = "3";
                Button_3.Background = Brushes.Transparent;
                Button_4.Content = "4";
                Button_4.Background = Brushes.Transparent;
                Button_5.Content = "5";
                Button_5.Background = Brushes.Transparent;
            }
            else
            {
                Button_1.Content = (actual_page - 2).ToString();
                Button_1.Background = Brushes.Transparent;
                Button_2.Content = (actual_page - 1).ToString();
                Button_2.Background = Brushes.Transparent;
                Button_3.Content = actual_page.ToString();
                Button_3.Background = color;
                Button_4.Content = (actual_page + 1).ToString();
                Button_4.Background = Brushes.Transparent;
                Button_5.Content = (actual_page + 2).ToString();
                Button_5.Background = Brushes.Transparent;
            }
        }
        //-------------------------------------------------------------
        //Táblázat elemek Törlése és modositása
        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            
            foreach (var item in produtcTable.ItemsSource)
            {
                MessageBox.Show(item.ToString());
            }
        }
        private void Modify(object sender, RoutedEventArgs e)
        {

        }
        //------------------------------------------------------------
        //Bejelentkezés/Logout
        private void Login_Button(object sender, RoutedEventArgs e)
        {
            string username = user_name.Text;
            string jelszo = password.Password.ToString();
            List<Costumer> user = costumerService.GetAll();
            foreach (var item in user)
            {
                bool isValidPassword = simpleHash.Verify(jelszo, item.Password);
                if (username == item.Name && isValidPassword && item.Privilage == "Admin")
                {
                    isloged= true;
                    Login_page.Visibility = Visibility.Collapsed;
                    Main_page.Visibility = Visibility.Visible;
                    user_name.Text = "";
                    password.Password = "";
                }
            }
            if (!isloged)
            {
                MessageBox.Show("Nem sikerült a bejelentkezés");
            }
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            if (isloged)
            {
                MessageBoxResult selectedButton =
                MessageBox.Show($"Biztos ki szeretne jelentkezni?",
                    "Biztos?", MessageBoxButton.YesNo);
                if (selectedButton == MessageBoxResult.Yes)
                {
                    Login_page.Visibility = Visibility.Visible;
                    Main_page.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Előbb be kell jelentkezni hogy ki tudjon jelentkezni");
            }
        }
    }
}