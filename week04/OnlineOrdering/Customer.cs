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

    public bool IsInUS()
    {
        //If the customer is outside the US than it is exportation (true for IsExportation) it returns false (to the IsInUS) and vice-versa
        return !_userAddress.IsExportation();
    }

    public string CustomerName()
    {
        // getter to the customer name
        return _name;
    }

    public string CustomerAddress()
    {
        //getter to the address, but only the full address
        return _userAddress.FullAddress();
    }
}