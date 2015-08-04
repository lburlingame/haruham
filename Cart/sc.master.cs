using System;
using System.Collections.Generic;
using System.Web.Security;


public partial class scmaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cartItems"] != null)
        {
            List<ShopItem> cartItems = (List<ShopItem>)Session["cartItems"];
            cart_num.Text = cartItems.Count.ToString();
         
            if (cartItems.Count > 9 && cartItems.Count < 20)
            {
                cart_num.Style["left"] = "14px";
            }
            else if (cartItems.Count >= 20)
            {
                cart_num.Style["font-size"] = "12px";
            }

        }

        if (Page.User.Identity.IsAuthenticated)
        {
            account_button_img.ImageUrl = "../user/avatar.png";
            account_menu_img.ImageUrl = "../user/avatar.png";
            DisplayName.Text = Page.User.Identity.Name;
        }
        else
        {
            account_button_img.ImageUrl = "assets/images/default_user.svg";
            account_menu_img.ImageUrl = "assets/images/default_user.svg";
            account_button.HRef = "Login.aspx";
            account_button.ID = "login_button";
        }
    }

    //<asp:Image ID="account_button_img" src="assets/images/default_user.svg" onerror="this.onerror=null; this.src='assets/images/default_user.png'" runat="server" />
    protected void logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session["Customer"] = null;
        Session["cartItems"] = null;
        Response.Redirect("TeaShop.aspx");

    }
}
