<%@ Page Language="C#" MasterPageFile="sc.master" AutoEventWireup="true" CodeFile="TeaShop.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
    Shop Teas
</asp:Content>

<asp:Content ContentPlaceHolderID="styles" Runat="Server">
    <link href="assets/styles/shop.css" rel="stylesheet">    
</asp:Content>

<asp:Content ContentPlaceHolderID="content_holder" Runat="Server">
    <div id="content">
        <div class="container">
            <div class="left">
                <img src="assets/images/greentea_banner.jpg">
            </div>
            <div class="right">
                <h2>Haruham's <span class="green">Greens</span></h2>
                <p><span class="green">Green teas</span> from China and Japan are the pride of these traditional tea producers. These are the teas that they drink the most. Many people are looking to reap the benefits of <span class="green">green tea</span>, while enjoying its unique flavor. Haruham respects the traditions surrounding <span class="green">green tea</span> by choosing the best available. Count on us to give you the best in <span class="green">green tea</span>.</p>
            </div>
        </div>
        <div class="container">
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            <asp:Panel ID="items" runat="server"></asp:Panel>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
    
</asp:Content>
