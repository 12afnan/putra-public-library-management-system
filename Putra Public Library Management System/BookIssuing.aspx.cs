using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class BookIssuing : System.Web.UI.Page
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

                if (Session["Username"] == null)
                {
                    Response.Redirect("AdminLogin.aspx");
                }

                GridViewIssue.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void ButtonGo_Click(object sender, EventArgs e)
         {
            if (CheckBookExist())
            {
                GetName();
            }
            else
            {
                ShowMessageInfo("Book or User not found!");
            }
        }

        protected void ButtonIss_Click(object sender, EventArgs e)
        {
            if (CheckBookExist() && CheckUserExist())
            {
                IssueBook();
            }
            else
            {
                ShowMessageInfo("Book or User not found!");
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void IssueBook()
        {
            try
            {
                string uID = TextBoxBrwrID.Text.Trim();
                string bID = TextBoxBID.Text.Trim();
                string copyNum = GetAvailableCopyNumber(bID);

                if (string.IsNullOrEmpty(copyNum))
                {
                    ShowMessageInfo("Book out of stock.");
                    return;
                }

                string borrowid = CreateBorrowRecord(uID);
                InsertBorrowList(borrowid, bID, copyNum);
                UpdateBookCopyStatus(bID, copyNum, "Issued");
                UpdateBookMaster(bID, -1);

                ShowMessageInfo("Book Issued Successfully!");
                GridViewIssue.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        string CreateBorrowRecord(string uID)
        {
            string borrowid;
            string insertQue = "INSERT INTO tbl_Borrow (UserID, IssueDate, ReturnDate, IsCheckedOut) VALUES (@uid, @issdate, @retdate, 1); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand(insertQue, conn))
            {
                cmd.Parameters.AddWithValue("uid", uID);
                cmd.Parameters.AddWithValue("issdate", DateTime.Now);
                cmd.Parameters.AddWithValue("retdate", DateTime.Now.AddDays(14));

                borrowid = cmd.ExecuteScalar().ToString();
            }
            return borrowid;
        }

        string GetAvailableCopyNumber(string bID)
        {
            string copyNum = null;
            string checkCopyQue = "SELECT TOP 1 CopyNumber FROM tbl_BookCopy WHERE BookID = @bid AND CopyStatus = 'Available' ORDER BY CopyNumber";
            using (SqlCommand cmd = new SqlCommand(checkCopyQue, conn))
            {
                cmd.Parameters.AddWithValue("@bid", bID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        copyNum = dr["CopyNumber"].ToString().Trim();
                    }
                }
            }
            return copyNum;
        }

        void InsertBorrowList(string borrowid, string bID, string copyNum)
        {
            string insertCartQue = "INSERT INTO tbl_Borrow_List (BorrowID, BookID, CopyNumber, Return_Status) VALUES (@brwid, @bid, @cpynum, 0)";
            using (SqlCommand cmd = new SqlCommand(insertCartQue, conn))
            {
                cmd.Parameters.AddWithValue("@brwid", borrowid);
                cmd.Parameters.AddWithValue("@bid", bID);
                cmd.Parameters.AddWithValue("@cpynum", copyNum);
                cmd.ExecuteNonQuery();
            }
        }

        void UpdateBookCopyStatus(string bID, string copyNum, string status)
        {
            string updateCopyStatus = "UPDATE tbl_BookCopy SET CopyStatus = @status WHERE CopyNumber = @cpynum AND BookID = @bid";
            using (SqlCommand cmd = new SqlCommand(updateCopyStatus, conn))
            {
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@cpynum", copyNum);
                cmd.Parameters.AddWithValue("@bid", bID);
                cmd.ExecuteNonQuery();
            }
        }

        void UpdateBookMaster(string bID, int change)
        {
            string updateBMastCopy = "UPDATE tbl_BookMaster SET CopyAvailable = CopyAvailable + @change WHERE BookID = @bid";
            using (SqlCommand cmd = new SqlCommand(updateBMastCopy, conn))
            {
                cmd.Parameters.AddWithValue("@change", change);
                cmd.Parameters.AddWithValue("@bid", bID);
                cmd.ExecuteNonQuery();
            }
        }

        void GetName()
        {
            try
            {
                string bID = TextBoxBID.Text.Trim();
                string uID = TextBoxBrwrID.Text.Trim();

                string bNameQuery = "SELECT BookName FROM tbl_BookMaster WHERE BookID = @bid";
                using (SqlCommand cmd = new SqlCommand(bNameQuery, conn))
                {
                    cmd.Parameters.AddWithValue("bid", bID);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        TextBoxBName.Text = result.ToString().Trim();
                    }
                    else
                    {
                        ShowMessageInfo("Book not found!");
                    }
                }

                string uNameQue = "SELECT FirstName, LastName FROM tbl_UserMaster WHERE UserID = @uid";
                using (SqlCommand cmd = new SqlCommand(uNameQue, conn))
                {
                    cmd.Parameters.AddWithValue("uid", uID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            TextBoxBrwrName.Text = dr["FirstName"].ToString().Trim() + " " + dr["LastName"].ToString().Trim();
                        }
                        else
                        {
                            ShowMessageInfo("User not found!");
                        }
                    }
                }

                string checkCopyQue = "SELECT TOP 1 CopyNumber FROM tbl_BookCopy WHERE BookID = @bid AND CopyStatus = 'Available' ORDER BY CopyNumber DESC";
                using (SqlCommand cmd = new SqlCommand(checkCopyQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bid", bID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            TextBoxAvaCpy.Text = dr["CopyNumber"].ToString().Trim();
                        }
                        else
                        {
                            TextBoxAvaCpy.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        bool CheckUserExist()
        {
            try
            {
                string userCheckQue = "SELECT * FROM tbl_UserMaster WHERE UserID = @uid";
                using (SqlCommand cmd = new SqlCommand(userCheckQue, conn))
                {
                    cmd.Parameters.AddWithValue("uid", TextBoxBrwrID.Text.Trim());
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        bool CheckBookExist()
        {
            try
            {
                string bookCheckQue = "SELECT * FROM tbl_BookMaster WHERE BookID = @bid";
                using (SqlCommand cmd = new SqlCommand(bookCheckQue, conn))
                {
                    cmd.Parameters.AddWithValue("bid", TextBoxBID.Text.Trim());
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                           $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
        }

        private void ClearForm()
        {
            foreach (Control c in Page.Controls)
            {
                ClearControl(c);
            }
        }

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