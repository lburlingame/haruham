<%@ Page Title="" Language="C#" MasterPageFile="~/sc.master" AutoEventWireup="true" CodeFile="OrderConfirmation.aspx.cs" Inherits="OrderConfirmation" %>

<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
</asp:Content>


<asp:Content ContentPlaceHolderID="styles" Runat="Server">
</asp:Content>


<asp:Content ContentPlaceHolderID="content_holder" Runat="Server">
    <div id="content">
        <div class="container">
            <asp:Label ID="errLbl" runat="server" Text=""></asp:Label>        
            <asp:Label ID="Order" runat="server" Text=""></asp:Label>    
            <asp:Label ID="Label" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>


<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>

