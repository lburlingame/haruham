using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ShopItem
{

    public int id;
    public string name;
    public string description;
    public double price;
    public string thumbURL;
    public string fullURL;
    public double rating;
    public double weight;
    public int cartqty = 0;

	public ShopItem(int id, string name, string description, double price, string thumbURL, string fullURL, double rating, double weight)
	{
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.thumbURL = thumbURL;
        this.fullURL = fullURL;
        this.rating = rating;
        this.weight = weight;
	}

    public string ToString()
    {
        return name;
    }
}