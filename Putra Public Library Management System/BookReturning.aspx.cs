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
    public partial class BookReturning : System.Web.UI.Page
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

                GridViewReturn.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void ButtonRet_Click(object sender, EventArgs e)
        {
            if (CheckIDExist())
            {
                ReturnBook();
            }
            else
            {
                ShowMessageInfo("Book already returned or invalid key");
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void ReturnBook()
        {
            try
            {
                string brwID = TextBoxBrwID.Text.Trim();

                string uID = TextBoxUID.Text.Trim();

                string bID = TextBoxBID.Text.Trim();

                string cpyNum = TextBoxCpyNum.Text.Trim();

                string checkBorrowID = "SELECT * FROM tbl_Borrow WHERE BorrowID = @brwid AND UserID = @uid AND IsCheckedOut = 1";
                using (SqlCommand cmd = new SqlCommand(checkBorrowID, conn))
                {
                    cmd.Parameters.AddWithValue("@brwid", brwID);
                    cmd.Parameters.AddWithValue("@uid", uID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            string borrowID = reader["BorrowID"].ToString().Trim();

                            // (1) Mark the book as returned in tbl_Borrow_List
                            string updateBLRetStat = "UPDATE tbl_Borrow_List SET Return_Status = 1 WHERE BorrowID = @brwid AND BookID = @bid";
                            using (SqlCommand updateBL = new SqlCommand(updateBLRetStat, conn))
                            {
                                updateBL.Parameters.AddWithValue("@brwid", borrowID);
                                updateBL.Parameters.AddWithValue("@bid", bID);

                                reader.Close();
                                if (updateBL.ExecuteNonQuery() > 0)
                                {
                                    // (2) Mark the book copy as available
                                    string returnCopyStatus = "UPDATE tbl_BookCopy SET CopyStatus = 'Available' WHERE CopyNumber = @cpynum AND BookID = @bid";
                                    using (SqlCommand updateBC = new SqlCommand(returnCopyStatus, conn))
                                    {
                                        updateBC.Parameters.AddWithValue("@cpynum", cpyNum);
                                        updateBC.Parameters.AddWithValue("@bid", bID);

                                        if (updateBC.ExecuteNonQuery() > 0)
                                        {
                                            // (3) Increase the available copies in tbl_BookMaster
                                            string updateBMastCopy = "UPDATE tbl_BookMaster SET CopyAvailable = CopyAvailable + 1 WHERE BookID = @bid";
                                            using (SqlCommand updateBM = new SqlCommand(updateBMastCopy, conn))
                                            {
                                                updateBM.Parameters.AddWithValue("@bid", bID);

                                                if (updateBM.ExecuteNonQuery() > 0)
                                                {
                                                    ShowMessageInfo("Book returned successfully.");
                                                }
                                                else
                                                {
                                                    ShowMessageInfo("Error (3): Book ID not found");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ShowMessageInfo("Error (2): Copy number or Book ID not found ");
                                        }
                                    }
                                }
                                else
                                {
                                    ShowMessageInfo("Error (1): Borrow ID or Book ID not found");
                                }
                            }
                        }
                        else
                        {
                            ShowMessageInfo("Error: Invalid Borrow ID");
                        }
                    }
                }
                GridViewReturn.DataBind();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        bool CheckIDExist()
        {
            try
            {
                string brwID = TextBoxBrwID.Text.Trim();

                string uID = TextBoxUID.Text.Trim();

                string bID = TextBoxBID.Text.Trim();

                string cpyNum = TextBoxCpyNum.Text.Trim();

                string checkKey = "SELECT r.BorrowID, r.UserID, u.FirstName, u.LastName, l.BookID, l.CopyNumber, r.IssueDate, r.ReturnDate, l.Return_Status " +
                    "FROM tbl_UserMaster AS u " +
                    "JOIN tbl_Borrow AS r ON u.UserID = r.UserID " +
                    "JOIN tbl_Borrow_List AS l ON r.BorrowID = l.BorrowID " +
                    "WHERE r.BorrowID = @brwid and r.UserID = @uid and l.BookID = @bid and l.CopyNumber = @cpynum AND (l.Return_Status IS NULL OR l.Return_Status = 0);";

                using (SqlCommand cmd = new SqlCommand(checkKey, conn))
                {
                    cmd.Parameters.AddWithValue("@brwid", brwID);
                    cmd.Parameters.AddWithValue("@uid", uID);
                    cmd.Parameters.AddWithValue("@bid", bID);
                    cmd.Parameters.AddWithValue("@cpynum", cpyNum);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        protected void GridViewReturn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string brwid = GridViewReturn.SelectedDataKey.Value.ToString().Trim();

                string selectQue = "SELECT r.BorrowID, r.UserID, l.BookID, l.CopyNumber " +
                                   "FROM tbl_Borrow AS r " +
                                   "JOIN tbl_Borrow_List AS l ON r.BorrowID = l.BorrowID " +
                                   "WHERE r.BorrowID = @brwID";

                using (SqlCommand cmd = new SqlCommand(selectQue, conn))
                {
                    cmd.Parameters.AddWithValue("@brwID", brwid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            TextBoxBrwID.Text = reader["BorrowID"].ToString().Trim();
                            TextBoxUID.Text = reader["UserID"].ToString().Trim();
                            TextBoxBID.Text = reader["BookID"].ToString().Trim();
                            TextBoxCpyNum.Text = reader["CopyNumber"].ToString().Trim();
                        }
                        else
                        {
                            ShowMessageInfo("Error: Borrow ID not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
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

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
        }

        //tak jadi tak tau sebab apa
        protected void GridViewReturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DateTime.TryParse(e.Row.Cells[7].Text, out DateTime returnDate))
                    {
                        if (returnDate > DateTime.Today)
                        {
                            e.Row.BackColor = System.Drawing.Color.Red;
                        }
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
        
    

