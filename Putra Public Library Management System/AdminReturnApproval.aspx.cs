using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class AdminReturnApproval : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        private string uid;

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
                    if (!IsPostBack)
                    {
                        GetUserID();

                        uid = Session["UserID"].ToString().Trim();

                        //LoadNotification();

                        RepeaterApprove.DataBind();
                    }

                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error " + ex.Message);
            }
        }


        protected void RepeaterApprove_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ApproveReturn")
                {
                    if (Session["Username"] == null)
                    {
                        Response.Redirect("UserLogin.aspx");
                        return;
                    }

                    string nid = e.CommandArgument.ToString();
                    // (0) Approve the return notification
                    string approveReturn = "UPDATE tbl_UserNotification SET Is_Approved = 1 WHERE Not_ID = @NID";

                    using (SqlCommand cmd = new SqlCommand(approveReturn, conn))
                    {
                        cmd.Parameters.AddWithValue("@NID", nid);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            string brwid = GetBorrowID(nid);

                            // (1) Mark the book as returned in tbl_Borrow_List
                            string updateBLRetStat = "UPDATE tbl_Borrow_List SET Return_Status = 1 WHERE BorrowID = @brwid";
                            using (SqlCommand updateBL = new SqlCommand(updateBLRetStat, conn))
                            {
                                updateBL.Parameters.AddWithValue("@brwid", brwid);

                                //var bookDetails = GetBookDetails(brwid);
                                //string bID = bookDetails.bookID;
                                //string cpyNum = bookDetails.copyNumber;

                                //reader.Close();
                                if (updateBL.ExecuteNonQuery() > 0)
                                {
                                    GetBookDetails(brwid);

                                    // (2) Mark the book copy as available
                                    //string returnCopyStatus = "UPDATE tbl_BookCopy SET CopyStatus = 'Available' WHERE CopyNumber = @cpynum AND BookID = @bid";
                                    //using (SqlCommand updateBC = new SqlCommand(returnCopyStatus, conn))
                                    //{
                                    //    updateBC.Parameters.AddWithValue("@cpynum", cpyNum);
                                    //    updateBC.Parameters.AddWithValue("@bid", bID);

                                    //    if (updateBC.ExecuteNonQuery() > 0)
                                    //    {
                                    //        // (3) Increase the available copies in tbl_BookMaster
                                    //        string updateBMastCopy = "UPDATE tbl_BookMaster SET CopyAvailable = CopyAvailable + 1 WHERE BookID = @bid";
                                    //        using (SqlCommand updateBM = new SqlCommand(updateBMastCopy, conn))
                                    //        {
                                    //            updateBM.Parameters.AddWithValue("@bid", bID);

                                    //            if (updateBM.ExecuteNonQuery() > 0)
                                    //            {
                                    //                ShowAlert("Returns Approved");
                                    //                RepeaterApprove.DataBind();
                                    //            }
                                    //            else
                                    //            {
                                    //                ShowMessageInfo("Error (3): Book ID not found");
                                    //            }
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        ShowMessageInfo("Error (2): Copy number or Book ID not found ");
                                    //    }
                                    //}
                                }
                                else
                                {
                                    ShowMessageInfo("Error (1): Borrow ID or Book ID not found");
                                }
                            }

                        }
                        else
                        {
                            ShowMessageInfo("Error: Return not approved");
                        }
                    }
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

        private void ShowAlert(string msg)
        {
            string script = @"
        <script>
            // Create the alert wrapper
            const wrapper = document.createElement('div');
            wrapper.innerHTML = `
                <div id='temporaryAlert' class='alert alert-primary alert-dismissible fade show position-fixed top-0 start-50 translate-middle-x' style='z-index: 1050;' role='alert'>
                    <i class='fa-solid fa-check'></i> &nbsp; " + msg + @"
                </div>`;
            // Append the alert to the body
            document.body.appendChild(wrapper);

            // Automatically remove the alert after 1.5 seconds
            setTimeout(function () {
                const alertElement = document.getElementById('temporaryAlert');
                if (alertElement) {
                    alertElement.classList.remove('show'); // Trigger fade out
                    alertElement.addEventListener('transitionend', () => alertElement.remove());
                }
            }, 1500);
        </script>";

            ScriptManager.RegisterStartupScript(this, GetType(), "AlertScript", script, false);
        }

        private string GetBorrowID(string notID)
        {
            string borrowID = string.Empty;
            string query = "SELECT BorrowID FROM tbl_UserNotification WHERE Not_ID = @NotID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NotID", notID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            borrowID = dr["BorrowID"].ToString();
                        }
                        
                    }
                    dr.Close();
                }
            }
            return borrowID;
        }
        private void GetBookDetails(string borrowID)
        {
            try
            {
                string bookID = string.Empty;
                string copyNumber = string.Empty;
                string query = "SELECT BookID, CopyNumber FROM tbl_Borrow_List WHERE BorrowID = @BorrowID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BorrowID", borrowID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                bookID = dr["BookID"].ToString().Trim();
                                copyNumber = dr["CopyNumber"].ToString().Trim();

                                // (2) Mark the book copy as available
                                string returnCopyStatus = "UPDATE tbl_BookCopy SET CopyStatus = 'Available' WHERE CopyNumber = @cpynum AND BookID = @bid";
                                using (SqlCommand updateBC = new SqlCommand(returnCopyStatus, conn))
                                {
                                    updateBC.Parameters.AddWithValue("@cpynum", copyNumber);
                                    updateBC.Parameters.AddWithValue("@bid", bookID);

                                    if (updateBC.ExecuteNonQuery() > 0)
                                    {
                                        // (3) Increase the available copies in tbl_BookMaster
                                        string updateBMastCopy = "UPDATE tbl_BookMaster SET CopyAvailable = CopyAvailable + 1 WHERE BookID = @bid";
                                        using (SqlCommand updateBM = new SqlCommand(updateBMastCopy, conn))
                                        {
                                            updateBM.Parameters.AddWithValue("@bid", bookID);
                                            updateBM.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        ShowMessageInfo("Error (2): Copy number or Book ID not found ");
                                    }
                                }
                            }
                        }
                        else
                        {
                            ShowMessageInfo("Not found");
                        }
                    }
                }
                ShowAlert("Returns Approved");
                RepeaterApprove.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

    }
}