using System;

class Customer
{
    private string _name;
    private Address _userAddress;
    public Customer(string name, Address userAddress)
    {
        _name = name;
        _userAddress = userAddress;
    }

    public bool IsFromUS()
    {
        return _userAddress.IsExportation();
    }

    public string CustomerName()
    {
        return _name;
    }

    public string CustomerAddress()
    {
        return _userAddress.FullAddress();
    }
}