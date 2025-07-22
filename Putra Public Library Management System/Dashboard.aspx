<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Putra_Public_Library_Management_System.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/management-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container " style="min-height: calc(100vh - 97px);">

            <div class="row">
                <div class="col">
                    <h2 class="text-center text-white mt-5 mb-4">Admin and Librarian Dashboard</h2>
                </div>
            </div>

            <!-- Summary Section -->
            <div class="row text-center mb-4">
                <div class="col-md-3">
                    <div class="card shadow-sm bg-primary text-white">
                        <div class="card-body">
                            <h5 class="card-title">Total Books</h5>
                            <h3>
                                <asp:Label ID="lblTotalBooks" runat="server" />
                            </h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="card shadow-sm bg-success text-white">
                        <div class="card-body">
                            <h5 class="card-title">Total Members</h5>
                            <h3>
                                <asp:Label ID="lblTotalMembers" runat="server" />
                            </h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="card shadow-sm bg-warning text-dark">
                        <div class="card-body">
                            <h5 class="card-title">Books Issued Today</h5>
                            <h3>
                                <asp:Label ID="lblBooksIssuedToday" runat="server" />
                            </h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="card shadow-sm bg-danger text-white">
                        <div class="card-body">
                            <h5 class="card-title">Total Librarian</h5>
                            <h3>
                                <asp:Label ID="lblLibrarian" runat="server" />
                            </h3>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Chart Section -->
            <div class="row">
                <div class="col-md-6 mb-4">
                    <div class="card shadow-sm" style="height: 450px;">
                        <div class="card-header bg-primary text-white">
                            Books Issued Per Month
                        </div>
                        <div class="card-body d-flex justify-content-center align-items-center">
                            <canvas id="booksIssuedChart" style="max-width: 100%; max-height: 100%;"></canvas>
                        </div>
                    </div>
                </div>

                <div class="col-md-6 mb-4">
                    <div class="card shadow-sm" style="height: 450px;">
                        <div class="card-header bg-success text-white">
                            Books Returned Per Month
                        </div>
                        <div class="card-body d-flex justify-content-center align-items-center">
                            <canvas id="booksReturnedChart" style="max-width: 100%; max-height: 100%;"></canvas>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 mb-4">
                    <div class="card shadow-sm" style="height: 450px;">
                        <div class="card-header bg-info text-white">
                            User's Gender Distribution
                        </div>
                        <div class="card-body d-flex justify-content-center align-items-center">
                            <canvas id="genderChart" style="max-width: 80%; max-height: 80%;"></canvas>
                        </div>
                    </div>
                </div>

                <div class="col-md-6 mb-4">
                    <div class="card shadow-sm" style="height: 450px;">
                        <div class="card-header bg-secondary text-white">
                            User's State Distribution
                        </div>
                        <div class="card-body d-flex justify-content-center align-items-center">
                            <canvas id="stateChart" style="max-width: 850%; max-height: 90%;"></canvas>
                        </div>
                    </div>
                </div>

            </div>

            <!-- Generate Report Button -->
            <div class="row text-center mb-4">
                <div class="col-md-12">
                    <asp:Button ID="BtnGenerateReport" runat="server" Text="Generate Report" CssClass="btn btn-primary" OnClick="BtnGenerateReport_Click" />
                </div>
            </div>

            <!-- Report Section -->
            <div class="row text-center mb-4">
                <div class="col-md-12">
                    <asp:Label ID="lblReport" runat="server" CssClass="text-white" />
                </div>
            </div>

        </div>

        <!-- normal modal -->
        <div class="modal fade" id="messageModalInfo" tabindex="-1" aria-labelledby="messageModalLabelInfo" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="messageModalLabelInfo">Message</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" id="modalMessageBodyInfo">
                        <!-- Message here -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <script>
            
            const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            var booksIssuedData = [];
            var booksReturnedData = [];

            var issuedPerDayData = [];
            var returnedPerDayData = [];

            var genderData = [];
            var stateData = [];

            // Books Issued Chart
            function renderBooksIssuedChart(data) {
                const ctx1 = document.getElementById('booksIssuedChart').getContext('2d');
                const booksIssuedChart = new Chart(ctx1, {
                    type: 'line',
                    data: {
                        labels: months,
                        datasets: [{
                            label: 'Books Issued',
                            data: booksIssuedData,
                            backgroundColor: 'rgba(75, 192, 192, 0.2)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 2,
                            fill: true
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { position: 'top' }
                        },
                        scales: {
                            y: {

                                beginAtZero: true,
                                stepSize: 10,
                                max: 100
                            }
                        }
                    }
                });
            }

            // Books Returned Chart
            function renderBooksReturnedChart(data) {
                const ctx2 = document.getElementById('booksReturnedChart').getContext('2d');
                const booksReturnedChart = new Chart(ctx2, {
                    type: 'line',
                    data: {
                        labels: months,
                        datasets: [{
                            label: 'Books Returned',
                            data: booksReturnedData,
                            backgroundColor: 'rgba(153, 102, 255, 0.2)',
                            borderColor: 'rgba(153, 102, 255, 1)',
                            borderWidth: 2,
                            fill: true
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { position: 'top' }
                        },
                        scales: {
                            y: {

                                beginAtZero: true,
                                stepSize: 10,
                                max: 100

                            }
                        }

                    }
                });
            }

            // Gender distribution pie chart
            function renderGenderChart(data) {
                const ctxGender = document.getElementById('genderChart').getContext('2d');
                const genderChart = new Chart(ctxGender, {
                    type: 'pie',
                    data: {
                        labels: ['Male', 'Female'],
                        datasets: [{
                            data: genderData,
                            backgroundColor: ['rgba(54, 162, 235, 0.7)', 'rgba(255, 99, 132, 0.7)'],
                            borderColor: ['rgba(54, 162, 235, 1)', 'rgba(255, 99, 132, 1)'],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            }
                        }
                    }
                });
            }

            // State distribution pie chart
            function renderStateChart(data) {
                const ctxState = document.getElementById('stateChart').getContext('2d');
                const stateChart = new Chart(ctxState, {
                    type: 'pie',
                    data: {
                        labels: ['Johor', 'Kedah', 'Kelantan', 'Wp Kuala Lumpur', 'Wp Labuan', 'Melaka', 'Negeri Sembilan', 'Pahang', 'Penang', 'Perak', 'Perlis', 'Wp Putrajaya', 'Sabah', 'Sarawak', 'Selangor', 'Terengganu'],
                        datasets: [{
                            data: stateData,
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.7)', 'rgba(54, 162, 235, 0.7)', 'rgba(255, 206, 86, 0.7)', 'rgba(75, 192, 192, 0.7)', 'rgba(153, 102, 255, 0.7)', 'rgba(255, 159, 64, 0.7)', 'rgba(199, 199, 199, 0.7)', 'rgba(83, 102, 255, 0.7)', 'rgba(255, 99, 132, 0.7)', 'rgba(54, 162, 235, 0.7)', 'rgba(255, 206, 86, 0.7)', 'rgba(75, 192, 192, 0.7)', 'rgba(153, 102, 255, 0.7)', 'rgba(255, 159, 64, 0.7)', 'rgba(199, 199, 199, 0.7)', 'rgba(83, 102, 255, 0.7)'
                            ],
                            borderColor: [
                                'rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)', 'rgba(199, 199, 199, 1)', 'rgba(83, 102, 255, 1)', 'rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)', 'rgba(199, 199, 199, 1)', 'rgba(83, 102, 255, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            }
                        }
                    }
                });
            }
        </script>
    </div>

</asp:Content>
