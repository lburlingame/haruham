using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Customer
/// </summary>
public class Customer
{
    public String Email;
    public String Name;
    public String Street;
    public String City;
    public String State;
    public String Zip;

	public Customer(String email, String name, String street, String city, String state, String zip)
	{
	    this.Email = email;
	    this.Name = name;
	    this.Street = street;
	    this.City = city;
	    this.State = state;
	    this.Zip = zip;
	}

}