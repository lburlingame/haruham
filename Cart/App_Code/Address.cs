using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Address
/// </summary>
public class Address
{
    public string AddressName;
    public string Name;
    public string Street;
    public string City;
    public string State;
    public string Zip;

	public Address(string AddressName, string Name, string Street, string City, string State, string Zip)
	{
        this.AddressName = AddressName;
	    this.Name = Name;
	    this.Street = Street;
	    this.City = City;
	    this.State = State;
	    this.Zip = Zip;
	}
}