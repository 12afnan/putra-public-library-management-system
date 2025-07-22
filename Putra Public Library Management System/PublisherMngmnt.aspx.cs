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
    public partial class PublisherMngmnt : System.Web.UI.Page
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

                GridViewPub.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            try
            {
                GetPubByID();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error : " + ex.Message);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckIfPubExists() == true)
                {
                    ShowMessageInfo("Author ID or name already exist !");
                }
                else
                {
                    AddNewPub();
                }
            }
            else
            {
                ShowMessageInfo("Please fill in all the fields !");
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckIfPubExists() == true)
                {
                    UpdatePub();
                }
                else
                {
                    ShowMessageInfo("Publisher ID or name doesnt exist !");
                }
            }
            else
            {
                ShowMessageInfo("Please fill in all the fields !");
            }
        }

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            if (HiddenFieldDelete.Value == "true" && Page.IsValid)
            {
                if (CheckIfIDExist())
                {
                    DeletePub();
                }
                else
                {
                    ShowMessageInfo("Publisher ID or name doesnt exist !");
                }

                HiddenFieldDelete.Value = string.Empty; // Reset the hidden field
            }
            else
            {
                ShowMessageWarn("Are you sure you want to delete this record?");
            }
        }

        void AddNewPub()
        {
            try
            {
                string pubidVal = TextBoxPubID.Text.Trim();
                string pubnameVal = TextBoxPubName.Text.Trim();

                string insertQue = "INSERT INTO tbl_PublisherMaster(Publisher_ID, Publisher_Name, DateAdded) VALUES (@pubID, @pubName, @dateAdded)";

                using (SqlCommand cmd = new SqlCommand(insertQue, conn))
                {
                    cmd.Parameters.AddWithValue("@pubID", pubidVal);
                    cmd.Parameters.AddWithValue("@pubName", pubnameVal);
                    cmd.Parameters.AddWithValue("@dateAdded", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    ShowMessageInfo("Publisher added successfully !");
                    ClearForm();

                    GridViewPub.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void UpdatePub()
        {
            try
            {
                string pubidVal = TextBoxPubID.Text.Trim();
                string pubnameVal = TextBoxPubName.Text.Trim();

                string updateQue = "UPDATE tbl_PublisherMaster SET Publisher_Name = @pubName WHERE Publisher_ID = '" + pubidVal + "'";

                using (SqlCommand cmd = new SqlCommand(updateQue, conn))
                {
                    //cmd.Parameters.AddWithValue("@pubID", pubidVal);
                    cmd.Parameters.AddWithValue("@pubName", pubnameVal);

                    cmd.ExecuteNonQuery();

                    ShowMessageInfo("Publisher updated successfully !");
                    ClearForm();

                    GridViewPub.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void DeletePub()
        {
            try
            {
                string pubidVal = TextBoxPubID.Text.Trim();

                string deleteQue = "DELETE FROM tbl_PublisherMaster WHERE Publisher_ID = '" + pubidVal + "'";

                using (SqlCommand cmd = new SqlCommand(deleteQue, conn))
                {
                    cmd.ExecuteNonQuery();

                    ShowMessageInfo("Publisher deleted successfully !");
                    ClearForm();

                    GridViewPub.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void GetPubByID()
        {
            try
            {
                string pubidVal = TextBoxPubID.Text.Trim();

                string selectQue = "SELECT * FROM tbl_PublisherMaster WHERE Publisher_ID = '" + pubidVal + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBoxPubName.Text = dr["Publisher_Name"].ToString();
                    }
                }
                else
                {
                    ShowMessageInfo("Publisher ID doesnt exist !");
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        //id and name 
        bool CheckIfIDExist()
        {
            try
            {
                string pubidVal = TextBoxPubID.Text.Trim();
                string pubnameVal = TextBoxPubName.Text.Trim();

                string selectQue = "SELECT * FROM tbl_PublisherMaster WHERE Publisher_ID = '" + pubidVal + "' AND Publisher_Name ='" + pubnameVal + "' ";

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

        //id or name
        bool CheckIfPubExists()
        {
            try
            {
                string pubidVal = TextBoxPubID.Text.Trim();
                string pubnameVal = TextBoxPubName.Text.Trim();

                string selectQue = "SELECT * FROM tbl_PublisherMaster WHERE Publisher_ID = '" + pubidVal + "' OR Publisher_Name= '" + pubnameVal + "' ";

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

        protected void GridViewPub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string pid = GridViewPub.SelectedDataKey.Value.ToString().Trim();

                string selectQue = "SELECT * FROM tbl_PublisherMaster WHERE Publisher_ID = '" + pid + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBoxPubID.Text = dr["Publisher_ID"].ToString().Trim();

                        TextBoxPubName.Text = dr["Publisher_Name"].ToString().Trim();
                    }
                }
                else
                {
                    ShowMessageInfo("Publisher ID doesnt exist !");
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }

        }

        //show message in modal
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

    }
}