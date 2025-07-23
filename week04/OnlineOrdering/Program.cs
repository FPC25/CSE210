using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the OnlineOrdering Project.");

        //declaring the products
        Product banana = new Product("Cacho de Banana", "f011221", 10, 1);
        Product chocolate = new Product("Barra de chocolate", "d123211", 5, 5);
        Product apple = new Product("Maçã Gala", "f011222", 8.50f, 2);
        Product bread = new Product("Pão Integral", "b321100", 3.25f, 4);
        Product milk = new Product("Leite Desnatado", "l555888", 2.99f, 6);
        Product cheese = new Product("Queijo Minas", "q777999", 12, 1);
        Product coffee = new Product("Café Torrado", "c888777", 15, 1);

        //creating a list with all products
        List<Product> allProducts = new List<Product> { banana, chocolate, apple, bread, milk, cheese, coffee };

        //randomly selecting products of each list
        Random rnd = new Random();

        List<Product> shuffled = allProducts.OrderBy(x => rnd.Next()).ToList();

        // Select 2 or 3 products for each list
        int count1 = rnd.Next(2, 4);
        int count2 = rnd.Next(2, 4);

        //selecting 2 or 3 products to the first, then selecting others for the second list, ignoring the ones that were chosen to the first list
        List<Product> list1 = shuffled.Take(count1).ToList();
        List<Product> list2 = shuffled.Skip(count1).Take(count2).ToList();

        //Debugging messages to see the products list
        //Utils.PrintProductList(list1);
        //Utils.PrintProductList(list2);

        // Creating two addresses, one in the US, other outside it 
        Address inUS = new Address("123 Main St", "Springfield", "IL", "USA");
        Address notUS = new Address("456 Queen St", "Toronto", "ON", "Canada");

        // Creating two customers
        Customer nathan = new Customer("Nathan", notUS);
        Customer phillip = new Customer("Phillip", inUS);

        //Creating Orders for the clients
        List<Order> orders = new List<Order>() { new Order(nathan, list1), new Order(phillip, list2) };

        string shipping, packing;
        float total;
        //Console.Clear();
        foreach (Order order in orders)
        {
            shipping = order.ShippingLabel();
            packing = order.PackingLabel();
            total = order.TotalPrice();

            Console.WriteLine(shipping);
            Console.WriteLine(packing);
            Console.WriteLine($"total: ${total}");
            Console.WriteLine();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine();
        }

    }
}