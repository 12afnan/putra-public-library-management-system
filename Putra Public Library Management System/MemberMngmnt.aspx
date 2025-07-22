<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="MemberMngmnt.aspx.cs" Inherits="Putra_Public_Library_Management_System.MemberMngmnt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('modalButtonDel').addEventListener('click', function () {
                document.getElementById('<%= HiddenFieldDelete.ClientID %>').value = 'true';
                __doPostBack('<%= HiddenFieldDelete.UniqueID %>', '');
            });
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('images/management-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container-fluid pt-5 " style="min-height: calc(100vh - 95px);">
            <div class="row">

                <!-- left card -->
                <div class="col-md-5">
                    <div class="card">

                        <div class="card-body">

                            <!-- Title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>User Details </h4>
                                    </center>
                                </div>
                            </div>

                            <!-- Image -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img src="images\user.png" width="100" />
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
                                            <asp:TextBox class="form-control border border-black" ID="TextBoxUName" runat="server" placeholder="Username"></asp:TextBox>
                                            <label for="TextBoxUName">Username </label>
                                        </div>
                                        <asp:Button class="btn btn-info" ID="ButtonGo" runat="server" Text="Go" OnClick="ButtonGo_Click" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxUName" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxFullName" runat="server" placeholder="Full Name" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxFullName">Full Name </label>
                                    </div>
                                </div>

                                <div class="col-md-5">
                                    <div class="input-group">
                                        <div class="form-floating">
                                            <asp:TextBox class="form-control me-1 border border-black" ID="TextBoxStat" runat="server" placeholder="Status" ReadOnly="True"></asp:TextBox>
                                            <label for="TextBoxStat">Status </label>
                                        </div>
                                        <asp:LinkButton class="btn btn-success me-1" ID="LinkButtonAct" runat="server" OnClick="LinkButtonAct_Click"><i class="fa-solid fa-circle-check"></i></asp:LinkButton>

                                        <asp:LinkButton class="btn btn-warning me-1" ID="LinkButtonPen" runat="server" OnClick="LinkButtonPen_Click"><i class="fa-regular fa-circle-pause"></i></asp:LinkButton>

                                        <asp:LinkButton class="btn btn-danger me-1" ID="LinkButtonDiact" runat="server" OnClick="LinkButtonDiact_Click"><i class="fa-solid fa-circle-xmark"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                            <!-- 2ND ROW-->
                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxDOB" runat="server" placeholder="Date of Birth" ReadOnly="True" TextMode="Date"></asp:TextBox>
                                        <label for="TextBoxDOB">Date of Birth </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxPhoneNo" runat="server" placeholder="Phone Num" ReadOnly="True" TextMode="Phone"></asp:TextBox>
                                        <label for="TextBoxPhoneNo">Phone Num </label>
                                    </div>
                                </div>

                                <div class="col-md-5">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxEmail" runat="server" placeholder="Email" ReadOnly="True" TextMode="Email"></asp:TextBox>
                                        <label for="TextBoxEmail">Email </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- 3RD ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxState" runat="server" placeholder="State" TextMode="SingleLine" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxState">State </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxCity" runat="server" placeholder="City" TextMode="SingleLine" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxCity">City </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxPCode" runat="server" placeholder="Postcode" TextMode="Number" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxPCode">Postcode </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- Button -->
                            <div class="row">

                                <div class="col-12 col-md-6 mb-3">
                                    <asp:Button class="btn btn-danger w-100" ID="ButtonDel" runat="server" Text="Delete User" ToolTip="Delete selected user" OnClick="ButtonDel_Click" />
                                </div>

                                <div class="col-12 col-md-6 mb-3">
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
                                        <h4>User List </h4>
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
                                        <asp:GridView class="table table-striped table-borderless table-responsive table-hover" ID="GridViewUser" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="UserID" OnSelectedIndexChanged="GridViewUser_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                                <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" InsertVisible="False" SortExpression="UserID"></asp:BoundField>
                                                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username"></asp:BoundField>
                                                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"></asp:BoundField>
                                                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"></asp:BoundField>
                                                <asp:BoundField DataField="NRIC" HeaderText="NRIC" SortExpression="NRIC"></asp:BoundField>
                                                <asp:BoundField DataField="PhoneNum" HeaderText="Phone Number" SortExpression="PhoneNum"></asp:BoundField>
                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>
                                                <asp:BoundField DataField="DOB" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date of Birth" SortExpression="DOB"></asp:BoundField>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender"></asp:BoundField>
                                                <asp:BoundField DataField="Race" HeaderText="Race" SortExpression="Race"></asp:BoundField>
                                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State"></asp:BoundField>
                                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City"></asp:BoundField>
                                                <asp:BoundField DataField="Postcode" HeaderText="Postcode" SortExpression="Postcode"></asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"></asp:BoundField>
                                                <asp:BoundField DataField="RegDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Registration Date" SortExpression="RegDate"></asp:BoundField>
                                                <asp:BoundField DataField="UserType" HeaderText="UserType" SortExpression="UserType"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionString3 %>" SelectCommand="SELECT * FROM [tbl_UserMaster] WHERE [Is_Deleted] = 0 OR [Is_Deleted] IS NULL ORDER BY UserID"></asp:SqlDataSource>
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

        <!-- static backdrop & vertically centered, warning modal-->
        <div class="modal fade" id="messageModalWarn" tabindex="-1" aria-labelledby="messageModalLabelWarn" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="messageModalLabelWarn">Confirmation</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" id="modalMessageBodyWarn">
                        <!-- Message here -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" class="btn btn-danger" id="modalButtonDel">Yes</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="HiddenFieldDelete" runat="server" />

    </div>


</asp:Content>
