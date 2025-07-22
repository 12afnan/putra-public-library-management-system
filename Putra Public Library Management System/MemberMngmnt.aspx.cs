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
    public partial class MemberMngmnt : System.Web.UI.Page
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


                GridViewUser.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error " + ex.Message);

            }
        }

        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            try
            {
                GetUserByUsername();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error " + ex.Message);
            }
        }

        protected void LinkButtonAct_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateUserStat("Active");
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void LinkButtonPen_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateUserStat("Pending");
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void LinkButtonDiact_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateUserStat("Inactive");
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
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

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (HiddenFieldDelete.Value == "true" && Page.IsValid)
                {
                    if(CheckUsernameExist())
                    {
                        DeleteUser();
                    }
                    else
                    {
                        ShowMessageInfo("Username doesnt exist !");
                    }

                    HiddenFieldDelete.Value = string.Empty;
                }
                else
                {
                    ShowMessageWarn("Are you sure you want to delete this record?");
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void GetUserByUsername()
        {
            try
            {
                string unameVal = TextBoxUName.Text.Trim();

                string selectQue = "SELECT * FROM tbl_UserMaster WHERE Username = '" + unameVal + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBoxFullName.Text = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                        TextBoxStat.Text = dr["Status"].ToString().Trim();

                        TextBoxDOB.Text = dr["DOB"].ToString();
                        TextBoxPhoneNo.Text = dr["PhoneNum"].ToString();
                        TextBoxEmail.Text = dr["Email"].ToString();

                        TextBoxState.Text = dr["State"].ToString();
                        TextBoxCity.Text = dr["City"].ToString();
                        TextBoxPCode.Text = dr["Postcode"].ToString();
                    }
                }
                else
                {
                    ShowMessageInfo("Username doesnt exist !");
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void UpdateUserStat(string stat)
        {
            try
            {
                string usernameVal = TextBoxUName.Text.Trim();

                string updateQue = "UPDATE tbl_UserMaster SET Status = '" + stat + "' WHERE Username = '" + usernameVal + "'";

                using (SqlCommand cmd = new SqlCommand(updateQue, conn))
                {
                    cmd.ExecuteNonQuery();

                    ShowMessageInfo("Account status updated successfully !");
                    ClearForm();

                    GridViewUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            } 
        }

        void DeleteUser()
        {
            try
            {
                string unameVal = TextBoxUName.Text.Trim();

                //string deleteQue = "DELETE FROM tbl_UserMaster WHERE Username = '" + unameVal + "'";

                string deleteQue = "UPDATE tbl_UserMaster SET Is_Deleted = 1, Status = 'Inactive' WHERE Username = '" + unameVal + "'";


                using (SqlCommand cmd = new SqlCommand(deleteQue, conn))
                {
                    cmd.ExecuteNonQuery();

                    ShowMessageInfo("User deleted successfully !");
                    ClearForm();

                    GridViewUser.DataBind();
                }
            }
            catch(Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        bool CheckUsernameExist()
        {
            try
            {
                string unameVal = TextBoxUName.Text.Trim();

                string selectQue = "SELECT * FROM tbl_UserMaster WHERE Username = '" + unameVal + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
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

        private void ShowMessageWarn(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyWarn').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalWarn')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageWarn", script);
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

        protected void GridViewUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string uid = GridViewUser.SelectedDataKey.Value.ToString().Trim();

                string selectQue = "SELECT * FROM tbl_UserMaster WHERE UserID = '" + uid + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBoxUName.Text = dr["Username"].ToString().Trim();

                        TextBoxFullName.Text = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                        TextBoxStat.Text = dr["Status"].ToString().Trim();

                        TextBoxDOB.Text = dr["DOB"].ToString();
                        TextBoxPhoneNo.Text = dr["PhoneNum"].ToString();
                        TextBoxEmail.Text = dr["Email"].ToString();

                        TextBoxState.Text = dr["State"].ToString();
                        TextBoxCity.Text = dr["City"].ToString();
                        TextBoxPCode.Text = dr["Postcode"].ToString();
                    }
                }
                else
                {
                    ShowMessageInfo("Username doesnt exist !");
                }

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }


        }
    }
}