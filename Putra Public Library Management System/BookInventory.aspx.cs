using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class BookInventory : System.Web.UI.Page
    {
        readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        static byte[] global_filebin;
        static int global_act_copy, global_ava_copy, global_iss_copy;

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

                GridViewBook.DataBind();
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
                GetBookByID();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error " + ex.Message);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                if (CheckBookExist())
                {
                    ShowMessageInfo("Book ID or name already exist!");
                }
                else
                {
                    AddNewBook();
                }
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                if (CheckBookExist())
                {
                    UpdateBook();
                }
                else
                {
                    ShowMessageInfo("Book ID or name not found!");
                }
            }
        }

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (HiddenFieldDelete.Value == "true" && Page.IsValid)
                {
                    if (CheckBookExist())
                    {
                        DeleteBook();
                    }
                    else
                    {
                        ShowMessageInfo("Book ID or name not found!");
                    }
                    HiddenFieldDelete.Value = string.Empty; // Reset the hidden field
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

        void AddNewBook()
        {
            try
            {
                string genres = GetSelectedGenres();
                byte[] imageData = GetUploadedFileData();

                if (imageData == null)
                {
                    ShowMessageInfo("Image size should not exceed 2MB.");
                    return;
                }

                string insertQue = "INSERT INTO tbl_BookMaster (BookID, BookName, BookGenre, BookImage, Author_Name, Publisher_Name, PublishYear, Language, Edition, BookCost, PageNumber, BookDescription, CopyNumber, CopyAvailable) " +
                                    "VALUES (@bid, @bname, @bgen, @bimg, @authid, @pname, @pdate, @lang, @edi, @bcost, @pagenum, @bdesc, @copynum, @copyava)";

                using (SqlCommand cmd = new SqlCommand(insertQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bimg", imageData);
                    cmd.Parameters.AddWithValue("@bid", TextBoxBID.Text.Trim());
                    cmd.Parameters.AddWithValue("@bname", TextBoxBName.Text.Trim());
                    cmd.Parameters.AddWithValue("@lang", DropDownListLang.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@authid", DropDownListAuthName.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@bgen", genres);
                    cmd.Parameters.AddWithValue("@pname", DropDownListPubName.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@pdate", TextBoxPubDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@edi", TextBoxEdi.Text.Trim());
                    cmd.Parameters.AddWithValue("@bcost", TextBoxCost.Text.Trim());
                    cmd.Parameters.AddWithValue("@pagenum", TextBoxPage.Text.Trim());
                    cmd.Parameters.AddWithValue("@copynum", TextBoxActCopy.Text.Trim());
                    cmd.Parameters.AddWithValue("@copyava", TextBoxActCopy.Text.Trim());
                    cmd.Parameters.AddWithValue("@bdesc", TextBoxBDesc.Text.Trim());

                    InsertBookCopy(TextBoxActCopy.Text.Trim());

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ShowMessageInfo("Book added successfully!");
                    }
                    //cmd.ExecuteNonQuery();
                }

                
                ClearForm();
                GridViewBook.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void InsertBookCopy(string copy)
        {
            try
            {
                string insertQue = "INSERT INTO tbl_BookCopy (CopyNumber, BookID, Status, CopyPrice) VALUES (@copynum, @bid, @stat, @cprice)";

                for (int i = 1; i <= Convert.ToInt32(copy); i++)
                {
                    using (SqlCommand cmd = new SqlCommand(insertQue, conn))
                    {
                        cmd.Parameters.AddWithValue("@copynum", i);
                        cmd.Parameters.AddWithValue("@bid", TextBoxBID.Text.Trim());
                        cmd.Parameters.AddWithValue("@stat", "Available");
                        cmd.Parameters.AddWithValue("@cprice", TextBoxCost.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void UpdateBook()
        {
            try
            {
                int act_copy = Convert.ToInt32(TextBoxActCopy.Text.Trim());
                int ava_copy = Convert.ToInt32(TextBoxAvaCopy.Text.Trim());

                if (global_act_copy != act_copy)
                {
                    if (act_copy < global_iss_copy)
                    {
                        ShowMessageInfo("Copies must meet or exceed the number of issued books.");
                        return;
                    }
                    else
                    {
                        ava_copy = act_copy - global_iss_copy;
                        TextBoxAvaCopy.Text = ava_copy.ToString();
                    }
                }

                string genres = GetSelectedGenres();
                byte[] imageData = GetUploadedFileData();

                if (imageData == null)
                {
                    ShowMessageInfo("Image size should not exceed 2MB.");
                    return;
                }

                string updateQue = "UPDATE tbl_BookMaster SET BookName = @bname, BookGenre = @bgen, BookImage = @bimg, Author_Name = @authname, Publisher_Name = @pname, PublishYear = @pdate, Language = @lang, Edition = @edi, BookCost = @bcost, PageNumber = @pagenum, BookDescription = @bdesc, CopyNumber = @copynum, CopyAvailable = @copyava " +
                                    "WHERE BookID = @bid";

                using (SqlCommand cmd = new SqlCommand(updateQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bimg", imageData);
                    cmd.Parameters.AddWithValue("@bname", TextBoxBName.Text.Trim());
                    cmd.Parameters.AddWithValue("@lang", DropDownListLang.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@authname", DropDownListAuthName.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@bgen", genres);
                    cmd.Parameters.AddWithValue("@pname", DropDownListPubName.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@pdate", TextBoxPubDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@edi", TextBoxEdi.Text.Trim());
                    cmd.Parameters.AddWithValue("@bcost", TextBoxCost.Text.Trim());
                    cmd.Parameters.AddWithValue("@pagenum", TextBoxPage.Text.Trim());
                    cmd.Parameters.AddWithValue("@copynum", act_copy);
                    cmd.Parameters.AddWithValue("@copyava", ava_copy);
                    cmd.Parameters.AddWithValue("@bdesc", TextBoxBDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@bid", TextBoxBID.Text.Trim());

                    cmd.ExecuteNonQuery();
                    ShowMessageInfo("Book updated successfully!");
                }

                ClearForm();
                GridViewBook.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void DeleteBook()
        {
            try
            {
                string deleteQue = "DELETE FROM tbl_BookMaster WHERE BookID = @bid";
                using (SqlCommand cmd = new SqlCommand(deleteQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bid", TextBoxBID.Text.Trim());
                    cmd.ExecuteNonQuery();
                    ShowMessageInfo("Book deleted successfully!");
                }
                ClearForm();
                GridViewBook.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        void GetBookByID()
        {
            try
            {
                string selectQue = "SELECT * FROM tbl_BookMaster WHERE BookID = @bid";
                using (SqlCommand cmd = new SqlCommand(selectQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bid", TextBoxBID.Text.Trim());
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            PopulateFormFields(dr);
                        }
                    }
                    else
                    {
                        ShowMessageInfo("Book ID not found!");
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        bool CheckBookExist()
        {
            try
            {
                string selectQue = "SELECT * FROM tbl_BookMaster WHERE BookID = @bid OR BookName = @bname";
                using (SqlCommand cmd = new SqlCommand(selectQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bid", TextBoxBID.Text.Trim());
                    cmd.Parameters.AddWithValue("@bname", TextBoxBName.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        protected void GridViewBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectQue = "SELECT * FROM tbl_BookMaster WHERE BookID = @bid";
                using (SqlCommand cmd = new SqlCommand(selectQue, conn))
                {
                    cmd.Parameters.AddWithValue("@bid", GridViewBook.SelectedDataKey.Value.ToString().Trim());
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            PopulateFormFields(dr);
                        }
                    }
                    else
                    {
                        ShowMessageInfo("Book ID not found!");
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
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

        private bool IsFormValid()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBoxBName.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(DropDownListLang.SelectedItem.Text) ||
                    string.IsNullOrWhiteSpace(DropDownListAuthName.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(DropDownListPubName.SelectedItem.Text) ||
                    string.IsNullOrWhiteSpace(TextBoxPubDate.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(TextBoxEdi.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(TextBoxCost.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(TextBoxPage.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(TextBoxActCopy.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(TextBoxBDesc.Text.Trim()))
                {
                    ShowMessageInfo("Please fill in all fields.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
                return false;
            }
        }

        private string GetSelectedGenres()
        {
            string genres = "";
            foreach (int i in ListBoxGen.GetSelectedIndices())
            {
                genres += ListBoxGen.Items[i] + ", ";
            }
            return genres.TrimEnd(',', ' ');
        }

        private byte[] GetUploadedFileData()
        {
            string imagePath = FileUploadImg.PostedFile.FileName;
            string fileName = Path.GetFileName(imagePath);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return global_filebin;
            }
            else
            {
                if (FileUploadImg.PostedFile.ContentLength > 2097152) // 2MB
                {
                    return null;
                }
                string savePath = Server.MapPath("~/Images/" + fileName);
                FileUploadImg.SaveAs(savePath);
                return File.ReadAllBytes(savePath);
            }
        }

        private void PopulateFormFields(SqlDataReader dr)
        {
            TextBoxBID.Text = dr["BookID"].ToString().Trim();

            TextBoxBName.Text = dr["BookName"].ToString().Trim();
            DropDownListLang.SelectedItem.Text = dr["Language"].ToString();
            DropDownListAuthName.SelectedItem.Text = dr["Author_Name"].ToString();

            global_act_copy = Convert.ToInt32(dr["CopyNumber"].ToString().Trim());
            global_ava_copy = Convert.ToInt32(dr["CopyAvailable"].ToString().Trim());
            global_iss_copy = global_act_copy - global_ava_copy;
            global_filebin = (byte[])dr["BookImage"];

            DropDownListPubName.SelectedItem.Text = dr["Publisher_Name"].ToString();
            TextBoxPubDate.Text = Convert.ToDateTime(dr["PublishYear"]).ToString("yyyy-MM-dd");
            TextBoxEdi.Text = dr["Edition"].ToString().Trim();
            TextBoxCost.Text = dr["BookCost"].ToString().Trim();
            TextBoxPage.Text = dr["PageNumber"].ToString().Trim();
            TextBoxActCopy.Text = dr["CopyNumber"].ToString().Trim();
            TextBoxAvaCopy.Text = dr["CopyAvailable"].ToString().Trim();
            TextBoxIssCopy.Text = (global_act_copy - global_ava_copy).ToString();
            TextBoxBDesc.Text = dr["BookDescription"].ToString();

            ListBoxGen.ClearSelection();
            string[] genres = dr["BookGenre"].ToString().Split(',');
            foreach (string genre in genres)
            {
                ListItem item = ListBoxGen.Items.FindByText(genre.Trim());
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }
    }
}