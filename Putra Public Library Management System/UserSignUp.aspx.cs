using System;
using System.Collections;
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
    public partial class UserSignUp : System.Web.UI.Page
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
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message);
            }
        }

        protected void ButtonSgnUp_Click(object sender, EventArgs e)
        {
            if (CheckUserExist() == true)
            {
                ShowMessage("Username already exist !");
            }
            else
            {
                SignUpNewUser();
            }
        }

        void SignUpNewUser()
        {
            string fnameVal = TxtBxFName.Text.Trim();
            string lnameVal = TxtBxLName.Text.Trim();

            string nricVal = TxtBxNRIC.Text.Trim();
            string phonenoVal = TxtBxPhoneNo.Text.Trim();
            string emailVal = TxtBxEmail.Text.Trim();

            string dobVal = TxtBxDOB.Text.Trim();
            string genVal = TxtBxGen.Text.Trim();
            string raceVal = DropDownListRace.SelectedItem.Value;

            // Access selected values on postback
            string selectedState = Request.Form[DropDownListState.UniqueID];
            string selectedCity = Request.Form[DropDownListCity.UniqueID];
            string selectedPostcode = Request.Form[DropDownListPCode.UniqueID];

            string usrnameVal = TxtBxUsrName.Text.Trim().ToLower();
            string pwdVal = TxtBxPwd.Text.Trim();

            if (string.IsNullOrWhiteSpace(TxtBxFName.Text) ||
                string.IsNullOrWhiteSpace(TxtBxLName.Text) ||
                string.IsNullOrWhiteSpace(TxtBxNRIC.Text) ||
                string.IsNullOrWhiteSpace(TxtBxPhoneNo.Text) ||
                string.IsNullOrWhiteSpace(TxtBxEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtBxDOB.Text) ||
                string.IsNullOrWhiteSpace(TxtBxGen.Text) ||
                string.IsNullOrWhiteSpace(TxtBxUsrName.Text) ||
                string.IsNullOrWhiteSpace(TxtBxPwd.Text))
            {
                ShowMessage("Please fill in all fields.");

                return;
            }

            string insertQue = "INSERT INTO tbl_UserMaster (FirstName,LastName,NRIC,PhoneNum,Email,DOB,Gender,Race,State,City,Postcode,Username,Password,Status,RegDate,UserType,Is_Deleted) VALUES " +
                "(@fname,@lname,@nric,@phoneno,@email,@dob,@gen,@race,@state,@city,@pcode,@usrname,@pwd,@stat,@regdate,'Member',0)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(insertQue, conn))
                {
                    cmd.Parameters.AddWithValue("@fname", fnameVal);
                    cmd.Parameters.AddWithValue("@lname", lnameVal);

                    cmd.Parameters.AddWithValue("@nric", nricVal);
                    cmd.Parameters.AddWithValue("@phoneno", phonenoVal);
                    cmd.Parameters.AddWithValue("@email", emailVal);

                    cmd.Parameters.AddWithValue("@dob", dobVal);
                    cmd.Parameters.AddWithValue("@gen", genVal);
                    cmd.Parameters.AddWithValue("@race", raceVal);

                    cmd.Parameters.AddWithValue("@state", selectedState);
                    cmd.Parameters.AddWithValue("@city", selectedCity);
                    cmd.Parameters.AddWithValue("@pcode", selectedPostcode);

                    cmd.Parameters.AddWithValue("@usrname", usrnameVal);
                    cmd.Parameters.AddWithValue("@pwd", pwdVal); // Consider hashing the password

                    cmd.Parameters.AddWithValue("@regdate", DateTime.Now.ToString("yyyy-MM-dd")); // Use DateTime directly
                    cmd.Parameters.AddWithValue("@stat", "Pending");

                    cmd.ExecuteNonQuery();
                    ShowMessage("Sign up Successful!");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message);
            }
        }

        bool CheckUserExist()
        {
            try
            {
                string selectQue = "SELECT * FROM tbl_UserMaster WHERE Username = @usrname";

                SqlCommand cmd = new SqlCommand(selectQue, conn);
                cmd.Parameters.AddWithValue("@usrname", TxtBxUsrName.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
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
                ShowMessage("Error: " + ex.Message);

                return false;
            }
        }

        //show message in modal
        private void ShowMessage(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBody').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModal')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script);
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
                if (c is TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                else if (c is DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = 0;
                }
                else if (c.Controls.Count > 0)
                {
                    ClearControl(c);
                }
            }
        }

    }
}