using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class UserLogin : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message);
            }
        }

        protected void ButtonLgIn_Click(object sender, EventArgs e)
        {
            try
            {
                string usrnameVal = TextBoxUserName.Text.Trim();
                string pwdVal = TextBoxPass.Text.Trim();

                string selectQue = "SELECT * FROM tbl_UserMaster WHERE Is_Deleted = 0 AND Username = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(selectQue, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", usrnameVal);
                    cmd.Parameters.AddWithValue("@Password", pwdVal);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                Session["Username"] = dr["Username"].ToString().Trim();

                                string usertype = dr["UserType"].ToString().Trim();

                                if (usertype == "Librarian")
                                {
                                    Session["Role"] = "Librarian";
                                }
                                else
                                {
                                    Session["Role"] = "User";
                                }

                                ShowMessage("Login Successful");
                            }

                            Response.Redirect("home.aspx");
                        }
                        else
                        {
                            ShowMessage("Invalid Username or Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        //show message in modal
        private void ShowMessage(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBody').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModal')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script);
        }

    }
}