using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Putra_Public_Library_Management_System
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        private string totalBooks, totalMembers, booksIssuedToday, totalLibrarian;

        protected void Page_Load(object sender, EventArgs e)
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

            if (!IsPostBack)
            {
                LoadSummaryData();
                LoadChartData();
            }
        }

        private void LoadSummaryData()
        {

            // Fetch total books
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbl_BookMaster", conn))
            {
                totalBooks = command.ExecuteScalar().ToString();

                lblTotalBooks.Text = command.ExecuteScalar().ToString();
            }

            // Fetch total members
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbl_UserMaster", conn))
            {
                totalMembers = command.ExecuteScalar().ToString();

                lblTotalMembers.Text = command.ExecuteScalar().ToString();
            }

            // Fetch books issued today
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbl_Borrow WHERE IssueDate = CAST(GETDATE() AS DATE)", conn))
            {
                booksIssuedToday = command.ExecuteScalar().ToString();

                lblBooksIssuedToday.Text = command.ExecuteScalar().ToString();
            }

            // Fetch books returned today
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbl_LibrarianMaster", conn))
            {
                totalLibrarian = command.ExecuteScalar().ToString();

                lblLibrarian.Text = command.ExecuteScalar().ToString();
            }

        }

        private void LoadChartData()
        {
            try
            {
                // Fetch data for Books Issued Per Month chart
                using (SqlCommand cmd = new SqlCommand("SELECT MONTH(IssueDate) AS Month, COUNT(*) AS Count FROM tbl_Borrow GROUP BY MONTH(IssueDate)", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    string[] months = new string[12];
                    int[] counts = new int[12];

                    foreach (DataRow row in dt.Rows)
                    {
                        int month = Convert.ToInt32(row["Month"]);
                        months[month - 1] = new DateTime(2025, month, 1).ToString("MMMM");
                        counts[month - 1] = Convert.ToInt32(row["Count"]);
                    }

                    
                    ClientScript.RegisterStartupScript(this.GetType(), "BooksIssuedChartData", $"var booksIssuedData = [{string.Join(",", counts)}]; renderBooksIssuedChart(booksIssuedData);", true);
                }

                // Fetch data for Books Returned Per Month chart
                //using (SqlCommand cmd = new SqlCommand("SELECT MONTH(ReturnDate) AS Month, COUNT(*) AS Count FROM tbl_Borrow GROUP BY MONTH(ReturnDate)", conn))
                //{
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);

                //    string[] months = new string[12];
                //    int[] counts = new int[12];

                //    foreach (DataRow row in dt.Rows)
                //    {
                //        int month = Convert.ToInt32(row["Month"]);
                //        months[month - 1] = new DateTime(2025, month, 1).ToString("MMMM");
                //        counts[month - 1] = Convert.ToInt32(row["Count"]);
                //    }

                //    // Pass data to JavaScript
                //    ClientScript.RegisterStartupScript(this.GetType(), "BooksReturnedChartData", $" booksReturnedData = [{string.Join(",", counts)}]; renderBooksReturnedChart(booksReturnedData);", true);
                //}

                // Fetch data for gender distribution chart
                using (SqlCommand cmd = new SqlCommand("SELECT Gender, COUNT(*) AS Count FROM tbl_UserMaster GROUP BY Gender", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int[] genderData = new int[2]; // Assuming only Male and Female
                    foreach (DataRow row in dt.Rows)
                    {
                        string gender = row["Gender"].ToString().Trim();
                        int count = Convert.ToInt32(row["Count"]);
                        if (gender == "Male")
                        {
                            genderData[0] = count;
                        }
                        else if (gender == "Female")
                        {
                            genderData[1] = count;
                        }
                    }

                    
                    ClientScript.RegisterStartupScript(this.GetType(), "GenderChartData", $"var genderData = [{string.Join(",", genderData)}]; renderGenderChart(genderData);", true);
                }

                
                using (SqlCommand cmd = new SqlCommand("SELECT State, COUNT(*) AS Count FROM tbl_UserMaster GROUP BY State", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    Dictionary<string, int> stateData = new Dictionary<string, int>
                    {
                        { "Johor", 0 }, { "Kedah", 0 }, { "Kelantan", 0 }, { "Wp Kuala Lumpur", 0 },
                        { "Wp Labuan", 0 }, { "Melaka", 0 }, { "Negeri Sembilan", 0 }, { "Pahang", 0 },
                        { "Penang", 0 }, { "Perak", 0 }, { "Perlis", 0 }, { "Wp Putrajaya", 0 },
                        { "Sabah", 0 }, { "Sarawak", 0 }, { "Selangor", 0 }, { "Terengganu", 0 }
                    };

                    foreach (DataRow row in dt.Rows)
                    {
                        string state = row["State"].ToString().Trim();
                        int count = Convert.ToInt32(row["Count"]);
                        if (stateData.ContainsKey(state))
                        {
                            stateData[state] = count;
                        }
                    }

                    
                    ClientScript.RegisterStartupScript(this.GetType(), "StateChartData", $"var stateData = [{string.Join(",", stateData.Values)}]; renderStateChart(stateData);", true);
                }

                

            }
            catch (Exception ex)
            {
                ShowMessageInfo("Error: " + ex.Message);
            }
        }

        protected void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            // Generate  report
            string report = GenerateReport();

            // Display report
            lblReport.Text = report.Replace(Environment.NewLine, "<br />");

            LoadSummaryData();
            LoadChartData();
        }

        private string GenerateReport()
        {
            StringBuilder reportBuilder = new StringBuilder();


            LoadSummaryData();

            // Generate report content 
            reportBuilder.AppendLine("<table border='1' class='table table-hover table-striped-columns table-stripped' style='width:100%; border-collapse: collapse;'>");
            reportBuilder.AppendLine("<tr><th colspan='2'>Library Management System Report</th></tr>");
            reportBuilder.AppendLine("<tr><td>Total Books</td><td>" + totalBooks + "</td></tr>");
            reportBuilder.AppendLine("<tr><td>Total Members</td><td>" + totalMembers + "</td></tr>");
            reportBuilder.AppendLine("<tr><td>Books Issued Today</td><td>" + booksIssuedToday + "</td></tr>");
            reportBuilder.AppendLine("<tr><td>Total Librarian</td><td>" + totalLibrarian + "</td></tr>");
            reportBuilder.AppendLine("</table>");

            return reportBuilder.ToString();
        }
        private void ShowMessageInfo(string message)
        {
            string script = $"<script>document.getElementById('modalMessageBodyInfo').innerText = '{message}';" +
                            $" var myModal = new bootstrap.Modal(document.getElementById('messageModalInfo')); myModal.show();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageInfo", script);
        }



    }
}