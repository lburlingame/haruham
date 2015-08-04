using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    //split into smaller chunks
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ShopItem> shopItems = new List<ShopItem>{ };
        List<ShopItem> cartItems = new List<ShopItem>{ };

        if (Session["cartItems"] != null)
        {
            cartItems = (List<ShopItem>)Session["cartItems"];
        }
        else
        {
            Session["cartItems"] = cartItems;
        }

        if (IsPostBack == false || shopItems == null || shopItems.Count == 0)
        {
            try
            {
                OleDbConnection cn;
                OleDbCommand cmd;
                OleDbDataReader dr;

                cn = new OleDbConnection();


              //  if (Request.UserHostAddress.ToString().Equals("::1"))
             //   {
                    // Local server...                 
                    cn.ConnectionString = ConfigurationManager.ConnectionStrings["server"].ToString();
             /*   }
                else
                {
                    // Service provider server..
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|scdatabase.accdb; Persist Security Info=False;";
                }*/
                cmd = new OleDbCommand("SELECT ItemID, Name, Description, Price, ThumbURL, FullURL, Rating, Weight FROM Items ORDER BY ItemID", cn);

                cn.Open();

                dr = cmd.ExecuteReader();
                lblStatus.Text = "";

                while (dr.Read())
                {
                    shopItems.Add(new ShopItem(Int32.Parse(dr["ItemID"].ToString()), dr["Name"].ToString(), dr["Description"].ToString(), Double.Parse(dr["Price"].ToString()), dr["ThumbURL"].ToString(), dr["FullURL"].ToString(), Double.Parse(dr["Rating"].ToString()), Double.Parse(dr["Weight"].ToString())));
                }

                dr.Close();
                cn.Close();

                Session["shopItems"] = shopItems;

            }
            catch (Exception err)
            {
                lblStatus.Text = err.ToString();
            }
        }
        else
        {
            shopItems = (List<ShopItem>)Session["shopItems"];
        }
        
        for (int i = 0; i < shopItems.Count; i++)
        {
            Panel itemPanel = new Panel();
            itemPanel.CssClass = "item_container";

            ImageButton itemButton = new ImageButton();
            itemButton.ID = i.ToString();
            itemButton.Click += new ImageClickEventHandler(itemButton_Click);
            itemButton.ImageUrl = shopItems[i].thumbURL;
            itemPanel.Controls.Add(itemButton);

            Label name = new Label();
            name.CssClass = "item_name";
            name.Text = shopItems[i].name;
            itemPanel.Controls.Add(name);

            items.Controls.Add(itemPanel);
        }
        
    }

    public void itemButton_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        if (button != null)
        {
            int current = Int32.Parse(button.ID);
            Session["current"] = current;
            Response.Redirect("ViewShopItem.aspx");
        } 
    }

}















/*
    <div class="items">

        <asp:Panel ID="Panel1" VerticalAlign="Center" runat="server">

                <div style="height: 40px; vertical-align: top">
                <div style="padding-top: 10px; float:left;">
                <asp:ImageButton ID="genbutton" runat="server" OnClick="genbutton_Click" />  
                </div>
                <div style="padding-top: 5px; float:left;">
                <asp:Label runat="server" ID="genlabel" Text="text1"
                Style="margin-left: 18px; margin-top: 0px"></asp:Label>

                </div>
        </asp:Panel>

        <asp:Panel ID="Panel2" VerticalAlign="Center" runat="server">
                <asp:ImageButton ID="gyobutton" runat="server" OnClick="gyobutton_Click" />
                <asp:Label runat="server" ID="gyolabel" Text="text2"></asp:Label>
        </asp:Panel>

        <div style="">
            <div>
            </div>
            <div>
            </div>
        </div>

        <div style="">
            <div>
                <asp:ImageButton ID="matchabutton" runat="server" OnClick="matchabutton_Click" />
            </div>
            <div>
                <asp:Label runat="server" ID="matchalabel" Text=""></asp:Label>
            </div>
        </div>

        <div style="">
            <div>
                <asp:ImageButton ID="senchabutton" runat="server" OnClick="senchabutton_Click" />
            </div>
            <div>
                <asp:Label runat="server" ID="senchalabel" Text=""></asp:Label>
            </div>
        </div>

        <div style="">
            <div>
                <asp:ImageButton ID="huangbutton" runat="server" OnClick="huangbutton_Click" />
            </div>
            <div>
                <asp:Label runat="server" ID="huanglabel" Text=""></asp:Label>
            </div>
        </div>

    </div>

*/