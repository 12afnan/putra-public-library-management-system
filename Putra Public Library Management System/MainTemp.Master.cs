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
    public partial class MainTemp : System.Web.UI.MasterPage
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        private string uID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                if (Session["Role"] == null)
                {
                    DefaultView();
                }
                else if (Session["Role"].Equals("User"))
                {
                    GetUserID();
                    uID = Session["UserID"].ToString().Trim();
                    //LoadNotification();

                    HtmlAnchor BtnAdminMenu = (HtmlAnchor)FindControl("BtnAdminMenu");
                    BtnAdminMenu.Visible = false;

                    HtmlAnchor BtnUsrLogin = (HtmlAnchor)FindControl("BtnUsrLogin");
                    BtnUsrLogin.Visible = false;

                    HtmlAnchor BtnSignUp = (HtmlAnchor)FindControl("BtnSignUp");
                    BtnSignUp.Visible = false;

                    LinkButtonLgt.Visible = true; //logout butt
                    LinkButtonHllUsr.Visible = true; //hello user
                    LinkButtonHllUsr.Text = "Hello, " + Session["Username"].ToString();

                    HtmlAnchor BtnAdLogin = (HtmlAnchor)FindControl("BtnAdLogin");
                    BtnAdLogin.Visible = false; // admin login butt

                    HtmlAnchor BtnAuthMngmnt = (HtmlAnchor)FindControl("BtnAuthMngmnt");
                    BtnAuthMngmnt.Visible = false; //author mngmnt butt

                    HtmlAnchor BtnPubMngmnt = (HtmlAnchor)FindControl("BtnPubMngmnt");
                    BtnPubMngmnt.Visible = false; //publisher mngmnt butt

                    HtmlAnchor BtnBInventory = (HtmlAnchor)FindControl("BtnBInventory");
                    BtnBInventory.Visible = false; //book inv butt

                    HtmlAnchor BtnBIssue = (HtmlAnchor)FindControl("BtnBIssue");
                    BtnBIssue.Visible = false; //book issue butt

                    HtmlAnchor BtnBRet = (HtmlAnchor)FindControl("BtnBRet");
                    BtnBRet.Visible = false; //book return butt

                    HtmlAnchor BtnUserMngmnt = (HtmlAnchor)FindControl("BtnUserMngmnt");
                    BtnUserMngmnt.Visible = false; //member mngmnt butt
                }
                else if (Session["Role"].Equals("Admin"))
                {
                    GetUserID();
                    uID = Session["UserID"].ToString().Trim();
                    //LoadNotification();

                    HtmlAnchor BtnUsrLogin = (HtmlAnchor)FindControl("BtnUsrLogin");
                    BtnUsrLogin.Visible = false;

                    HtmlAnchor BtnSignUp = (HtmlAnchor)FindControl("BtnSignUp");
                    BtnSignUp.Visible = false;

                    LinkButtonLgt.Visible = true; //logout butt
                    LinkButtonHllUsr.Visible = true; //hello user
                    LinkButtonHllUsr.Text = "Hello! Admin";

                    HtmlAnchor BtnAdLogin = (HtmlAnchor)FindControl("BtnAdLogin");
                    BtnAdLogin.Visible = false; // admin login butt

                    HtmlAnchor BtnAuthMngmnt = (HtmlAnchor)FindControl("BtnAuthMngmnt");
                    BtnAuthMngmnt.Visible = true; //author mngmnt butt

                    HtmlAnchor BtnPubMngmnt = (HtmlAnchor)FindControl("BtnPubMngmnt");
                    BtnPubMngmnt.Visible = true; //publisher mngmnt butt

                    HtmlAnchor BtnBInventory = (HtmlAnchor)FindControl("BtnBInventory");
                    BtnBInventory.Visible = true; //book inv butt

                    HtmlAnchor BtnBIssue = (HtmlAnchor)FindControl("BtnBIssue");
                    BtnBIssue.Visible = true; //book issue butt

                    HtmlAnchor BtnBRet = (HtmlAnchor)FindControl("BtnBRet");
                    BtnBRet.Visible = true; //book return butt

                    HtmlAnchor BtnUserMngmnt = (HtmlAnchor)FindControl("BtnUserMngmnt");
                    BtnUserMngmnt.Visible = true; //member mngmnt butt

                    HtmlAnchor BtnLibMngmnt = (HtmlAnchor)FindControl("BtnLibMngmnt");
                    BtnLibMngmnt.Visible = true; //librarian mngmnt butt

                }
                else if (Session["Role"].Equals("Librarian"))
                {
                    HtmlAnchor BtnUsrLogin = (HtmlAnchor)FindControl("BtnUsrLogin");
                    BtnUsrLogin.Visible = false;

                    HtmlAnchor BtnSignUp = (HtmlAnchor)FindControl("BtnSignUp");
                    BtnSignUp.Visible = false;

                    LinkButtonLgt.Visible = true; //logout butt
                    LinkButtonHllUsr.Visible = true; //hello user
                    LinkButtonHllUsr.Text = "Hello! Librarian";

                    HtmlAnchor BtnAdLogin = (HtmlAnchor)FindControl("BtnAdLogin");
                    BtnAdLogin.Visible = false; // admin login butt

                    HtmlAnchor BtnAuthMngmnt = (HtmlAnchor)FindControl("BtnAuthMngmnt");
                    BtnAuthMngmnt.Visible = false; //author mngmnt butt

                    HtmlAnchor BtnPubMngmnt = (HtmlAnchor)FindControl("BtnPubMngmnt");
                    BtnPubMngmnt.Visible = false; //publisher mngmnt butt

                    HtmlAnchor BtnBInventory = (HtmlAnchor)FindControl("BtnBInventory");
                    BtnBInventory.Visible = true; //book inv butt

                    HtmlAnchor BtnBIssue = (HtmlAnchor)FindControl("BtnBIssue");
                    BtnBIssue.Visible = true; //book issue butt

                    HtmlAnchor BtnBRet = (HtmlAnchor)FindControl("BtnBRet");
                    BtnBRet.Visible = true; //book return butt

                    HtmlAnchor BtnUserMngmnt = (HtmlAnchor)FindControl("BtnUserMngmnt");
                    BtnUserMngmnt.Visible = true; //member mngmnt butt

                    HtmlAnchor BtnLibMngmnt = (HtmlAnchor)FindControl("BtnLibMngmnt");
                    BtnLibMngmnt.Visible = false; //librarian mngmnt butt
                }


                //LoadNotification();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        //default user view settings
        void DefaultView()
        {
            //uncomment once dah siap
            HtmlAnchor BtnAdminMenu = (HtmlAnchor)FindControl("BtnAdminMenu");
            BtnAdminMenu.Visible = false;

            HtmlAnchor BtnUsrLogin = (HtmlAnchor)FindControl("BtnUsrLogin");
            BtnUsrLogin.Visible = true;


            HtmlAnchor BtnSignUp = (HtmlAnchor)FindControl("BtnSignUp");
            BtnSignUp.Visible = true;

            LinkButtonLgt.Visible = false; //logout butt
            LinkButtonHllUsr.Visible = false; //hello user

            HtmlAnchor BtnAdLogin = (HtmlAnchor)FindControl("BtnAdLogin");
            BtnAdLogin.Visible = true; // admin login butt

            HtmlAnchor BtnAuthMngmnt = (HtmlAnchor)FindControl("BtnAuthMngmnt");
            BtnAuthMngmnt.Visible = false; //author mngmnt butt

            HtmlAnchor BtnPubMngmnt = (HtmlAnchor)FindControl("BtnPubMngmnt");
            BtnPubMngmnt.Visible = false; //publisehr mngmnt butt

            HtmlAnchor BtnBInventory = (HtmlAnchor)FindControl("BtnBInventory");
            BtnBInventory.Visible = false; //book inv butt

            HtmlAnchor BtnBIssue = (HtmlAnchor)FindControl("BtnBIssue");
            BtnBIssue.Visible = false; //book issue butt

            HtmlAnchor BtnBRet = (HtmlAnchor)FindControl("BtnBRet");
            BtnBRet.Visible = false; //book return butt

            HtmlAnchor BtnUserMngmnt = (HtmlAnchor)FindControl("BtnUserMngmnt");
            BtnUserMngmnt.Visible = false; //member mngmnt butt
        }

        protected void LinkButtonLgt_Click(object sender, EventArgs e)
        {
            DefaultView();

            Session.Abandon();

            //Session.Clear();

            string script = " <script> " +
                "document.addEventListener('DOMContentLoaded', () => { " +
                "const logoutAlert = document.getElementById('logoutAlert');" +
                "const wrapper = document.createElement('div');" +
                "wrapper.innerHTML = `" +
                    "<div class='alert alert-danger alert-dismissible fade show position-fixed top-0 start-50 translate-middle-x' style='z-index: 1050;' role='alert'>" +
                    "<i class=\"fa-solid fa-triangle-exclamation\"></i> &nbsp;" +
                        "You have been logged out." +
                    "</div>`;" +
                "logoutAlert.append(wrapper);" +

                "setTimeout(() => {" +
                    "window.location.href = 'home.aspx';" +
                "}, 1000);" +
            "}); " +
            "</script>";


            ScriptManager.RegisterStartupScript(this, GetType(), "LogoutAlertScript", script, false);
        }

        void LoadNotification()
        {
            string notiQuery = "SELECT Message FROM tbl_UserNotification WHERE User_ID = @UID AND Is_Read = 0";
            using (SqlCommand cmd = new SqlCommand(notiQuery, conn))
            {
                cmd.Parameters.AddWithValue("@UID", uID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //lblNoNotif.Visible = false;

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        //GridViewNoti.DataSource = dt;
                        //GridViewNoti.DataBind();
                    }
                    else
                    {
                        //lblNoNotif.Text = "No new notifications.";
                        //GridViewNoti.Visible = false;
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

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageInfo", script, false);
        }


    }
}