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
    public partial class UserProfile : System.Web.UI.Page
    {
        readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        private string uID;
        private string newpass;

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

                    uID = Session["UserID"].ToString().Trim();

                    SqlDataSourceReturnBooks.SelectParameters.Clear();
                    SqlDataSourceReturnBooks.SelectParameters.Add("uid", uID);



                    GetUserData();

                    GridViewReturnBooks.DataBind();

                    RptBooks.DataBind();

                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxNwPwd.Text))
            {
                TextBoxNwPwd.Attributes["value"] = TextBoxNwPwd.Text; // Preserve the new password valu;
                newpass = TextBoxNwPwd.Attributes["value"];
                ShowConfirmPwd();
            }
            else
            {
                ShowMessageInfo("Enter new password");
            }
        }

        void GetUserData()
        {
            string selectQue = "SELECT * FROM tbl_UserMaster WHERE UserID = '" + uID + "'";
            using (SqlCommand cmd = new SqlCommand(selectQue, conn))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();

                        TextBoxFName.Text = dr["FirstName"].ToString().Trim();
                        TextBoxLName.Text = dr["LastName"].ToString().Trim();

                        TextBoxPNum.Text = dr["PhoneNum"].ToString().Trim();
                        TextBoxEml.Text = dr["Email"].ToString().Trim();

                        TextBoxState.Text = dr["State"].ToString().Trim();
                        TextBoxCity.Text = dr["City"].ToString().Trim();
                        TextBoxPtCode.Text = dr["Postcode"].ToString().Trim();

                        TextBoxUName.Text = dr["Username"].ToString().Trim();
                        TextBoxPwd.Attributes["value"] = dr["Password"].ToString().Trim();

                        lblAccStatus.Text = dr["Status"].ToString().Trim();

                        if (dr["Status"].ToString().Trim() == "Active")
                        {
                            lblAccStatus.Attributes.Add("class", "badge rounded-pill text-bg-success");
                        }
                        else if (dr["Status"].ToString().Trim() == "Pending")
                        {
                            lblAccStatus.Attributes.Add("class", "badge rounded-pill text-bg-warning");
                        }
                        else if (dr["Status"].ToString().Trim() == "Inactive")
                        {
                            lblAccStatus.Attributes.Add("class", "badge rounded-pill text-bg-danger");
                        }
                        else
                        {
                            lblAccStatus.Attributes.Add("class", "badge rounded-pill text-bg-info");
                        }

                    }
                    else
                    {
                        ShowMessageInfo("No data found");
                    }
                }
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

        protected void GridViewReturnBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string bid = GridViewReturnBooks.SelectedDataKey.Value.ToString();

                string query = "select l.BorrowID, m.BookImage, m.BookName, m.Author_Name, m.Publisher_Name from tbl_Borrow_List l join tbl_BookMaster m on l.BookID = m.BookID where l.Return_Status = 0 and l.BorrowID = @bID  ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@bID", bid);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            RptBooks.DataSource = dr;
                            RptBooks.DataBind();

                            if (RptBooks.Items.Count == 0)
                            {
                                BtnReturn.Visible = false;
                            }
                            else
                            {
                                BtnReturn.Visible = true;
                            }

                            ShowBooksModal();
                        }
                        else
                        {
                            ShowMessageInfo("Books already returned or no books found.");
                            //kalau tak jumpa buku
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                string borrowID = GridViewReturnBooks.SelectedDataKey.Value.ToString();
                string findReturnDateQue = "SELECT ReturnDate FROM tbl_Borrow WHERE IsCheckedOut = 1 AND UserID = @UID AND BorrowID = @BorrowID";
                DateTime expectedReturnDate;

                using (SqlCommand cmd = new SqlCommand(findReturnDateQue, conn))
                {
                    cmd.Parameters.AddWithValue("@BorrowID", borrowID);
                    cmd.Parameters.AddWithValue("@UID", uID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            expectedReturnDate = Convert.ToDateTime(dr["ReturnDate"]);
                            dr.Close();
                        }
                        else
                        {
                            ShowMessageInfo("Error: Borrow record not found.");
                            return;
                        }
                    }
                }

                // Check if the return date has been exceeded
                DateTime currentDate = DateTime.Now;
                double fineAmount = 0;

                if (currentDate > expectedReturnDate)
                {
                    // Calculate fine for late return
                    fineAmount = (currentDate - expectedReturnDate).TotalDays * 2.0; // Assume $2.0/day fine
                }

                string insertNotiQue = "INSERT INTO tbl_UserNotification (User_ID, BorrowID, Message, Fine_Amount, Is_Approved, Is_Read, Created_At) VALUES (@UID, @brwid, @msg, @famt, 0, 0, @create)";
                using (SqlCommand cmd = new SqlCommand(insertNotiQue, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", uID);
                    cmd.Parameters.AddWithValue("@brwid", borrowID);
                    cmd.Parameters.AddWithValue("@msg", fineAmount > 0
                        ? $"Late return! Fine: ${fineAmount:F2}. Return the books to the library within 24 hours."
                        : "Please return the books to the library within 24 hours.");
                    cmd.Parameters.AddWithValue("@famt", fineAmount);
                    cmd.Parameters.AddWithValue("@create", DateTime.Now);


                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ShowToast("Notification sent successfully.");

                    }
                }

                GridViewReturnBooks.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo(ex.Message);
            }
        }

        protected void RptBooks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int index = e.Item.ItemIndex;
                if (index % 3 == 0)
                {
                    // Open a new row
                    e.Item.Controls.AddAt(0, new LiteralControl("<div class='row'>"));
                }
                if ((index + 1) % 3 == 0 || (index + 1) == RptBooks.Items.Count)
                {
                    // Close the row after 3 items or at the end of the items
                    e.Item.Controls.Add(new LiteralControl("</div>"));
                }
            }

            // Hide BtnReturn if no rows are present
            if (RptBooks.Items.Count == 0)
            {
                BtnReturn.Visible = false;
            }
            else
            {
                BtnReturn.Visible = true;
            }
        }

        private void ShowBooksModal()
        {

            string script = "<script> var myModal = new bootstrap.Modal(document.getElementById('bReturnMod')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowBooksModal", script);
        }

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
        }

        private void ShowConfirmPwd()
        {
            string script = $"<script> var myModal = new bootstrap.Modal(document.getElementById('ModalPwd'));" +
                $"myModal.show(); " +
                $"</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, false);

        }

        private void ShowToast(string message)
        {
            string script = $"<script>document.querySelector('.toast-body').innerText = '{message}';" +
                            $" var toast = new bootstrap.Toast(document.getElementById('liveToast')); toast.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowToast", script);
        }

        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            TextBoxNwPwd.Attributes["value"] = TextBoxNwPwd.Text; // Preserve the new password valu;
            newpass = TextBoxNwPwd.Attributes["value"];

            // Retrieve the input from the TextBox
            string oldPassword = TextBoxPwd.Attributes["value"];

            // Perform necessary operations with the oldPassword
            if (ValidateOldPassword(oldPassword))
            {
                // Password is valid, update the password
                UpdatePassword(newpass);
            }
            else
            {
                // Password is invalid
                ShowMessageInfo("Old password is incorrect.");
            }
        }

        private bool ValidateOldPassword(string password)
        {
            string selectQue = "SELECT Password FROM tbl_UserMaster WHERE UserID = @UserID";
            using (SqlCommand cmd = new SqlCommand(selectQue, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", uID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        string currentPassword = dr["Password"].ToString().Trim();
                        return password == currentPassword;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private void UpdatePassword(string newPassword)
        {
            try
            {
                string updateQue = "UPDATE tbl_UserMaster SET Password = @NewPassword WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(updateQue, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@UserID", uID);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ShowMessageInfo("Password updated successfully.");
                        

                    }
                    else
                    {
                        ShowMessageInfo("Error updating password.");
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

    }
}