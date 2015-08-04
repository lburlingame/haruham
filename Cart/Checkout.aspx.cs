using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Net.Mail;


public partial class Checkout : System.Web.UI.Page
{
    private List<ShopItem> cartItems;

    private int totalquantity = 0;
    private double totalprice = 0;
    private double totalweight = 0;
    private double shippingcost = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                EmailLabel.Visible = false;
                Email.Visible = false;
                EmailValidator.Visible = false;
            }
            else
            {
                AddressName.Visible = false;
            }
            updateCheckout();
        }
        catch (Exception err)
        {
            errLbl.Text = err.ToString();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["Customer"] != null) 
        {
            Customer cust = (Customer)Session["Customer"];
            Email.Text = cust.Email;

            foreach (Address a in cust.Addresses)
            {
                Name.Text = a.Name;
                Street.Text = a.Street;
                City.Text = a.City;
                State.Text = a.State;
                Zip.Text = a.Zip;
            }
        }
    }


    public void updateCheckout()
    {
        if (Session["cartItems"] == null)
        {
            Response.Redirect("TeaShop.aspx");
        }
        else
        {
            cartItems = (List<ShopItem>)Session["cartItems"];

            if (cartItems.Count == 0)
            {
                Response.Redirect("TeaShop.aspx");
            }

            for (int i = 0; i < cartItems.Count; i++)
            {
                totalquantity += cartItems[i].cartqty;
                totalprice += cartItems[i].price * cartItems[i].cartqty;
                totalweight += cartItems[i].weight * cartItems[i].cartqty;
            }

            Items.Text = "Items: " + cartItems.Count;
            Quantity.Text = "Quantity: " + totalquantity;
            Weight.Text = "Weight: " + totalweight + " lbs";
            TotalPrice.Text = "Order Total: $" + totalprice;

            shippingcost = totalweight * 16 * .46;
            ItemTotal.Text = "Item Total Cost: $" + totalprice;
            Shipping.Text = "Shipping: $" + shippingcost;
            FullTotal.Text = "Total: $" + (totalprice + shippingcost);

        }
    }

    protected void ToShop_Click(object sender, EventArgs e)
    {
        SaveState();
        Response.Redirect("TeaShop.aspx");
    }



    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            SaveState();

            string connString = ConfigurationManager.ConnectionStrings["server"].ToString();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                string UID = GetUserID(conn);
                if (UID == null)
                {
                    UID = CreateUser(conn);
                    StoreUserInfo(conn, UID);
                }
                else
                {
                    UpdateUserInfo(conn, UID);
                }

                string OID = GetOrderNumber(conn, UID);
                ProcessOrder(conn, OID);
                string body = BuildOrderEmail(OID);
                SendConfirmationEmail(body);

                Session["OrderNum"] = OID;
                Session["cartItems"] = null;
                Response.Redirect("OrderConfirmation.aspx");
            }
        }
        catch (Exception err)
        {
            errLbl.Text = err.ToString();
        }
    }

    private void SaveState()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            //Address address = new Address(Name.Text, Street.Text, City.Text, State.Text, Zip.Text);
        }
        else
        {
            Session["Customer"] = null;
            Customer cust = new Customer(null, Email.Text, false);
            cust.Addresses.Add(new Address(null, Name.Text, Street.Text, City.Text, State.Text, Zip.Text));

            Session["Customer"] = cust;
        }
    }

    private string GetUserID(OleDbConnection conn)
    {
        string cmdStr = "SELECT UserID FROM Users WHERE Email=?";
        string UID = null;
        using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("Email", Email.Text);

            conn.Open();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    UID = reader["UserID"].ToString();
                }
            }
        }
        return UID;
    }

    private string CreateUser(OleDbConnection conn)
    {
        string cmdStr = @"INSERT INTO [Users] (Email) VALUES (?);";
        string UID = null;
        using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("Email", Email.Text);

            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT @@Identity";
            UID = cmd.ExecuteScalar().ToString();
        }
        return UID;
    }

    private void StoreUserInfo(OleDbConnection conn, String UID)
    {
        string cmdStr = @"INSERT INTO UserInfo (UserID, AddressName, FName, LName, Street, City, State, Zip) VALUES (?,?,?,?,?,?,?,?);";
        using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
        {
            cmd.CommandType = CommandType.Text;
            string[] names = Name.Text.Split(' ');
            string fname = "";
            if (names.Length == 2)
            {
                fname = names[1];
            }

            cmd.Parameters.AddWithValue("UserID", UID);
            cmd.Parameters.AddWithValue("AddressName", "Default");
            cmd.Parameters.AddWithValue("FName", fname);
            cmd.Parameters.AddWithValue("LName", names[0]);
            cmd.Parameters.AddWithValue("Street", Street.Text);
            cmd.Parameters.AddWithValue("City", City.Text);
            cmd.Parameters.AddWithValue("State", State.Text);
            cmd.Parameters.AddWithValue("Zip", Zip.Text);

            cmd.ExecuteNonQuery();
        }
    }

    private void UpdateUserInfo(OleDbConnection conn, string UID)
    {
        string cmdStr = @"INSERT INTO UserInfo (UserID, FName, LName, Street, City, State, Zip) VALUES (?,?,?,?,?,?,?);";
        using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
        {
            cmd.CommandType = CommandType.Text;
            string[] names = Name.Text.Split(' ');
            string lname = "";
            if (names.Length == 2)
            {
                lname = names[1];
            }

            cmd.Parameters.AddWithValue("UserID", UID);
            cmd.Parameters.AddWithValue("FName", names[0]);
            cmd.Parameters.AddWithValue("LName", lname);
            cmd.Parameters.AddWithValue("Street", Street.Text);
            cmd.Parameters.AddWithValue("City", City.Text);
            cmd.Parameters.AddWithValue("State", State.Text);
            cmd.Parameters.AddWithValue("Zip", Zip.Text);

            cmd.ExecuteNonQuery();
        }
    }

    private string GetOrderNumber(OleDbConnection conn, string UID)
    {
        string cmdStr = @"INSERT INTO [Orders] (UserID, ODate) VALUES (?,?);";
        string OID;
        using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
        {
            string format = "yyyy-MM-dd";  
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UserID", UID);
            cmd.Parameters.AddWithValue("ODate", DateTime.Now.ToString(format));

            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT @@Identity";
            OID = cmd.ExecuteScalar().ToString();
        }
        return OID;
    }

    private void ProcessOrder(OleDbConnection conn, string OID)
    {
        foreach (ShopItem item in cartItems)
        {
            string cmdStr = @"INSERT INTO [OrderInfo] (OrderID, ItemID, Quantity) VALUES (?,?,?);";
            using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("OrderID", OID);
                cmd.Parameters.AddWithValue("ItemID", item.id);
                cmd.Parameters.AddWithValue("Quantity", item.cartqty);

                cmd.ExecuteNonQuery();
            }
        }
    }

    private string BuildOrderEmail(string OID)
    {
        string body = "<h2>Haruham Tea Shop</h2><p>Your order has been successfully processed</p><br><br><p><strong>Order number: ham-" + OID;
        body += "</strong></p><table style='border: 1px solid lightgray; font-size: 14px;' width='650'><tr><td colspan='3'><strong>Item</strong></td><td><strong>Price</strong></td><td><strong>Quantity</strong></td><td><strong>Total</strong></td></tr>";

        foreach (ShopItem item in cartItems)
        {
            body += "<tr><td colspan='3'>" + item.name + "</td><td>$" + item.price + "</td><td>" + item.cartqty + "</td><td>$" + item.price * item.cartqty + "</td></tr>";
        }

        body += "</table><br><br><p style='height: 5px;'>Total quantity: " + totalquantity + "</p><p style='height: 5px;'>Shipping cost: $" + shippingcost + "</p><p style='height: 5px;'><strong>" + FullTotal.Text + "</strong></p><br><br><p style='height: 5px;'>";
        body += Name.Text + "</p><p style='height: 5px;'>" + Street.Text + "</p><p style='height: 5px;'>" + City.Text + ", " + State.Text + " " + Zip.Text + "</p><br><br><br>If you did not place this order with Haruham, please contact<p><a href='mailto:support@haruham.com' target='_top'>support@haruham.com</a>";

        return body;
    }

    private void SendConfirmationEmail(string body)
    {
        MailMessage msg = new MailMessage("auto-confirm@haruham.com", Email.Text.Trim());
        msg.IsBodyHtml = true;
        msg.Subject = "Your Haruham.com Order";
        msg.Body = body;

        SmtpClient client = new SmtpClient();

        client.Credentials = new System.Net.NetworkCredential("auto-confirm@haruham.com", "HamEmailConfirmation");
        client.Host = "www.haruham.com";

        try
        {
            client.Send(msg);
        }
        catch (Exception err)
        {
            errLbl.Text = err.ToString();
        }
    
    
    }

    /*
     *                                                              reader["FName"].ToString() + " " + reader["LName"].ToString(),
                                                             reader["Street"].ToString(),
                                                             reader["City"].ToString(),
                                                             reader["State"].ToString(),
                                                             reader["Zip"].ToString());*/

}




/*
 *  set Session["orderNum"] to null after email confirmation is sent
 * 
 * /










/*

<asp:Table ID="tableTable" runat="server" Width="900px" align = "center">
                   <asp:TableRow>
                   <asp:TableCell Width="450px">
                   <asp:Table ID="leftTable" runat="server" Width="450px" Border ="1px" Height="200px">
                   <asp:TableRow>
                    <asp:TableCell CssClass ="innerTableCell">

                    </asp:TableCell>
                    </asp:TableRow>
                    </asp:Table>
                    </asp:TableCell>
                    <asp:TableCell>
                   <asp:Table ID="rightTable" runat="server" Width="450px" Border ="1px" Height="200 px">
                    <asp:TableRow>
                        <asp:TableCell  CssClass ="innerTableCell">
                            Right
                        </asp:TableCell>
                    </asp:TableRow>
                   </asp:Table>
                    </asp:TableCell>
                   </asp:TableRow>
                   </asp:Table>

*/