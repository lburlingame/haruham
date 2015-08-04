<%@ Page Title="" Language="C#" MasterPageFile="~/sc.master" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<asp:Content ContentPlaceHolderID="title_holder" Runat="Server">
    Create Account
</asp:Content>

<asp:Content ContentPlaceHolderID="styles" Runat="Server">
    <link href="assets/styles/create.css" rel="stylesheet">
</asp:Content>

<asp:Content ContentPlaceHolderID="content_holder" Runat="Server">

     <div id="content">
        <div class="container">
            <div class="centered" id="login_section">
                <asp:Label ID="errLbl" runat="server" Text=""></asp:Label>
                <h2>Create an Account</h2>
                
                <asp:TextBox class="blk textbox" PlaceHolder="First" ID="FName" spellcheck="false" onfocus="this.value = this.value" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="blk validator" ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this empty" ControlToValidate="FName"></asp:RequiredFieldValidator>

                <asp:TextBox class="blk textbox" PlaceHolder="Last" ID="Lname" spellcheck="false" onfocus="this.value = this.value" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="blk validator" ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this empty" ControlToValidate="LName"></asp:RequiredFieldValidator>

                <asp:TextBox class="blk textbox" PlaceHolder="Email" ID="Email" spellcheck="false" onfocus="this.value = this.value" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="blk validator" ID="RequiredFieldValidator3" runat="server" ErrorMessage="You can't leave this empty" ControlToValidate="Email"></asp:RequiredFieldValidator>
                
                <asp:TextBox class="blk textbox" PlaceHolder="Password" ID="Password" TextMode="password" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="blk validator" ID="RequiredFieldValidator4" runat="server" ErrorMessage="You can't leave this empty" ControlToValidate="Password"></asp:RequiredFieldValidator>
                
                <asp:TextBox class="blk textbox" PlaceHolder="Confirm Password" ID="Password2" TextMode="password" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="blk validator" ID="RequiredFieldValidator5" runat="server" ErrorMessage="You can't leave this empty" ControlToValidate="Password2"></asp:RequiredFieldValidator>
                
                <asp:CustomValidator CssClass="validator" runat="server" 
                    ErrorMessage="Passwords must match"
                    ControlToValidate="Password2" 
                    OnServerValidate="matchPasswords_ServerValidate"
                    ValidateEmptyText="False">
                </asp:CustomValidator>
                <asp:Button class="blk button" ID="Submit" ClientIDMode="Static" runat="server" Text="Create Account" OnClick="Submit_Click" />
                
                <div id="login-footer" class="centered">
                    <span>Already have an account?</span>
                    <asp:Button class="blk button" ID="Login" ClientIDMode="Static" runat="server" Text="Sign In Here" CausesValidation="False" OnClick="Login_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" Runat="Server">
    <script src="assets/scripts/create.js"></script>
</asp:Content>

