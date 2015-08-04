<%@ Page Title="" Language="C#" MasterPageFile="~/sc.master" AutoEventWireup="true" CodeFile="ViewShopItem.aspx.cs" Inherits="ViewShopItem" %>


<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
    View Item
</asp:Content>

<asp:Content ContentPlaceHolderID="styles" Runat="Server">
    <link href="assets/styles/item.css" rel="stylesheet">    
</asp:Content>


<asp:Content  ContentPlaceHolderID="content_holder" Runat="Server">

    <div id="content">
        <div class="container">
            <div style="text-align:center;">
              <div style="width:100%; margin: 0 auto; text-align:center;">
                <asp:Table ID="itemTable"  runat="server"></asp:Table>
              </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
    
</asp:Content>

