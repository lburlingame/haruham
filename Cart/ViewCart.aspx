<%@ Page Title="" Language="C#" MasterPageFile="~/sc.master" AutoEventWireup="true" CodeFile="ViewCart.aspx.cs" Inherits="ViewCart" %>

<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
    Cart
</asp:Content>

<asp:Content ContentPlaceHolderID="styles" Runat="Server">
    <link href="assets/styles/cart.css" rel="stylesheet">    
</asp:Content>

<asp:Content ContentPlaceHolderID="content_holder" Runat="Server">
    <div id="content">
        <div class="container">
            <div id="cart_section">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>

                <div style="text-align:center;">
                  <div style="margin: 0 auto; text-align:center;">
                    <asp:Table ID="cartTable" runat="server"></asp:Table>
                  </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
    
</asp:Content>
