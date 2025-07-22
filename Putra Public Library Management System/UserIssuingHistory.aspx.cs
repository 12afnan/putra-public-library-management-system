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
    public partial class UserIssuingHistory : System.Web.UI.Page
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

                if (Session["Username"] == null)
                {
                    Response.Redirect("UserLogin.aspx");
                }
                else
                {
                    GetUserID();

                    string uID = Session["UserID"].ToString().Trim();

                    SqlDataSourceHistory.SelectParameters.Clear();
                    SqlDataSourceHistory.SelectParameters.Add("uid", uID);
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }
        private void GetUserID()
        {
            string selectQue = "SELECT UserID FROM tbl_UserMaster WHERE Username = @Username";
            using (SqlCommand cmd = new SqlCommand(selectQue, conn))
            {
                cmd.Parameters.AddWithValue("Username", Session["Username"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Session["UserID"] = dr["UserID"].ToString();
                        }
                    }
                    else
                    {
                        dr.Close();
                        GetAdminID();
                    }
                }
            }
        }

        private void GetAdminID()
        {
            string adminQue = "SELECT UserID FROM tbl_AdminMaster WHERE Admin_Username = @AdminUsername";
            using (SqlCommand cmd = new SqlCommand(adminQue, conn))
            {
                cmd.Parameters.AddWithValue("AdminUsername", Session["Username"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Session["UserID"] = dr["UserID"].ToString();
                        }
                    }
                    else
                    {
                        ShowMessageInfo("Error: No User Found");
                    }
                }
            }
        }

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
        }

        protected void GridViewHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the selected BorrowID

                string borrowID = GridViewHistory.SelectedDataKey.Value.ToString();

                string query = "SELECT m.BookID, m.BookName, m.BookImage, m.Author_Name, m.Publisher_Name, l.Return_Status FROM tbl_Borrow_List l JOIN tbl_BookMaster m ON l.BookID = m.BookID  WHERE l.BorrowID = @BorrowID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BorrowID", borrowID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable bookDetails = new DataTable();
                        da.Fill(bookDetails);

                        RepeaterBookDetails.DataSource = bookDetails;
                        if (bookDetails.Rows.Count == 0)
                        {
                            lblBInfo.Visible = true;
                        }
                        else
                        {
                            lblBInfo.Visible = false;
                            RepeaterBookDetails.DataBind();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error" + ex.Message);
            }
        }
    }
}