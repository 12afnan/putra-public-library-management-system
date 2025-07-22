using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

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

        //clear form
        private void ClearForm()
        {
            foreach (Control c in Page.Controls)
            {
                ClearControl(c);
            }
        }

        //clear control
        private void ClearControl(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox box)
                {
                    box.Text = string.Empty;
                }
                else if (c is DropDownList list)
                {
                    list.SelectedIndex = 0;
                }
                else if (c.Controls.Count > 0)
                {
                    ClearControl(c);
                }
            }
        }

        protected void ButtonLgIn_Click(object sender, EventArgs e)
        {
            try
            {
                string usrnameVal = TextBoxAdUsrnm.Text.Trim();
                string pwdVal = TextBoxAdPwd.Text.Trim();

                string selectQue = "SELECT * FROM tbl_AdminMaster WHERE Admin_Username = '" + usrnameVal + "' AND Admin_Password = '" + pwdVal + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ShowMessage("Welcome " + dr.GetValue(1).ToString());

                        Session["Username"] = dr["Admin_Username"].ToString();
                        Session["Role"] = "Admin";
                        //Session["Status"] = dr["Status"].ToString();

                        ClearForm();
                    }
                    Response.Redirect("home.aspx");
                }
                else
                {
                    ShowMessage("Invalid Username or Password");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }
    }
}