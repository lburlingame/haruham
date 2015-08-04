using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class ViewShopItem : System.Web.UI.Page
{

    private int current;
    private List<ShopItem> shopItems;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["current"] == null || Session["shopItems"] == null)
        {
            Response.Redirect("TeaShop.aspx");
        }
        

        itemTable.CellSpacing = 45;

        shopItems = (List<ShopItem>)Session["shopItems"];
        current = (int)Session["current"];
        ShopItem currItem = shopItems[current];
            
        TableRow row = new TableRow();

        TableCell cell0 = new TableCell();
        TableCell cell1 = new TableCell();
        TableCell cell2 = new TableCell();
        TableCell cell3 = new TableCell();
        TableCell cell4 = new TableCell();

        cell1.Text = currItem.name + ", " + currItem.weight + " lbs";
        cell1.Font.Size = 20;
        cell2.Text = "Rating";
        cell3.Text = "Price";

        row.Cells.Add(cell0);
        row.Cells.Add(cell1);
        row.Cells.Add(cell2);
        row.Cells.Add(cell3);
        row.Cells.Add(cell4);

        itemTable.Rows.Add(row);

        row = new TableRow();

        cell0 = new TableCell();
        cell1 = new TableCell();
        cell2 = new TableCell();
        cell3 = new TableCell();
        cell4 = new TableCell();

        cell1.VerticalAlign = VerticalAlign.Top;
        cell2.VerticalAlign = VerticalAlign.Top;
        cell3.VerticalAlign = VerticalAlign.Top;
        cell4.VerticalAlign = VerticalAlign.Top;

        Image img = new Image();
        img.ImageUrl = currItem.fullURL;
        cell0.Controls.Add(img);

        cell1.Text = currItem.description;
        cell1.Width = 500;

        cell2.Text = currItem.rating + "/5";

        cell3.Text = "$" + currItem.price;

        TextBox quantityBox = new TextBox();
        quantityBox.CssClass = "textbox";
        Button addtocartButton = new Button();
        addtocartButton.CssClass = "button";
        quantityBox.Width = 42;
        quantityBox.Text = "1";
        cell4.Controls.Add(quantityBox);

        addtocartButton.Text = "Add to Cart";
        addtocartButton.Width = 85;
        addtocartButton.Click += new EventHandler(addtocartbutton_Click);
        cell4.Controls.Add(addtocartButton);

        row.Cells.Add(cell0);
        row.Cells.Add(cell1);
        row.Cells.Add(cell2);
        row.Cells.Add(cell3);
        row.Cells.Add(cell4);

        itemTable.Rows.Add(row);

        /*
        old way without table
        nameLabel.Text = currItem.name + ", " + currItem.weight + " lbs";
        descriptionLabel.Text = currItem.description;
        priceLabel.Text = "$" + currItem.price;
        ratingLabel.Text = currItem.rating + "/5";
        */
    }

    protected void addtocartbutton_Click(object sender, EventArgs e)
    {

        TableCell curr = itemTable.Rows[1].Cells[4];
        string inputqty = ((TextBox)curr.Controls[0]).Text.ToString();

        int quantity;
        bool valid = Int32.TryParse(inputqty, out quantity);

        if (valid)
        {
            if (quantity > 0)
            {
                List<ShopItem> cartItems = (List<ShopItem>)Session["cartItems"];
                if (cartItems.Find(item => item.id == shopItems[current].id) != null)
                {
                    cartItems.Find(item => item.id == shopItems[current].id).cartqty += quantity;
                }
                else
                {
                    shopItems[current].cartqty = quantity;
                    cartItems.Add(shopItems[current]);
                }
                Response.Redirect("ViewCart.aspx");
            }
            else
            {
                itemTable.Rows[0].Cells[4].Text = "Invalid Quantity";
            }
        }
        else
        {
            itemTable.Rows[0].Cells[4].Text = "Invalid Quantity";
        }
    }
}

