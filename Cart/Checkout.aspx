<%@ Page Title="" Language="C#" MasterPageFile="~/sc.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
    Checkout</asp:Content>

<asp:Content ContentPlaceHolderID="styles" Runat="Server">
    <link href="assets/styles/checkout.css" rel="stylesheet">
</asp:Content>


<asp:Content ContentPlaceHolderID="content_holder" Runat="Server">
    <div id="content">
        <div class="container">
            <h2>Order Checkout</h2>
            <asp:Label ID="errLbl" runat="server" Text=""></asp:Label>
            <div class="checkout-container border">
                <asp:Label CssClass="Title" runat="server" Text="Order Summary"></asp:Label>
                <asp:Label ID="Items" runat="server" Text=""></asp:Label>
                <asp:Label ID="Quantity" runat="server" Text=""></asp:Label>
                <asp:Label ID="Weight" runat="server" Text=""></asp:Label>
                <asp:Label ID="TotalPrice" runat="server" Text=""></asp:Label>
            </div>
            <div class="checkout-container">
                <asp:Label ID="ItemTotal" runat="server" Text=""></asp:Label>
                <asp:Label ID="Shipping" runat="server" Text=""></asp:Label>
                <asp:Label ID="FullTotal" runat="server" Text=""></asp:Label>
            </div>
            <div class="checkout-container border">
                <asp:Label CssClass="Title" runat="server" Text="Shipping Information"></asp:Label>

                <div ID="EmailLabel" class="label" runat="server">Email</div>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ErrorMessage="*" ControlToValidate="Email"></asp:RequiredFieldValidator>
                <asp:TextBox ID="Email" CssClass="textbox" runat="server"></asp:TextBox>

                <div class="label">Name</div>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ErrorMessage="*" ControlToValidate="Name"></asp:RequiredFieldValidator>
                <asp:TextBox ID="Name" CssClass="textbox" runat="server"></asp:TextBox>
                
                <div class="label">Street</div>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ErrorMessage="*" ControlToValidate="Street"></asp:RequiredFieldValidator>
                <asp:TextBox ID="Street" CssClass="textbox" runat="server"></asp:TextBox>

                <div class="label">City</div>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ErrorMessage="*" ControlToValidate="City"></asp:RequiredFieldValidator>
                <asp:TextBox ID="City" CssClass="textbox" runat="server"></asp:TextBox>

                <div class="label">State</div>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ErrorMessage="*" ControlToValidate="State"></asp:RequiredFieldValidator>
                <asp:TextBox ID="State" CssClass="textbox" runat="server"></asp:TextBox>

                <div class="label">Zip</div>
                <asp:RequiredFieldValidator CssClass="validator" runat="server" ErrorMessage="*" ControlToValidate="Zip"></asp:RequiredFieldValidator>
                <asp:TextBox ID="Zip" CssClass="textbox" runat="server"></asp:TextBox>

            </div>
            <div class="button-block">
                <asp:Button ID="ToShop" CssClass="button cancel" runat="server" Text="Continue Shopping" OnClick="ToShop_Click" CausesValidation="False" />
                <asp:Button ID="Submit" CssClass="button submit" runat="server" Text="Submit Order" OnClick="Submit_Click" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
    
</asp:Content>

