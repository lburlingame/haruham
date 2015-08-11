using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Security.Cryptography;

using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Email.Focus();
        if (!Email.Text.Equals(""))
        {
            Password.Focus();
        }  
    }

    // split into smaller chunks

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var salt = Crypt.getNewSalt(16);

            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(Password.Text, salt, 24000))
            {
                hash = pbkdf2.GetBytes(20);
            }


            string hashStr = Convert.ToBase64String(hash);

            string saltStr = Convert.ToBase64String(salt);


            try
            {

                string connString = ConfigurationManager.ConnectionStrings["server"].ToString();
                string cmdStr = @"UPDATE [Users] SET [Password]=? WHERE Email=? AND [Password] IS NULL;";

                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    int success;
                    using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("[Password]", saltStr + hashStr);
                        cmd.Parameters.AddWithValue("Email", Email.Text);

                        conn.Open();
                        success = cmd.ExecuteNonQuery();
                        conn.Close();
                    }      


                    if (success == 0)
                    {
                        cmdStr = @"INSERT INTO [Users] (Email, [Password]) VALUES (?,?);";
                        using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("Email", Email.Text);
                            cmd.Parameters.AddWithValue("[Password]", saltStr + hashStr);
                            //  cmd.Parameters.AddWithValue("Email", Email.Text);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                errLbl.Text = err.ToString();
            }
        }
        else
        {
            errLbl.Text = "One or more inputs are not correct";
        }
        
        
  
    }
    
    protected void Login_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void matchPasswords_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (Password.Text.Equals(Password2.Text))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        catch (Exception e)
        {
            args.IsValid = false;
        }
    }

    private void StoreKey(OleDbConnection conn, string key)
    {

    }

    private void SendValidationEmail(string email, string key)
    {
        string url = "Validation.aspx?";
        url += "Key=" + key;
    }


}