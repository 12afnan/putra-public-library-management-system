<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="UserIssuingHistory.aspx.cs" Inherits="Putra_Public_Library_Management_System.UserIssuingHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/sign-up-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container pt-5 pb-5" style="min-height: calc(100vh - 97px);">
            <div class="row ">
                <div class="col-md-6 mt-4">
                    <div class="card shadow-lg">
                        <div class="card-header bg-primary text-white">
                            <h3 class="mb-0">Borrowing History</h3>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <!-- GridView for Borrow History -->
                                <asp:GridView ID="GridViewHistory" class="table table-striped table-bordered table-responsive" runat="server" DataSourceID="SqlDataSourceHistory" AutoGenerateColumns="False" DataKeyNames="BorrowID" OnSelectedIndexChanged="GridViewHistory_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                        <asp:BoundField DataField="BorrowID" HeaderText="Borrow ID" ReadOnly="True" InsertVisible="False" SortExpression="BorrowID"></asp:BoundField>
                                        <asp:BoundField DataField="IssueDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Issue Date" SortExpression="IssueDate"></asp:BoundField>
                                        <asp:BoundField DataField="ReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Expected Return Date" SortExpression="ReturnDate"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>

                    </div>
                    <asp:SqlDataSource ID="SqlDataSourceHistory" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringBorrowHistory %>" SelectCommand="SELECT BorrowID, UserID, IssueDate, ReturnDate FROM tbl_Borrow WHERE (UserID = @uid) AND (IsCheckedOut = 1) ORDER BY BorrowID DESC">
                        <SelectParameters>
                            <asp:Parameter Name="uid"></asp:Parameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>

                <div class="col-md-6 mt-4">
                    <div class="card shadow-lg">
                        <div class="card-header bg-primary text-white">
                            <h3 class="mb-0">Books Information</h3>
                        </div>
                        <div class="card-body">

                            <!-- label -->
                            <div class="row">
                                <div class="col">
                                    <p class="text-center">
                                        <asp:Label ID="lblBInfo" runat="server" Text="Select A History"></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <!-- Repeater for Book Details -->
                            <asp:Repeater ID="RepeaterBookDetails" runat="server">
                                <HeaderTemplate>
                                    <div class="list-group" style="max-height: 400px; overflow-y: auto;">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="list-group-item">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <img src='<%# "data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("BookImage")) %>' class="img-fluid" alt="Book Image" />
                                            </div>
                                            <div class="col-md-10">
                                                <h5 class="mb-1"><%# Eval("BookName") %></h5>
                                                <p class="mb-1 text-muted">Author: <%# Eval("Author_Name") %></p>
                                                <p class="mb-1 text-muted">Publisher: <%# Eval("Publisher_Name") %></p>
                                                <p class="mb-1 text-muted">Return Status: <%# Eval("Return_Status").ToString() == "0" ? "No" : "Yes" %></p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
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
