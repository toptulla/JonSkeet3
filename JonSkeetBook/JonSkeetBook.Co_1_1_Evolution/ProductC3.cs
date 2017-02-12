using System.Collections.Generic;

namespace JonSkeetBook.Co_1_1_Evolution
{
    // Автоматически реализуемые свойства (automatically implemented property) и  
    // упрощенная инициализация(simplified initialization).

    // У свойств теперь нет никакого кода (или видимых переменных!), и мы построили жестко 
    // заданный список совсем другим способом.Без переменных name и price, для доступа мы
    // вынуждены использовать в классе свойства повсюду, что улучшает его целостность.Теперь
    // у нас есть закрытый конструктор без параметров, обеспечивающий инициализацию на основе
    // свойств.В этом примере мы фактически полностью удалили открытый конструктор, и
    // никакой внешний код, очевидно, не сможет создать другие экземпляры продукта.

    class ProductC3
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public ProductC3(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        private ProductC3()
        {

        }

        private static List<ProductC3> GetSampleProducts()
        {
            return new List<ProductC3>
            {
                new ProductC3 { Name = "West side story", Price = 9.99m },
                new ProductC3 { Name = "Assassins", Price = 14.99m },
                new ProductC3 { Name = "Frogs", Price = 13.99m },
                new ProductC3 { Name = "Sweeney Todd", Price = 10.99m }
            };
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
}
