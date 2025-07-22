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
    public partial class BookCart : System.Web.UI.Page
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("UserLogin.aspx");
                    return;
                }

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                RepeaterCart.DataBind();

                GetUserID();

                string uid = Session["UserID"].ToString().Trim();

                SqlDataSourceCart.SelectCommand = @"
                        SELECT r.BorrowID, r.UserID, r.ReturnDate, r.IsCheckedOut, l.CopyNumber, b.BookID, b.BookName, b.BookImage, b.Publisher_Name, b.Author_Name, b.BookGenre 
                        FROM tbl_Borrow AS r
                        INNER JOIN tbl_Borrow_List AS l ON r.BorrowID = l.BorrowID
                        INNER JOIN tbl_BookMaster AS b ON l.BookID = b.BookID
                        WHERE r.UserID = @UserID AND r.IsCheckedOut = 0";
                SqlDataSourceCart.SelectParameters.Clear();
                SqlDataSourceCart.SelectParameters.Add("UserID", uid);

                RepeaterCart.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo(ex.Message);
            }

            UpdateCartVisibility();
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

        private void UpdateCartVisibility()
        {
            if (RepeaterCart.Items.Count == 0)
            {
                lblCart.Visible = true;
                lblTotalBook.Visible = false;
                lblBIC.Visible = false;
                BtnCheckout.Visible = false;
            }
            else
            {
                lblCart.Visible = false;
                lblTotalBook.Text = RepeaterCart.Items.Count.ToString();
                lblBIC.Text = RepeaterCart.Items.Count < 2 ? "BOOK IN CART" : "BOOKS IN CART";
                BtnCheckout.Visible = true;
            }
        }

        protected void RepeaterCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RmvBook")
            {
                try
                {
                    string bid = e.CommandArgument.ToString().Trim();
                    string uid = Session["UserID"].ToString().Trim();
                    string deleteQue = "DELETE FROM tbl_Borrow_List WHERE BookID = @BookID AND BorrowID = (SELECT TOP 1 BorrowID FROM tbl_Borrow WHERE UserID = @UserID AND IsCheckedOut = 0)";
                    using (SqlCommand cmd = new SqlCommand(deleteQue, conn))
                    {
                        cmd.Parameters.AddWithValue("BookID", bid);
                        cmd.Parameters.AddWithValue("UserID", uid);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            RepeaterCart.DataBind();

                            UpdateCartVisibility();

                            ShowMessageInfo("Book removed from cart successfully.");
                        }
                        else
                        {
                            RepeaterCart.DataBind();

                            UpdateCartVisibility();

                            ShowMessageInfo("Error: Book not removed from cart.");
                        }
                    }

                    

                }
                catch (Exception ex)
                {
                    ShowMessageInfo(ex.Message);
                }
            }
        }

        protected void BtnCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                string uid = Session["UserID"].ToString().Trim();

                // (1) Fetch cpyNum and bid for each book in the cart
                string fetchBooksQuery = "SELECT l.CopyNumber, l.BookID FROM tbl_Borrow_List AS l INNER JOIN tbl_Borrow AS r ON l.BorrowID = r.BorrowID WHERE r.UserID = @UserID AND r.IsCheckedOut = 0";
                using (SqlCommand cmd = new SqlCommand(fetchBooksQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", uid);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable booksTable = new DataTable();
                        adapter.Fill(booksTable);

                        foreach (DataRow row in booksTable.Rows)
                        {
                            string cpyNum = row["CopyNumber"].ToString().Trim();
                            string bid = row["BookID"].ToString().Trim();

                            // (2) Mark the book copy as issued
                            string borrowCopyStatus = "UPDATE tbl_BookCopy SET CopyStatus = 'Issued' WHERE CopyNumber = @cpynum AND BookID = @bid";
                            using (SqlCommand updateCmd = new SqlCommand(borrowCopyStatus, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@cpynum", cpyNum);
                                updateCmd.Parameters.AddWithValue("@bid", bid);
                                updateCmd.ExecuteNonQuery();
                            }

                            // (3) Decrease the available copies in tbl_BookMaster
                            string updateBMastCopy = "UPDATE tbl_BookMaster SET CopyAvailable = CopyAvailable - 1 WHERE BookID = @bid";
                            using (SqlCommand updateCmd = new SqlCommand(updateBMastCopy, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@bid", bid);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // (4) Update tbl_Borrow to mark the books as checked out
                string checkOutQue = "UPDATE tbl_Borrow SET IsCheckedOut = 1, IssueDate = @issDate, ReturnDate = @retDate WHERE UserID = @UserID AND IsCheckedOut = 0";
                using (SqlCommand cmd = new SqlCommand(checkOutQue, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", uid);
                    cmd.Parameters.AddWithValue("@issDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@retDate", DateTime.Now.AddDays(14));
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ShowMessageInfo("Checkout successful.");
                    }
                    else
                    {
                        ShowMessageInfo("Error: Checkout failed.");
                    }
                }

                RepeaterCart.DataBind();
                UpdateCartVisibility();
            }
            catch (Exception ex)
            {
                ShowMessageInfo(ex.Message);
            }
        }

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
        }

    }
}