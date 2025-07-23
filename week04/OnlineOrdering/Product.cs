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
        return _name;
    }

    public string GetProductId()
    {
        return _id;
    }

    public float TotalCostPerProduct()
    {
        return _price * _quantity;
    }
}