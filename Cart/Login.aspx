<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/sc.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
    Login
</asp:Content>


<asp:Content ContentPlaceHolderID="styles" Runat="Server">
    <link href="assets/styles/login.css" rel="stylesheet">
</asp:Content>


<asp:Content ContentPlaceHolderID="content_holder" Runat="Server">         

    <div id="content">
        <div class="container">
            <div class="centered" id="login_section">
                <img id="user_img" src="assets/images/default_user.svg" onerror="this.onerror=null; this.src='assets/images/default_user.png'">
                <asp:Label ID="errLbl" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                <asp:TextBox class="blk textbox" PlaceHolder="Email" ID="Email" spellcheck="false" onfocus="this.value = this.value" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:TextBox class="blk textbox" PlaceHolder="Password" ID="Password" TextMode="password" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:Button class="blk button" ID="Submit" ClientIDMode="Static" runat="server" Text="Sign In" OnClick="Submit_Click"/>
                <div id="login-footer" class="centered">
                    <span>Don't have an account?</span>
                    <asp:Button class="blk button" ID="Create" ClientIDMode="Static" runat="server" Text="Create One Here" OnClick="Create_Click" CausesValidation="False"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
    <script src="assets/scripts/login.js"></script>
</asp:Content>

