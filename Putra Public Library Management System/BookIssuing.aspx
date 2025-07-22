<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="BookIssuing.aspx.cs" Inherits="Putra_Public_Library_Management_System.BookIssuing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function ()
        {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/management-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container-fluid pt-5 pb-5" style="min-height: calc(100vh - 96px);">
            <div class="row">
                <!-- left card -->
                <div class="col-md-5">
                    <div class="card">

                        <div class="card-body">

                            <!-- Title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>Book Issuing </h4>
                                    </center>
                                </div>
                            </div>

                            <!-- Image -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img src="images\book-issue.png" width="100" />
                                    </center>
                                </div>
                            </div>

                            <!-- line -->
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>

                            <!-- 1ST ROW -->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="input-group">
                                        <div class="form-floating">
                                            <asp:TextBox class="form-control border border-black" ID="TextBoxBID" runat="server" placeholder="Book ID"></asp:TextBox>
                                            <label for="TextBoxBID">Book ID </label>
                                        </div>
                                        <asp:Button class="btn btn-warning" ID="ButtonGo" runat="server" Text="Go" OnClick="ButtonGo_Click" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxBID" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxBrwrID" runat="server" placeholder="Borrower ID"></asp:TextBox>
                                        <label for="TextBoxBrwrID">Borrower ID </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxAvaCpy" runat="server" placeholder="Available Copy" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxAvaCpy">Available Copy </label>
                                    </div>
                                </div>
                            </div>

                            <br />

                            <!-- 2ND ROW-->
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxBName" runat="server" placeholder="Book Name" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxBName">Book Name </label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxBrwrName" runat="server" placeholder="Borrower Name" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxBrwrName">Borrower Name </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- 3RD ROW-->
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxIssDate" runat="server" TextMode="Date" ReadOnly="True"></asp:TextBox>
                                        <label>Issue Date </label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxRetDate" runat="server" TextMode="Date" ReadOnly="True"></asp:TextBox>
                                        <label>Return Date </label>
                                    </div>
                                </div>

                            </div>

                            <br />
                            <!-- Buttons -->
                            <div class="row">
                                <div class="col-12 col-md-6 mb-3">
                                    <asp:Button class="btn btn-primary w-100" ID="ButtonIss" runat="server" Text="Issue" OnClick="ButtonIss_Click" />
                                </div>

                                <div class="col-12 col-md-6 mb-3">
                                    <asp:Button class="btn btn-info w-100" ID="ButtonClear" runat="server" Text="Clear" OnClick="ButtonClear_Click" />
                                </div>
                            </div>

                            <a class="btn icon-link" href="home.aspx">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">
                                    <!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.-->
                                    <path d="M575.8 255.5c0 18-15 32.1-32 32.1l-32 0 .7 160.2c0 2.7-.2 5.4-.5 8.1l0 16.2c0 22.1-17.9 40-40 40l-16 0c-1.1 0-2.2 0-3.3-.1c-1.4 .1-2.8 .1-4.2 .1L416 512l-24 0c-22.1 0-40-17.9-40-40l0-24 0-64c0-17.7-14.3-32-32-32l-64 0c-17.7 0-32 14.3-32 32l0 64 0 24c0 22.1-17.9 40-40 40l-24 0-31.9 0c-1.5 0-3-.1-4.5-.2c-1.2 .1-2.4 .2-3.6 .2l-16 0c-22.1 0-40-17.9-40-40l0-112c0-.9 0-1.9 .1-2.8l0-69.7-32 0c-18 0-32-14-32-32.1c0-9 3-17 10-24L266.4 8c7-7 15-8 22-8s15 2 21 7L564.8 231.5c8 7 12 15 11 24z" />
                                </svg>
                                Go home
                            </a>

                        </div>
                    </div>

                </div>

                <!-- right card -->
                <div class="col-md-7">
                    <div class="card">

                        <div class="card-body">

                            <!-- Title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>Borrow & Return Book Record </h4>
                                    </center>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <div style="overflow-x: auto;"> 
                                        <asp:GridView class="table table-striped table-bordered table-responsive" ID="GridViewIssue" runat="server" DataSourceID="SqlDataSourceIssue" AutoGenerateColumns="False" DataKeyNames="BorrowID">
                                            <Columns>
                                                <asp:BoundField DataField="BorrowID" HeaderText="Borrow ID" ReadOnly="True" InsertVisible="False" SortExpression="BorrowID"></asp:BoundField>
                                                <asp:BoundField DataField="UserID" HeaderText="User ID" SortExpression="UserID"></asp:BoundField>
                                                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"></asp:BoundField>
                                                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"></asp:BoundField>
                                                <asp:BoundField DataField="BookID" HeaderText="Book ID" SortExpression="BookID"></asp:BoundField>
                                                <asp:BoundField DataField="CopyNumber" HeaderText="Copy Number" SortExpression="CopyNumber"></asp:BoundField>
                                                <asp:BoundField DataField="IssueDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Borrowed" SortExpression="IssueDate"></asp:BoundField>
                                                <asp:BoundField DataField="ReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Exp. Date Return" SortExpression="ReturnDate"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Return Status" SortExpression="Return_Status">
                                                    <ItemTemplate>
                                                        <%# Convert.ToInt32(Eval("Return_Status")) == 0 ? "No" : "Yes" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:SqlDataSource ID="SqlDataSourceIssue" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringAdminIssue %>" ProviderName="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringAdminIssue.ProviderName %>" SelectCommand="SELECT r.BorrowID, r.UserID, u.FirstName, u.LastName, l.BookID, l.CopyNumber, r.IssueDate, r.ReturnDate, l.Return_Status FROM tbl_UserMaster AS u INNER JOIN tbl_Borrow AS r ON u.UserID = r.UserID INNER JOIN tbl_Borrow_List AS l ON r.BorrowID = l.BorrowID"></asp:SqlDataSource>
                                </div>
                            </div>

                            <br />

                        </div>
                    </div>
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

    </div>

</asp:Content>
