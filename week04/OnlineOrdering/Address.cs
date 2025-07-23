using System;

class Address
{
    private string _street, _city, _state, _country;
    public Address(string street, string city, string state, string country)
    {
        _street = street;
        _city = city;
        _state = state;
        _country = country;
    }

    public bool IsExportation()
    {
        //if the customer is from the US the return false, otherwise is true
        if (_country.Equals("USA") || _country.Equals("US") || _country.Equals("United States of America")) return false;
        return true;
    }

    public string FullAddress()
    {
        return $"{_street}, {_city}, {_state}, {_country}";
    }
}