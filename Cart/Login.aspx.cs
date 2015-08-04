using System;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Web.Security;


public partial class Login : System.Web.UI.Page
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            Response.Redirect("TeaShop.aspx");
        }

        Email.Focus();
        if (!Email.Text.Equals(""))
        {
            Password.Focus();
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string connString = ConfigurationManager.ConnectionStrings["server"].ToString();
            
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                Customer cust = ValidateUser(conn);
                if (cust != null)
                {
                    FormsAuthentication.SetAuthCookie(Email.Text, false);
                    
                    InitializeUser(conn, cust);

                    Session["Customer"] = null;
                    Session["Customer"] = cust;
                    Response.Redirect("TeaShop.aspx");
                }
                else
                {
                    errLbl.Text = "Invalid Email/Password Combination";
                }
                
            }
        }
        catch (Exception err)
        {
            errLbl.Text = err.ToString();
        }
    }

    protected void Create_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

    private Customer ValidateUser(OleDbConnection conn)
    {
        string query = @"SELECT UserID, Email, Password, Verified FROM Users WHERE Email=?;";
        Customer cust = null;
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("Email", Email.Text);

            conn.Open();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string upw = reader["Password"].ToString();

                    string saltStore = upw.Substring(0, 44);
                    string hash = upw.Substring(44);

                    var salt = Convert.FromBase64String(saltStore);

                    byte[] hashValue;
                    using (var pbkdf2 = new Rfc2898DeriveBytes(Password.Text, salt, 24000))
                    {
                        hashValue = pbkdf2.GetBytes(64);
                    }

                    if (Convert.ToBase64String(hashValue).Equals(hash))
                    {
                        cust = new Customer(reader["UserID"].ToString(), Email.Text, Convert.ToBoolean(reader["UserID"]));
                    }
                }
            }
            conn.Close();
        }
        return cust;
    }

    private void InitializeUser(OleDbConnection conn, Customer cust)
    {
        // query = @"SELECT UserID, Email, Password FROM Users WHERE Email = ?";

        string query = @"SELECT AddressName, FName, LName, Street, City, State, Zip FROM UserInfo WHERE UserID=?";
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UserID", cust.ID);

            conn.Open();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cust.Addresses.Add(new Address(reader["AddressName"].ToString(),
                                                   reader["FName"].ToString() + " " + reader["LName"].ToString(),
                                                   reader["Street"].ToString(),
                                                   reader["City"].ToString(),
                                                   reader["State"].ToString(),
                                                   reader["Zip"].ToString()));
                }
            }
        }
    }
}