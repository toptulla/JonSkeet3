using System.Collections.Generic;

namespace JonSkeetBook.Co_1_1_Evolution
{
    // + List<Product> указывает компилятору, что список содержит продукты.
    // Попытка добавить в список другой тип привела бы к ошибке во время
    // компиляции, и нам не придется приводить результаты при их получении из списка.

    // + Теперь у нас есть свойства с закрытыми методами установки значения.

    // - В коде создания свойств и переменных много воды, что усложняет простую задачу 
    // инкапсуляции строки и десятичного числа.

    class ProductC2
    {
        private string _name;
        public string Name {
            get { return _name; }
            private set { _name = value; }
        }
        private decimal _price;
        public decimal Price {
            get { return _price; }
            private set { _price = value; }
        }

        public ProductC2(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        private static List<ProductC2> GetSampleProducts()
        {
            List<ProductC2> list = new List<ProductC2>();
            list.Add(new ProductC2("West side story", 9.99m));
            list.Add(new ProductC2("Assassins", 14.99m));
            list.Add(new ProductC2("Frogs", 13.99m));
            list.Add(new ProductC2("Sweeney Todd", 10.99m));
            return list;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", _name, _price);
        }
    }
}
