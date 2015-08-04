using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Customer
/// </summary>
public class Customer
{
    public string ID;
    public string Email;
    public bool Verified;
    public List<Address> Addresses;
     
    public Customer(string ID, string Email, bool Verified)
    {
        this.ID = ID;
        this.Email = Email;
        this.Verified = Verified;
        Addresses = new List<Address>();
    }

}