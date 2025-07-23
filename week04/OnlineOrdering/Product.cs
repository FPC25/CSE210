using System;

class Product
{
    private int _quantity;
    private float _price;
    private string _name, _id;
    public Product(string name, string id, float price, int quantity)
    {
        _name = name;
        _id = id;
        _price = price;
        _quantity = quantity;
    }

    public string GetProductName()
    {
        //getter to the product name
        return _name;
    }

    public string GetProductId()
    {
        //getter to the product id
        return _id;
    }

    public float TotalCostPerProduct()
    {
        //returns the full price of a given amount of a product 
        return _price * _quantity;
    }
}