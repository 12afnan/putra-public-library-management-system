<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Putra_Public_Library_Management_System.UserProfile" EnableViewState="False" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function ()
        {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid pt-5 pb-5">
        <div class="row">

            <!-- left card -->
            <div class="col-md-6">
                <div class="card">

                    <div class="card-body">

                        <!-- Image -->
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="images\user.png" width="100" />
                                </center>
                            </div>
                        </div>

                        <!-- Title -->
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Your Profile </h4>

                                    <span>Account Status : </span>
                                    <asp:Label class="badge rounded-pill text-bg-success" ID="lblAccStatus" runat="server" Text="Status"></asp:Label>
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

                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxFName" runat="server" placeholder="First Name" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxFName">First Name</label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxLName" runat="server" placeholder="Last Name" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxFName">Last Name</label>
                                </div>
                            </div>

                        </div>

                        <br />

                        <!-- 2ND ROW-->
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxPNum" runat="server" placeholder="Phone Num" TextMode="Phone" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxPNum">Phone Num </label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxEml" runat="server" placeholder="Email" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxEml">Email </label>
                                </div>
                            </div>
                        </div>

                        <br />

                        <!-- 3RD ROW-->
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxState" runat="server" placeholder="State" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxState">State</label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxCity" runat="server" placeholder="City" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxCity">City</label>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxPtCode" runat="server" placeholder="Postcode" ReadOnly="true"></asp:TextBox>
                                    <label for="TextBoxPtCode">Postcode</label>
                                </div>
                            </div>

                        </div>

                        <br />

                        <!-- Credential Badge -->
                        <div class="row">
                            <div class="col">
                                <center>
                                    <span class="badge text-bg-info">Login Credentials</span>
                                </center>
                            </div>
                        </div>

                        <br />

                        <!-- 5TH ROW-->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxUName" runat="server" placeholder="Username" ReadOnly="True"></asp:TextBox>
                                    <label for="TextBoxUName">Username </label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxPwd" runat="server" placeholder="Password" TextMode="Password" ReadOnly="True"></asp:TextBox>
                                    <label for="TextBoxPwd">Password </label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-floating">
                                    <asp:TextBox class="form-control border border-black" ID="TextBoxNwPwd" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                                    <label for="TextBoxNwPwd">New Password</label>
                                </div>
                            </div>
                        </div>


                        <br />
                        <!-- Update Button -->
                        <div class="row">
                            <div class="col">
                                <div class="d-grid gap-2 col-4 mx-auto">
                                    <asp:Button class="btn btn-warning" ID="ButtonUpdate" runat="server" Text="Update Password" OnClick="ButtonUpdate_Click" />
                                </div>
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
            <div class="col-md-6">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary ">
                        <h3 class="mb-0">My Books</h3>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <asp:GridView ID="GridViewReturnBooks" class="table table-hover table-borderless table-responsive" runat="server" DataSourceID="SqlDataSourceReturnBooks" AutoGenerateColumns="False" DataKeyNames="BorrowID" OnSelectedIndexChanged="GridViewReturnBooks_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                        <asp:BoundField DataField="BorrowID" HeaderText="Borrow ID" ReadOnly="True" InsertVisible="False" SortExpression="BorrowID"></asp:BoundField>
                                        <asp:BoundField DataField="IssueDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Issue Date" SortExpression="IssueDate"></asp:BoundField>
                                        <asp:BoundField DataField="ReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Expected Return Date" SortExpression="ReturnDate"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSourceReturnBooks" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringReturnBooks %>" SelectCommand="SELECT BorrowID, IssueDate, ReturnDate FROM tbl_Borrow WHERE (UserID = @uid) AND (IsCheckedOut = 1) ORDER BY BorrowID DESC">
                                    <SelectParameters>
                                        <asp:Parameter Name="uid"></asp:Parameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
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

    <!-- books in a borrow modal -->
    <div class="modal fade" id="bReturnMod" tabindex="-1" aria-labelledby="bReturnModLbl" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <h3 class="modal-title" id="bReturnModLbl">Return books</h3>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" id="bReturnModBody">
                    <asp:Repeater ID="RptBooks" runat="server" OnItemDataBound="RptBooks_ItemDataBound">
                        <HeaderTemplate>
                            <div class="row">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-md-4 mb-5 d-flex">
                                <div class="p-1 text-center">
                                    <asp:Image ID="imgBook" runat="server" ImageUrl='<%# "data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("BookImage")) %>' Style="width: 150px; height: 200px; object-fit: cover;" />
                                    <h4 class="fw-bold"><%# Eval("BookName") %></h4>
                                    <h6><%# Eval("Author_Name") %></h6>
                                    <h6><%# Eval("Publisher_Name") %></h6>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <!-- button -->
                <div class="modal-footer">
                    <div class="d-flex justify-content-center w-100">
                        <asp:Button ID="BtnReturn" class="btn btn-danger mt-auto" runat="server" Text="Return" OnClick="BtnReturn_Click" CommandName="ReturnBks"  />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- bottom left notification (toasts)-->
    <div class="toast-container position-fixed bottom-0 end-0 p-3">
        <div id="liveToast" class="toast align-items-center" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">

                <strong class="me-auto">Notification</strong>
                <small class="text-body-secondary">Just now</small>
                <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                <!-- msg -->
            </div>
        </div>

    </div>

        <!-- pwd modal -->
    <div class="modal fade" id="ModalPwd" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalLblPwd" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Confirm password</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" id="ModalBodyPwd">
                    <div class="mb-3">
                        <label for="message-text" class="col-form-label">Old password</label>
                        <input id="message-text" class="form-control border border-black" type="text" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnConfirm" class="btn btn-secondary" runat="server" Text="Confirm" OnClick="BtnConfirm_Click" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>
