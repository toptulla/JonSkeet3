namespace JonSkeetBook.Co_1_1_Evolution
{
    class Program
    {
        static void Main()
        {
            SortingC2V2();

            Console.ReadLine();
        }

        private static void SortingC1()
        {
            ArrayList products = ProductC1.GetSampleProducts();
            products.Sort(new ProductC1NameComparer());
            foreach (ProductC1 product in products)
                Console.WriteLine(product);
        }

        private static void SortingC2V1()
        {
            List<ProductC2> products = ProductC2.GetSampleProducts();
            products.Sort(new ProductC2NameComparer());
            foreach (ProductC2 product in products)
                Console.WriteLine(product);
        }

        private static void SortingC2V2()
        {
            List<ProductC2> products = ProductC2.GetSampleProducts();
            products.Sort(delegate(ProductC2 p1, ProductC2 p2) { return String.Compare(p1.Name, p2.Name, StringComparison.CurrentCulture); });
            foreach (ProductC2 product in products)
                Console.WriteLine(product);
        }

        private static void SortingC3V1()
        {
            List<ProductC3> products = ProductC3.GetSampleProducts();
            products.Sort((p1, p2) => String.Compare(p1.Name, p2.Name, StringComparison.CurrentCulture) );
            foreach (ProductC3 product in products)
                Console.WriteLine(product);
        }

        private static void SortingC3V2()
        {
            List<ProductC3> products = ProductC3.GetSampleProducts();
            foreach (ProductC3 product in products.OrderBy(p => p.Name))
                Console.WriteLine(product);
        }
    }
}
