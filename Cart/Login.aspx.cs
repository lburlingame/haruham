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
        Email.Focus();
        if (!Email.Text.Equals(""))
        {
            Password.Focus();
        }
    }

    //split into smaller chunks

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string connString = ConfigurationManager.ConnectionStrings["server"].ToString();
            //string query = @"SELECT Email, Password FROM Users WHERE Email = ?";
            string query = @"SELECT Email, Password, FName, LName, Street, City, State, Zip FROM Users INNER JOIN UserInfo ON Users.UserID = UserInfo.UserID WHERE Email = ?";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("Email", Email.Text);

                  //  string upw = "";

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
                                FormsAuthentication.SetAuthCookie(Email.Text, false);
                                Customer cust = new Customer(Email.Text, 
                                                             reader["FName"].ToString() + " " + reader["LName"].ToString(),
                                                             reader["Street"].ToString(),
                                                             reader["City"].ToString(),
                                                             reader["State"].ToString(),
                                                             reader["Zip"].ToString());
                                Session["Customer"] = cust;
                                Response.Redirect("TeaShop.aspx");
                            }
                            else
                            {
                                errLbl.Text = "Incorrect Email/Password Combination";
                            }
                        }
                        else
                        {
                            errLbl.Text = "Incorrect Email/Password Combination";
                        }
                    }
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
}