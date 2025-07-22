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
    public partial class LibrarianMngmnt : System.Web.UI.Page
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
                    Response.Redirect("AdminLogin.aspx");
                }


                GridViewLibrarian.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            GetUserByUserID();
        }

        protected void ButtonReg_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = TextBoxUID.Text.Trim();

                if (CheckLibrarianExist())
                {
                    ShowMessageInfo("User is already a registered librarian.");
                    return;
                }

                if (!IsActiveUser(userId))
                {
                    ShowMessageInfo("Only active users can be registered as librarians.");
                    return;
                }

                string libquery = "INSERT INTO tbl_LibrarianMaster (UserID,Date_Added) VALUES (@UserID,@date)";

                using (SqlCommand cmd = new SqlCommand(libquery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        string updatetype = "UPDATE tbl_UserMaster SET UserType = 'Librarian' WHERE UserID = @UserID ";

                        using (SqlCommand cmdUpd = new SqlCommand(updatetype, conn))
                        {
                            cmdUpd.Parameters.AddWithValue("@UserID", userId);

                            if (cmdUpd.ExecuteNonQuery() > 0)
                            {
                                ShowMessageInfo("Librarian registered successfully.");
                            }
                            else
                            {
                                ShowMessageInfo("Failed to register librarian.");
                            }
                        }
                    }
                    else
                    {
                        ShowMessageInfo("Failed to register librarian.");
                    }
                }
                GridViewLibrarian.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        private bool IsActiveUser(string userId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM tbl_UserMaster WHERE UserID = @UserID AND Status = 'Active'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            if (CheckLibrarianExist())
            {
                DeleteLibrarian();
            }
            else
            {
                ShowMessageInfo("Librarian not found");
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            try { ClearForm(); }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }

        }

        bool CheckLibrarianExist()
        {
            try
            {
                string userId = TextBoxUID.Text.Trim();
                string query = "SELECT COUNT(*) FROM tbl_LibrarianMaster WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        void DeleteLibrarian()
        {
            try
            {
                string userId = TextBoxUID.Text.Trim();

                string deleteQue = "DELETE FROM tbl_LibrarianMaster WHERE UserID = '" + userId + "'";

                using (SqlCommand cmd = new SqlCommand(deleteQue, conn))
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        string updatetype = "UPDATE tbl_UserMaster SET UserType = 'Member' WHERE UserID = @UserID ";

                        using (SqlCommand cmdUpd = new SqlCommand(updatetype, conn))
                        {
                            cmdUpd.Parameters.AddWithValue("@UserID", userId);

                            if (cmdUpd.ExecuteNonQuery() > 0)
                            {

                                ShowMessageInfo("Librarian deleted successfully !");
                                ClearForm();
                            }
                            else
                            {
                                ShowMessageInfo("Librarian deletion failed");
                                ClearForm();
                            }
                        }
                    }
                    else
                    {
                        ShowMessageInfo("Librarian deletion failed");
                    }

                }
                GridViewLibrarian.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void GetUserByUserID()
        {
            try
            {
                string userId = TextBoxUID.Text.Trim();
                string query = "SELECT * FROM tbl_UserMaster WHERE Status = 'Active' AND UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TextBoxUName.Text = reader["Username"].ToString().Trim();
                                TextBoxStat.Text = reader["Status"].ToString().Trim();
                                TextBoxFullName.Text = $"{reader["FirstName"]} {reader["LastName"]}";
                            }
                        }
                        else
                        {
                            ShowMessageInfo("Librarian/user does not exist or user status not active");
                            ClearForm();
                        }
                    }
                }
                GridViewLibrarian.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void GridViewLibrarian_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string uid = GridViewLibrarian.SelectedDataKey.Value.ToString().Trim();

                string query = "SELECT * FROM tbl_UserMaster WHERE Status = 'Active' AND UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", uid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TextBoxUID.Text = reader["UserID"].ToString().Trim();

                                TextBoxUName.Text = reader["Username"].ToString().Trim();
                                TextBoxStat.Text = reader["Status"].ToString().Trim();
                                TextBoxFullName.Text = $"{reader["FirstName"]} {reader["LastName"]}";
                            }
                        }
                        else
                        {
                            ShowMessageInfo("Librarian/user does not exist or user status not active");
                            ClearForm();
                        }
                    }
                }
                GridViewLibrarian.DataBind();

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }

        }


        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
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

    }
}