using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonSkeetBook.Co_1_1_Evolution
{
    class ProductC4
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private readonly decimal _price;
        public decimal Price { get { return _price; } }

        public ProductC4(string name, decimal price)
        {
            _name = name;
            _price = price;
        }

        private static List<ProductC4> GetSampleProducts()
        {
            return new List<ProductC4>
            {
                new ProductC4(name: "West side story", price: 9.99m),
                new ProductC4(name: "Assassins", price: 14.99m),
                new ProductC4(name: "Frogs", price: 13.99m),
                new ProductC4(name: "Sweeney Todd", price: 10.99m)
            };
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", _name, _price);
        }
    }
}
