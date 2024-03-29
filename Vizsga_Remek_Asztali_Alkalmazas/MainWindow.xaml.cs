﻿using System.Text;
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
        
        public ProductsService productsService;
        CostumerService costumerService;
        int actual_page;
        ISimpleHash simpleHash;
        bool isloged= false;
        bool isprod = true;
        public MainWindow()
        {
            InitializeComponent();
            this.productsService = new ProductsService();
            this.costumerService = new CostumerService();
            this.simpleHash = new SimpleHash();
            actual_page = 1;
            productcounter();
            products_Read(actual_page);
            costumer_Read(actual_page);
        }
        //----------------------------------------------------------------
        //Számlálok
        private void productcounter()
        {
            counter.Text = productsService.GetAll().Count + " Termék Van";
        }
        private void costumercounter()
        {
            counter.Text = costumerService.GetAll().Count + " Felhasználo van";
        }
        //-------------------------------------------------------------------
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
            productcounter();
            title.Text = "Termékek";
            isprod = true;
            actual_page = 1;
            products_Read(actual_page);
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
        private void move_costumer_table(object sender, RoutedEventArgs e)
        {
            Brush color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 11, 58, 188));
            costumer_button.BorderBrush = color;
            products_button.BorderBrush = Brushes.Transparent;
            produtcTable.Visibility = Visibility.Collapsed;
            costumerTable.Visibility = Visibility.Visible;
            title.Text = "Felhasználók";
            isprod = false;
            actual_page = 1;
            costumercounter();
            costumer_Read(actual_page);
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
        //-------------------------------------------------------------
        //Táblák Feltöltése
        public void products_Read(int page_number)
        {
            if (Maximum)
            {
                //List<Products> list = productsService.GetAll();
                //List<Products> completlist = new List<Products>();
                //int number = page_number * 20;
                //int i = 1;
                //foreach (Products product in list)
                //{
                //    if (i > number - 20 && i < number + 1)
                //    {
                //        completlist.Add(product);
                //    }
                //    i++;
                //}
                //produtcTable.ItemsSource = completlist;
            }
            else
            {
                List<Products> list = productsService.GetAll();
                List<Products> completlist = new List<Products>();
                int number = page_number * 8;
                int i = 1;
                foreach (Products product in list)
                {
                    if (i > number - 8 && i < number + 1)
                    {
                        completlist.Add(product);
                    }
                    i++;
                }
                produtcTable.ItemsSource = completlist;
            }
        }
        private void costumer_Read(int page_number)
        {
            if (Maximum)
            {
                //List<Costumer> list = costumerService.GetAll();
                //List<Costumer> completlist = new List<Costumer>();
                //int number = page_number * 20;
                //int i = 1;
                //foreach (Costumer costumer in list)
                //{
                //    if (i > number - 20 && i < number + 1)
                //    {
                //        completlist.Add(costumer);
                //    }
                //    i++;
                //}
                //costumerTable.ItemsSource = completlist;
            }
            else
            {
                List<Costumer> list = costumerService.GetAll();
                List<Costumer> completlist = new List<Costumer>();
                int number = page_number * 8;
                int i = 1;
                foreach (Costumer costumer in list)
                {
                    if (i > number - 8 && i < number + 1)
                    {
                        completlist.Add(costumer);
                    }
                    i++;
                }
                costumerTable.ItemsSource = completlist;
            }
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
            List<Products> list = productsService.GetAll();
            foreach (var item in list)
            {
                if (button.Tag.ToString() == item.Id.ToString())
                {
                    MessageBoxResult selectedButton =
                MessageBox.Show($"Biztos, hogy törölni szeretné ezt a terméket: {item.Products_name}?",
                    "Biztos?", MessageBoxButton.YesNo);
                    if (selectedButton == MessageBoxResult.Yes)
                    {
                        if (productsService.Delete(item.Id))
                        {
                            MessageBox.Show("Sikeres törlés!");
                            productcounter();
                        }
                        else
                        {
                            MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található");
                        }
                        products_Read(actual_page);
                    }
                }
            }
        }
        private void Delete_cost(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            List<Costumer> list = costumerService.GetAll();
            foreach (var item in list)
            {
                MessageBox.Show((button.Tag.ToString() == item.Id_Costumer.ToString()).ToString());
                if (button.Tag.ToString() == item.Id_Costumer.ToString())
                {
                    if (item.Privilage == "Admin")
                    {
                        MessageBox.Show("Admin fiokot nem lehet törölni");
                    }
                    else
                    {
                        MessageBoxResult selectedButton =
                MessageBox.Show($"Biztos, hogy törölni szeretné ezt a fiokot: {item.Name}?",
                    "Biztos?", MessageBoxButton.YesNo);
                        if (selectedButton == MessageBoxResult.Yes)
                        {
                            if (costumerService.Delete(item.Id_Costumer))
                            {
                                MessageBox.Show("Sikeres törlés!");
                                costumercounter();
                            }
                            else
                            {
                                MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található");
                            }
                            costumer_Read(actual_page);
                        }
                    }
                }
            }
        }
        private void Modify(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            List<Products> list = productsService.GetAll();
            foreach (var item in list)
            {
                if (button.Tag.ToString() == item.Id.ToString())
                {
                    ProductsForm form = new ProductsForm(productsService,item);
                    form.Closed += (_, _) =>
                    {
                        products_Read(actual_page);
                    };
                    form.ShowDialog();
                }
            }
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
                login_btn.Visibility = Visibility.Collapsed;
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
                login_btn.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Előbb be kell jelentkezni hogy ki tudjon jelentkezni");
            }
        }
        //-----------------------------------------------------------
        //Keresés
        private void Seartch_byname(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Search.Text);
        }
        private void Seartch_byid(object sender, RoutedEventArgs e)
        {
            if(isprod==true && Search_list.Text == "")
            {
                products_Read(actual_page);
            }
            else if(isprod == false && Search_list.Text == "")
            {
                costumer_Read(actual_page);
            }
            else if(isprod == true)
            {
                List<Products> all = new List<Products>();
                string name = Search_list.Text;
                List<Products> list_p = productsService.GetAll();
                foreach (Products p in list_p)
                {
                    if (p.Products_name.Contains(name))
                    {
                        all.Add(p);
                    }
                }
                produtcTable.ItemsSource = all;
            }
            else if(isprod== false)
            {
                List<Costumer> all = new List<Costumer>();
                string name = Search_list.Text;
                List<Costumer> list_c = costumerService.GetAll();
                foreach (Costumer c in list_c)
                {
                    if (c.Name.Contains(name))
                    {
                        all.Add(c);
                    }
                }
                costumerTable.ItemsSource = all;
            }
        }
        //-----------------------------------------------------------------
        //Termék Hozzáaddása
        private void add_button(object sender, RoutedEventArgs e)
        {
            ProductsForm form = new ProductsForm(productsService);
            form.Closed += (_, _) =>
            {
                products_Read(actual_page);
                productcounter();
            };
            form.ShowDialog();
        }

        private void Picture(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            string id = button.Tag.ToString();
            PictureAddForm form = new PictureAddForm(id);
            form.Closed += (_, _) =>
            {
                products_Read(actual_page);
                productcounter();
            };
            form.ShowDialog();
        }
    }
}