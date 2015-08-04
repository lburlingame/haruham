using System;

public partial class Verification : System.Web.UI.Page
{
    // password reset will also have you enter in your email address to reset the password
    // encrypt user name and street, phone number, and email
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request.QueryString["Key"];

        if (true)
        {
            Label.Text = "Your account has been successfully verified.";
        }
    }
}