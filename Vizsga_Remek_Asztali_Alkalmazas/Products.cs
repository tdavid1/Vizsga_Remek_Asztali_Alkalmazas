using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizsga_Remek_Asztali_Alkalmazas
{
    class Products
    {
        private int id;
        private string products;
        private string picture;
        private int price;
        private string description;

        public Products(int id, string products, string picture, int price, string description)
        {
            this.id = id;
            this.products = products;
            this.picture = picture;
            this.price = price;
            this.description = description;
        }

        public int Id { get => id; set => id = value; }
        public string Products_name { get => products; set => products = value; }
        public string Picture { get => picture; set => picture = value; }
        public int Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
    }
}
