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
    public partial class ViewBooks : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    BindBooks(null);
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void RepeaterVwBks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VwDetails")
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("UserLogin.aspx");
                    return;
                }

                string bid = e.CommandArgument.ToString().Trim();

                string selectQue = "SELECT * FROM tbl_BookMaster WHERE BookID = '" + bid + "'";

                using (SqlCommand cmd = new SqlCommand(selectQue, conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            string img;
                            string bname;
                            string authname;
                            string pub;
                            string gen;
                            string pubyear;
                            string copy;
                            string bdesc;

                            while (dr.Read())
                            {
                                img = Convert.ToBase64String((byte[])dr["BookImage"]);
                                bname = dr["BookName"].ToString();
                                authname = dr["Author_Name"].ToString();
                                pub = dr["Publisher_Name"].ToString();
                                gen = dr["BookGenre"].ToString().TrimEnd(',');
                                pubyear = Convert.ToDateTime(dr["PublishYear"]).ToString("yyyy-MM-dd");
                                copy = dr["CopyAvailable"].ToString();
                                bdesc = dr["BookDescription"].ToString();

                                ShowBookDetails(img, bname, authname, pub, gen, pubyear, copy, bdesc, bid);
                            }
                        }
                        else
                        {
                            ShowMessageInfo("Error: No Book Found");
                        }
                    }
                }

            }
            else
            {
                ShowMessageInfo("Error: Invalid Command");
            }
        }

        private void ShowBookDetails(string img, string bname, string authname, string pub, string gen, string pubyear, string copy, string desc, string bid)
        {
            string script = "var myModal = new bootstrap.Modal(document.getElementById('bDetailsMod')); " +
                "myModal.show();";

            imgDetails.ImageUrl = "data:image/jpg;base64," + img;
            bID.Text = bid;
            bTitle.Text = bname;
            bAuthor.Text = authname;
            bPublisher.Text = pub;
            bGen.Text = gen;
            pubYear.Text = pubyear;
            bAvailability.Text = copy;
            descDetails.Text = desc;

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", script, true);
        }

        protected void BtnAddCart_ServerClick(object sender, EventArgs e)
        {
            try
            {
                GetUserID();

                string uid = Session["UserID"]?.ToString();

                if (string.IsNullOrEmpty(uid))
                {
                    ShowMessageInfo("User is not logged in.");
                    return;
                }

                // Check if user status is active
                string userStatus = GetUserStatus(uid);
                if (userStatus != "Active")
                {
                    ShowMessageInfo("User account is not active. Please contact the library for assistance.");
                    return;
                }

                string bid = bID.Text.Trim();
                string bname = bTitle.Text.Trim();
                string authname = bAuthor.Text.Trim();
                string pub = bPublisher.Text.Trim();
                string cpyNum = string.Empty;
                string borrowid = null;

                // (1) check existing borrow record
                string checkBorrowQue = "SELECT BorrowID FROM tbl_Borrow WHERE UserID = @uid AND IsCheckedOut = 0";
                using (SqlCommand cmd = new SqlCommand(checkBorrowQue, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                borrowid = dr["BorrowID"].ToString();
                            }
                        }
                    }
                }

                // (2) If no borrow record exists, create a new one and fetch the auto-incremented BorrowID
                if (string.IsNullOrEmpty(borrowid))
                {
                    string insertBorrowQue = "INSERT INTO tbl_Borrow (UserID, IssueDate, ReturnDate, IsCheckedOut) VALUES (@uid, @issdate, @retdate,@checkout); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(insertBorrowQue, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", uid);
                        cmd.Parameters.AddWithValue("@issdate", DateTime.Now); //takyah pun takpe
                        cmd.Parameters.AddWithValue("@retdate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@checkout", 0);

                        borrowid = cmd.ExecuteScalar()?.ToString();
                    }
                }

                // (3) Check if the book is already in the borrow list
                string checkCartQue = "SELECT COUNT(*) FROM tbl_Borrow_List WHERE BorrowID = @brwid AND BookID = @bid";
                using (SqlCommand cmd = new SqlCommand(checkCartQue, conn))
                {
                    cmd.Parameters.AddWithValue("@brwid", borrowid);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        ShowMessageInfo("This book is already in your cart.");
                        return;
                    }
                }

                // (4) Fetch the copy number of the selected book (ensure it’s available)
                string checkCopyQue = "SELECT TOP 1 CopyNumber FROM tbl_BookCopy WHERE BookID = @bid AND CopyStatus = 'Available' ORDER BY CopyNumber";
                using (SqlCommand cmd = new SqlCommand(checkCopyQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bid", bid);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                cpyNum = dr["CopyNumber"].ToString().Trim();
                            }
                        }
                        else
                        {
                            ShowMessageInfo("Book out of stock.");
                            return;
                        }
                    }
                }

                // (5) Insert the book into the borrow list
                string insertCartQue = "INSERT INTO tbl_Borrow_List (BorrowID, BookID, CopyNumber, Return_Status) VALUES (@brwid, @bid, @cpynum, 0)";
                using (SqlCommand cmd = new SqlCommand(insertCartQue, conn))
                {
                    cmd.Parameters.AddWithValue("@brwid", borrowid);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cpynum", cpyNum);
                    cmd.ExecuteNonQuery();
                }

                ShowMessageInfo("Book added to cart successfully.");
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        private string GetUserStatus(string userId)
        {
            string status = string.Empty;
            string selectQue = "SELECT Status FROM tbl_UserMaster WHERE UserID = @userId";
            using (SqlCommand cmd = new SqlCommand(selectQue, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            status = dr["Status"].ToString();
                        }
                    }
                }
            }
            return status;
        }

        void GetUserID()
        {
            string selectQue = "SELECT UserID FROM tbl_UserMaster WHERE Username = '" + Session["Username"].ToString() + "'";
            using (SqlCommand cmd = new SqlCommand(selectQue, conn))
            {
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            // Get the search terms from the TextBox
            string searchKeyword = txtSearch.Text.Trim();

            // Bind books with the search filters applied
            BindBooks(searchKeyword);
        }

        private void BindBooks(string searchKeyword)
        {
            // Build the query dynamically based on the search keywords
            string query = "SELECT * FROM [tbl_BookMaster] WHERE 1=1";
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query += " AND (BookName LIKE '%' + @SearchKeyword + '%' OR BookGenre LIKE '%' + @SearchKeyword + '%' OR Author_Name LIKE '%' + @SearchKeyword + '%')";
            }

            // Use SqlDataSource to fetch data
            SqlDataSourceVw.SelectCommand = query;

            // Add parameters only if they are not null
            SqlDataSourceVw.SelectParameters.Clear();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                SqlDataSourceVw.SelectParameters.Add("SearchKeyword", searchKeyword);
            }

            // Bind data to the Repeater
            RepeaterVwBks.DataSource = SqlDataSourceVw;
            RepeaterVwBks.DataBind();
        }
    }
}