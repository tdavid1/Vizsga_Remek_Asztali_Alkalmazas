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
        public ProductsForm(ProductsService productsService)
        {
            InitializeComponent();
            this.productsService = productsService;
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
                    add_pic.Text = "";
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
        }

        private Products CreateProducts()
        {
            string name = add_name.Text.Trim();
            string priceText = add_price.Text.Trim();
            string desc = add_desc.Text.Trim();
            string pic = add_pic.Text.Trim();
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
            if (string.IsNullOrEmpty(priceText))
            {
                throw new Exception("Ár megadása kötelező");
            }
            if (!int.TryParse(priceText, out int price))
            {
                throw new Exception("Kor csak szám lehet");
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
            products.Picture = pic;
            products.SpecType = spectype;
            products.Type = type;
            return products;
        }
    }
}
