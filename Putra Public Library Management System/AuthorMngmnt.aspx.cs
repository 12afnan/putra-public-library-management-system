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
    public partial class AuthorMngmnt : System.Web.UI.Page
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


                GridViewAuthor.DataBind();
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
                GetAuthorbyID();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error " + ex.Message);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckIfAuthorExists() == true)
                {
                    ShowMessageInfo("Author with the same ID or name already exist !");
                }
                else
                {
                    AddNewAuthor();
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
                if (CheckIfAuthorExists() == true)
                {
                    UpdateAuthor();
                }
                else
                {
                    ShowMessageInfo("Author ID or name doesnt exist !");
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
                if (CheckIfAuthorExists())
                {
                    DeleteAuthor();
                }
                else
                {
                    ShowMessageInfo("Author ID or name doesnt exist !");
                }

                HiddenFieldDelete.Value = string.Empty; // Reset the hidden field
            }
            else
            {
                ShowMessageWarn("Are you sure you want to delete this record?");
            }
        }

        void AddNewAuthor()
        {
            try
            {
                string authidVal = TextBoxAuthID.Text.Trim();
                string authnameVal = TextBoxAuthName.Text.Trim();

                string insertQue = "INSERT INTO tbl_AuthorMaster(Author_ID, Author_Name, DateAdded) VALUES (@authID, @authName, @dateAdded)";

                using (SqlCommand cmd = new SqlCommand(insertQue, conn))
                {
                    cmd.Parameters.AddWithValue("@authID", authidVal);
                    cmd.Parameters.AddWithValue("@authName", authnameVal);
                    cmd.Parameters.AddWithValue("@dateAdded", DateTime.Now);

                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        ShowMessageInfo("Author added successfully !");
                        ClearForm();
                    }

                    GridViewAuthor.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void UpdateAuthor()
        {
            try
            {
                string authidVal = TextBoxAuthID.Text.Trim();
                string authnameVal = TextBoxAuthName.Text.Trim();

                string updateQue = "UPDATE tbl_AuthorMaster SET Author_Name = @authName WHERE Author_ID = '" + authidVal + "'";

                using (SqlCommand cmd = new SqlCommand(updateQue, conn))
                {
                    cmd.Parameters.AddWithValue("@authName", authnameVal);
 
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ShowMessageInfo("Author updated successfully !");
                        ClearForm();

                    }
                    else
                    {
                        ShowMessageInfo("Update failed !");
                        ClearForm();
                    }

                    GridViewAuthor.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void DeleteAuthor()
        {
            try
            {
                string authidVal = TextBoxAuthID.Text.Trim();

                string deleteQue = "DELETE FROM tbl_AuthorMaster WHERE Author_ID = '" + authidVal + "'";

                using (SqlCommand cmd = new SqlCommand(deleteQue, conn))
                {

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ShowMessageInfo("Author deleted successfully !");
                        ClearForm();
                    }
                }
                GridViewAuthor.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }

        }

        void GetAuthorbyID()
        {
            try
            {
                string authidVal = TextBoxAuthID.Text.Trim();

                string selectQue = "SELECT * FROM tbl_AuthorMaster WHERE Author_ID = '" + authidVal + "' ";

                SqlCommand cmd = new SqlCommand(selectQue, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBoxAuthName.Text = dr["Author_Name"].ToString();
                    }
                }
                else
                {
                    ShowMessageInfo("Author ID doesnt exist !");
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        //id or name
        bool CheckIfAuthorExists()
        {
            try
            {
                string authidVal = TextBoxAuthID.Text.Trim();
                string authnameVal = TextBoxAuthName.Text.Trim();

                string selectQue = "SELECT * FROM tbl_AuthorMaster WHERE Author_ID = '" + authidVal + "' OR Author_Name= '" + authnameVal + "' ";

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

        protected void GridViewAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string aid = GridViewAuthor.SelectedDataKey.Value.ToString().Trim();

                if (!string.IsNullOrEmpty(aid))
                {
                    TextBoxAuthID.Text = aid;
                    GetAuthorbyID();
                }
                else
                {
                    ShowMessageInfo("No author selected!");
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }

        }

        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBody').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModal')); myModal.show();</script>";
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