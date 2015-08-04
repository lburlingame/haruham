using System;


public partial class OrderConfirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["OrderNum"] != null)
        {
            Order.Text = "Order ham-" + Session["OrderNum"] + " has been processed.";
            Label.Text = "An email has been sent to confirm your order.";
        }

    }
}