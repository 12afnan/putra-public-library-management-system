<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="AuthorMngmnt.aspx.cs" Inherits="Putra_Public_Library_Management_System.AuthorMngmnt" %>
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
                                        <h4>Author Details </h4>
                                    </center>
                                </div>
                            </div>

                            <!-- Image -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img src="images\author.png" width="100" />
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
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <div class="form-floating">
                                            <asp:TextBox class="form-control border border-black" ID="TextBoxAuthID" runat="server" placeholder="Author ID"></asp:TextBox>
                                            <label for="TextBoxAuthID">Author ID</label>
                                        </div>
                                        <asp:Button class="btn btn-info" ID="ButtonGo" runat="server" Text="Go" OnClick="ButtonGo_Click" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxAuthID" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-8">

                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxAuthName" runat="server" placeholder="Author Name"></asp:TextBox>
                                        <label for="TextBoxAuthName">Author Name </label>
                                    </div>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxAuthName" Font-Italic="True" Font-Size="Small" ForeColor="Red" ValidationGroup="AddGrp"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxAuthName" Font-Italic="True" Font-Size="Small" ForeColor="Red" ValidationGroup="UpdGrp"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxAuthName" Font-Italic="True" Font-Size="Small" ForeColor="Red" ValidationGroup="DelGrp"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <br />

                            <!-- Buttons -->
                            <div class="row">
                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-primary w-100" ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" ValidationGroup="AddGrp" />

                                </div>

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-warning w-100" ID="ButtonUpd" runat="server" Text="Update" OnClick="ButtonUpd_Click" ValidationGroup="UpdGrp" />

                                </div>

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-danger w-100" ID="ButtonDel" runat="server" Text="Delete" OnClick="ButtonDel_Click" ValidationGroup="DelGrp" />
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
                                        <h4>Author List </h4>
                                    </center>
                                </div>
                            </div>

                            <!-- line -->
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <div style="overflow-x: auto;">
                                        <asp:GridView class="table table-striped table-bordered table-responsive" ID="GridViewAuthor" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnSelectedIndexChanged="GridViewAuthor_SelectedIndexChanged" DataKeyNames="Author_ID">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                                <asp:BoundField DataField="Author_ID" HeaderText="Author ID" SortExpression="Author_ID"></asp:BoundField>
                                                <asp:BoundField DataField="Author_Name" HeaderText="Author Name" SortExpression="Author_Name"></asp:BoundField>
                                                <asp:BoundField DataField="DateAdded" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Added" SortExpression="DateAdded"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionString %>" SelectCommand="SELECT * FROM [tbl_AuthorMaster]"></asp:SqlDataSource>
                                </div>
                            </div>

                            <br />

                        </div>
                    </div>
                </div>
            </div>

        </div>

        <!-- Normal Modal -->
        <div class="modal fade" id="messageModal" tabindex="-1" aria-labelledby="messageModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="messageModalLabel">Message</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" id="modalMessageBody">
                        <!-- Message here -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- static backdrop & vertically centered modal-->
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

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('modalButtonDel').addEventListener('click', function () {
                document.getElementById('<%= HiddenFieldDelete.ClientID %>').value = 'true';
                __doPostBack('<%= HiddenFieldDelete.UniqueID %>', '');
            });
        });
    </script>


</asp:Content>
