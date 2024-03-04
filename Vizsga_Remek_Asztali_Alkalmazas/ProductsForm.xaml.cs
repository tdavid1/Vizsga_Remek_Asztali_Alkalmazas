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
    /// Interaction logic for ProductsForm.xaml
    /// </summary>
    public partial class ProductsForm : Window
    {
        private ProductsService productsService;
        private Products productTo;
        public ProductsForm(ProductsService productsService)
        {
            InitializeComponent();
            this.productsService = productsService;
        }
        public ProductsForm(ProductsService productsService, Products productToU)
        {
            InitializeComponent();
            this.productTo = productToU;
            this.productsService = productsService;
            text_cim.Text = "Termék modositása";
            add_btn.Visibility = System.Windows.Visibility.Collapsed;
            mod_btn.Visibility = System.Windows.Visibility.Visible;
            add_name.Text = productTo.Products_name;
            add_price.Text = productTo.Price.ToString();
            add_desc.Text = productTo.Description;
            add_spectype.Text = productTo.SpecType;
            add_type.Text = productTo.Type;
        }

        private void add_products(object sender, RoutedEventArgs e)
        {
            try
            {
                Products product = CreateProducts();
                if (productsService.Create(product))
                {
                    MessageBox.Show("Sikeres hozzáadás");
                    add_name.Text = "";
                    add_price.Text = "";
                    add_desc.Text = "";
                    add_spectype.Text = "";
                    add_type.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a hozzáadás során");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private Products CreateProducts()
        {
            string name = add_name.Text.Trim();
            string priceText = add_price.Text.Trim();
            string desc = add_desc.Text.Trim();
            string spectype = add_spectype.Text.Trim();
            string type = add_type.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Név megadása kötelező");
            }
            if (string.IsNullOrEmpty(type))
            {
                throw new Exception("Típus megadása kötelező");
            }
            if (string.IsNullOrEmpty(spectype))
            {
                throw new Exception("Specifikus Típus megadása kötelező");
            }
            if (string.IsNullOrEmpty(priceText))
            {
                throw new Exception("Ár megadása kötelező");
            }
            if (!int.TryParse(priceText, out int price))
            {
                throw new Exception("Ár csak szám lehet");
            }
            if (string.IsNullOrEmpty(desc))
            {
                throw new Exception("Leírás megadása kötelező");
            }
            if (price < 0)
            {
                throw new Exception("Az Ár nem lehet negatív");
            }
            Products products = new Products();
            products.Products_name = name;
            products.Price = price;
            products.Description = desc;
            products.SpecType = spectype;
            products.Type = type;
            return products;
        }

        private void modify(object sender, RoutedEventArgs e)
        {
            try
            {
                Products product = CreateProducts();
                MessageBox.Show((productsService.Update(productTo.Id, product)).ToString());
                if (productsService.Update(productTo.Id, product))
                {
                    MessageBox.Show("Sikeres módosítás");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során! Javasoljuk az ablak bezárását!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
