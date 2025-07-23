using System;
using System.Linq;
using System.Text;

class Order
{
    private Customer _customerInfo;
    private List<Product> _order;
    public Order(Customer customerInfo, List<Product> orderList)
    {
        _customerInfo = customerInfo;
        _order = orderList;
    }

    public int ShippingCost()
    {
        // if the customer is in the US than the shipping cost is $5, otherwise is $35
        if (_customerInfo.IsInUS()) return 5;
        return 35;
    }

    public float TotalPrice()
    {
        //using a aggregation linq method to sum all costs in the list with a lambda expression return the float with the value
        return _order.Sum(product => product.TotalCostPerProduct()) + ShippingCost();
    }

    public string PackingLabel()
    {
        //creating a new builder to the table
        StringBuilder tableOfProducts = new StringBuilder();

        // Table Constants
        int commonWidth = 20; //a common but not obligatory size
        int productWidth = commonWidth; //customable width for product name cell
        int idWidth = commonWidth; //customable width for product id cell
        int fullWidth = productWidth + idWidth + 3; //the full size for the table, summing all cells width + separators
        string separator = new string('-', fullWidth);

        //header of the table of products
        tableOfProducts.AppendLine(separator);
        tableOfProducts.AppendLine($"|{CenterText("Product", productWidth)}|{CenterText("ID", productWidth)}|");
        tableOfProducts.AppendLine(separator);

        //DataRows
        foreach (Product product in _order)
        {
            //adding a new for each product in the list and its separator
            tableOfProducts.AppendLine($"|{CenterText(product.GetProductName(), productWidth)}|{CenterText(product.GetProductId(), productWidth)}|");
            tableOfProducts.AppendLine(separator);
        }

        //return the table created as a string
        return tableOfProducts.ToString();
    }

    // Helper method to center text within a given width
    private string CenterText(string text, int width)
    {
        // if the string is wider than the available width create a substring that fits the size
        if (text.Length >= width)
            return text.Substring(0, width);
        
        //setting the padding in order to center the string
        int padding = width - text.Length;
        int leftPadding = padding / 2;
        int rightPadding = padding - leftPadding;
        
        //return a centered string 
        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }

    public string ShippingLabel()
    {
        // Method to return a string with the customer name and their address in the same place 
        return $"{_customerInfo.CustomerName()}\n{_customerInfo.CustomerAddress()}";

    }
}