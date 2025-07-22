<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="LibrarianMngmnt.aspx.cs" Inherits="Putra_Public_Library_Management_System.LibrarianMngmnt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/management-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container-fluid pt-5" style="min-height: calc(100vh - 95px);">
            <div class="row">

                <!-- left card -->
                <div class="col-md-5">
                    <div class="card">

                        <div class="card-body">

                            <!-- Title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>Register Librarian </h4>
                                        <span class="badge text-bg-warning">Active user only</span>
                                    </center>
                                </div>
                            </div>
                            <br />
                            <!-- Image -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img src="images\librarian.png" width="100" />
                                    </center>
                                </div>
                            </div>

                            <!-- line -->
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>

                            <!-- 1ST ROW-->
                            <div class="row">

                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="form-floating">
                                            <asp:TextBox class="form-control border border-black" ID="TextBoxUID" runat="server" placeholder="User ID"></asp:TextBox>
                                            <label for="TextBoxUID">User ID </label>
                                        </div>
                                        <asp:Button class="btn btn-info" ID="ButtonGo" runat="server" Text="Go" OnClick="ButtonGo_Click" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxUID" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxUName" runat="server" placeholder="Username" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxUName">Username </label>
                                    </div>
                                </div>

                                <div class="col-md-5">
                                    <div class="input-group">
                                        <div class="form-floating">
                                            <asp:TextBox class="form-control border border-black" ID="TextBoxStat" runat="server" placeholder="Status" ReadOnly="True"></asp:TextBox>
                                            <label for="TextBoxStat">Status </label>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <!-- 2ND ROW-->
                            <div class="row">

                                <div class="col-md-5">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxFullName" runat="server" placeholder="Full Name" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxFullName">Full Name </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- Button -->
                            <div class="row">

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-primary w-100" ID="ButtonReg" runat="server" Text="Register" ToolTip="Assign as librarian" OnClick="ButtonReg_Click" />
                                </div>

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-danger w-100" ID="ButtonDel" runat="server" Text="Delete" ToolTip="Delete librarian" OnClick="ButtonDel_Click" />
                                </div>

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-secondary w-100" ID="ButtonClear" runat="server" Text="Clear" ToolTip="Clear all textboxes" OnClick="ButtonClear_Click" />
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
                                        <h4>List of Librarian</h4>
                                    </center>
                                </div>
                            </div>

                            <!-- line -->
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>

                            <!-- table -->
                            <div class="row">
                                <div class="col">
                                    <div style="overflow-x: auto;">
                                        <asp:GridView class="table table-striped table-borderless table-responsive table-hover" ID="GridViewLibrarian" runat="server" DataSourceID="SqlDataSourceLibrarian" AutoGenerateColumns="False" OnSelectedIndexChanged="GridViewLibrarian_SelectedIndexChanged" DataKeyNames="UserID">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                                <asp:BoundField DataField="UserID" HeaderText="User ID" SortExpression="UserID"></asp:BoundField>
                                                <asp:BoundField DataField="Date_Added" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Added" SortExpression="Date_Added"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:SqlDataSource ID="SqlDataSourceLibrarian" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringLibrarian %>" SelectCommand="SELECT * FROM [tbl_LibrarianMaster]" ProviderName="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringLibrarian.ProviderName %>"></asp:SqlDataSource>
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
