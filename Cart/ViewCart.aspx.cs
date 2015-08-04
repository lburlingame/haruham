using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewCart : System.Web.UI.Page
{
    List<ShopItem> cartItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        updateCart();
    }

    protected void updateCart()
    {
        if (Session["cartItems"] == null)
        {
            lblStatus.Text = "Shopping Cart is Empty";
        }
        else
        {
            cartItems = (List<ShopItem>)Session["cartItems"];

            if (cartItems.Count == 0)
            {
                lblStatus.Text = "Shopping Cart is Empty";
            }
            else
            {
                int totalqty = 0;
                double totalprice = 0;
                double totalweight = 0;

                TableRow row = new TableRow();

                TableCell cell0 = new TableCell();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                TableCell cell5 = new TableCell();
                TableCell cell6 = new TableCell();


                cell1.Text = "PRODUCT NAME";
                cell2.Text = "UNIT PRICE";
                cell3.Text = "QTY";
                cell4.Text = "SUBTOTAL";
                cell5.Text = "WEIGHT";
                cell6.Text = "REMOVE";

                cell1.Width = 175;
                cell2.Width = 135;
                cell3.Width = 75;
                cell4.Width = 125;
                cell5.Width = 100;
                cell6.Width = 25;

                row.Cells.Add(cell0);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);

                cartTable.Rows.Add(row);

                for (int i = 0; i < cartItems.Count; i++)
                {
                    row = new TableRow();

                    cell0 = new TableCell();
                    cell1 = new TableCell();
                    cell2 = new TableCell();
                    cell3 = new TableCell();
                    cell4 = new TableCell();
                    cell5 = new TableCell();
                    cell6 = new TableCell();

                    ShopItem cartcurr = cartItems[i];

                    Image img = new Image();
                    img.ImageUrl = cartcurr.thumbURL;
                    img.Width = 75;
                    img.Height = 75;
                    cell0.Controls.Add(img);

                    cell1.Text = cartcurr.name;

                    cell2.Text = "$" + cartcurr.price.ToString();

                    TextBox quantity = new TextBox();
                    quantity.ID = (i+100).ToString();
                    quantity.Width = 45;
                    quantity.Text = cartcurr.cartqty.ToString();
                    cell3.Controls.Add(quantity);
                    totalqty += cartcurr.cartqty;

                    cell4.Text = "$" + (cartcurr.price * cartcurr.cartqty).ToString();
                    totalprice += cartcurr.price * cartcurr.cartqty;

                    cell5.Text = (cartcurr.weight * cartcurr.cartqty).ToString() + " lbs";
                    totalweight += cartcurr.weight * cartcurr.cartqty;

                    Button remove = new Button();
                    remove.Text = "X";
                    remove.Width = 25;
                    remove.Height = 25;
                    remove.ID = (i).ToString();
                    remove.CssClass = "remove_button";
                    remove.Click += new EventHandler(remove_Click);
                    cell6.Controls.Add(remove);

                    row.Cells.Add(cell0);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);
                    row.Cells.Add(cell6);

                    cartTable.Rows.Add(row);
                }

                row = new TableRow();

                cell0 = new TableCell();
                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                cell4 = new TableCell();
                cell5 = new TableCell();
                cell6 = new TableCell();

                cell3.Text = totalqty.ToString();
                cell4.Text = "$" + totalprice.ToString();
                cell5.Text = totalweight.ToString() + " lbs";

                row.Cells.Add(cell0);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);

                cartTable.Rows.Add(row);

                row = new TableRow();

                cell0 = new TableCell();
                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                cell4 = new TableCell();
                cell5 = new TableCell();
                cell6 = new TableCell();
                TableCell cell7 = new TableCell();

                Button update = new Button();
                update.Text = "Update";
                update.Width = 75;
                update.Height = 25;
                update.ID = "update";
                update.Click += new EventHandler(updateButton_Click);
                cell7.Controls.Add(update);

                row.Cells.Add(cell0);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);
                row.Cells.Add(cell7);

                cartTable.Rows.Add(row);

                row = new TableRow();

                cell0 = new TableCell();
                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                cell4 = new TableCell();
                cell5 = new TableCell();
                cell6 = new TableCell();
                cell7 = new TableCell();
                cell7.Height = 75;
                cell7.VerticalAlign = VerticalAlign.Bottom;

                Button checkout = new Button();
                checkout.Text = "Checkout";
                checkout.ID = "checkout";
                checkout.Width = 75;
                checkout.Height = 25;
                checkout.Click += new EventHandler(checkoutButton_Click);
                cell7.Controls.Add(checkout);

                row.Cells.Add(cell0);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);
                row.Cells.Add(cell7);

                cartTable.Rows.Add(row);

            }
        }
    }

    protected void remove_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        if (b != null)
        {
            cartItems.Remove(cartItems[Int32.Parse(b.ID)]);
            cartTable.Rows.Clear();
            Response.Redirect("ViewCart.aspx");
        }
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        List<int> removed = new List<int> { };

        int totalqty = 0;
        double totalprice = 0;
        double totalweight = 0;
        for (int i = 0; i < cartItems.Count; i++)
        {
            int quantity;

            TableCell curr = cartTable.Rows[i+1].Cells[3];
            string inputqty = ((TextBox)curr.Controls[0]).Text.ToString();

            bool valid = Int32.TryParse(inputqty, out quantity);

            if (valid)
            {
                if (quantity > 0)
                {
                    totalqty += quantity;
                    totalprice += cartItems[i].price * quantity;
                    totalweight += cartItems[i].weight * quantity;

                    cartItems[i].cartqty = quantity;
                    cartTable.Rows[i + 1].Cells[4].Text = "$" + (cartItems[i].price * cartItems[i].cartqty).ToString();
                    cartTable.Rows[i + 1].Cells[5].Text = (cartItems[i].weight * cartItems[i].cartqty).ToString() + " lbs";
                }
                else if (quantity == 0)
                {
                    removed.Add(i);
                }
                else
                {
                    lblError.Text = "Invalid Quantity set on item: " + cartItems[i].name;
                }
            }
            else
            {
                lblError.Text = "Invalid Quantity set on item: " + cartItems[i].name;
            }
        }


        cartTable.Rows[cartTable.Rows.Count - 3].Cells[3].Text = totalqty.ToString();
        cartTable.Rows[cartTable.Rows.Count - 3].Cells[4].Text = "$" + totalprice;
        cartTable.Rows[cartTable.Rows.Count - 3].Cells[5].Text = totalweight + " lbs";

        if (removed.Count > 0)
        {
            for (int i = removed.Count - 1; i >= 0; i--)
            {
                cartItems.Remove(cartItems[removed[i]]);
            }
            
            cartTable.Rows.Clear();
            updateCart();
        }

    }

    protected void checkoutButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Checkout.aspx");
    }



}