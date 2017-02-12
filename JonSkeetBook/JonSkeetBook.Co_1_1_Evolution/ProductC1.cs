using System.Collections;

namespace JonSkeetBook.Co_1_1_Evolution
{
    // - Cписок ArrayList, во время компиляции, не имеет никакой информации о том,
    // что в нем будет находиться.

    // - Мы предоставили открытые свойства "получения значения" (getter), а это значит, 
    // что если бы мы захотели иметь соответствующие свойства "установки значения" 
    // (setter), то они также должны были бы быть открытыми.

    // - В коде создания свойств и переменных много воды, что усложняет простую задачу 
    // инкапсуляции строки и десятичного числа.

    class ProductC1
    {
        private string _name;
        public string Name { get { return _name; } }
        private decimal _price;
        public decimal Price { get { return _price; } }

        public ProductC1(string name, decimal price)
        {
            _name = name;
            _price = price;
        }

        private static ArrayList GetSampleProducts()
        {
            ArrayList list = new ArrayList();
            list.Add(new ProductC1("West side story", 9.99m));
            list.Add(new ProductC1("Assassins", 14.99m));
            list.Add(new ProductC1("Frogs", 13.99m));
            list.Add(new ProductC1("Sweeney Todd", 10.99m));
            return list;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", _name, _price);
        }
    }
}
