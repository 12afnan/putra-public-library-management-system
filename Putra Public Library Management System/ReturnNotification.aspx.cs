using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class ReturnNotification : System.Web.UI.Page
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

                        LoadNotification();

                        RepeaterReturn.DataBind();
                    }

                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error " + ex.Message);
            }
        }

        void LoadNotification()
        {
            try
            {
                if (string.IsNullOrEmpty(uid))
                {
                    return;
                }

                string query = @"SELECT * FROM tbl_UserNotification WHERE Is_Read = 0 AND User_ID = @UID ORDER BY Created_At DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", uid);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            RepeaterReturn.DataSource = dt;
                            RepeaterReturn.DataBind();
                        }
                        else
                        {
                            lblNotification.Text = "No notifications found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblNotification.Text = $"Error: {ex.Message}";
            }
        }

        protected void RepeaterReturn_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string query = "SELECT Is_Approved FROM tbl_UserNotification WHERE User_ID = @UID AND Is_Read = 0 ORDER BY Created_At DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", uid);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bool isApproved = Convert.ToBoolean(dr["Is_Approved"]);
                            if (isApproved)
                            {
                                e.Item.Controls.Clear();
                                e.Item.Controls.Add(new LiteralControl(@"
                                    <div class='row pt-4 pb-4'>
                                        <div class='col text-center'>
                                            <h4><span class='badge text-bg-success'>Book returns approved</span></h4>
                                            <br />
                                            <svg width='90' height='90' xmlns='http://www.w3.org/2000/svg' viewBox='0 0 448 512'>
                                                <path d='M438.6 105.4c12.5 12.5 12.5 32.8 0 45.3l-256 256c-12.5 12.5-32.8 12.5-45.3 0l-128-128c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0L160 338.7 393.4 105.4c12.5-12.5 32.8-12.5 45.3 0z' />
                                            </svg>
                                        </div>
                                    </div>
                                "));
                            }
                            else
                            {
                                ;
                            }
                        }
                    }
                }
            }
        }

        protected void RepeaterReturn_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "MarkAsRead")
                {
                    string readQue = "UPDATE tbl_UserNotification SET Is_Read = 1 ";

                    using (SqlCommand cmd = new SqlCommand(readQue, conn))
                    {
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            RepeaterReturn.DataBind();
                            lblNotification.Text = "No notifications found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error" + ex.Message);
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

    }
}