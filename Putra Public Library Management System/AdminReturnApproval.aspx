<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="AdminReturnApproval.aspx.cs" Inherits="Putra_Public_Library_Management_System.AdminReturnApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/management-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container mt-5 pt-5" style="min-height: calc(100vh - 125px);">
            <div class="row">
                <div class="col-md-7 mx-auto">
                    <div class="card shadow-lg" style="background-color: #e9ecef;">
                        <div class="card-body" style="background-color: #dee2e6;">

                            <!-- title -->
                            <div class="row">
                                <div class="col pb-5">
                                    <center>
                                        <h3>Return Approval List</h3>
                                    </center>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col text-center">
                                    <asp:Label ID="lblNotification" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">

                                    <asp:Repeater ID="RepeaterApprove" runat="server" OnItemCommand="RepeaterApprove_ItemCommand" DataSourceID="SqlDataSourceAdminApproval">
                                        <ItemTemplate>
                                            <div class="row mb-3">
                                                <div class="col-md-4">
                                                    <p><%# Eval("Message") %></p>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col">
                                                    <hr />
                                                </div>
                                            </div>

                                            <div class="row mb-3">
                                                <div class="col">
                                                    <p class="fw-bold">Total Fine Amount:</p>
                                                </div>
                                                <div class="col text-right">
                                                    <p>RM <%# Eval("Fine_Amount", "{0:F2}") %></p>
                                                </div>
                                            </div>

                                            <div class="row pt-4 pb-4">
                                                <div class="col text-center">
                                                    <asp:Button ID="ButtonApprove" class="btn btn-warning" runat="server" Text="Approve" CommandName="ApproveReturn" CommandArgument='<%# Eval("Not_ID") %>' />
                                                </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <asp:SqlDataSource ID="SqlDataSourceAdminApproval" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringaAdminApproval %>" ProviderName="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringaAdminApproval.ProviderName %>" SelectCommand="SELECT Not_ID, User_ID, Message, Fine_Amount, Is_Approved, Is_Read, Created_At FROM tbl_UserNotification WHERE (Is_Approved = 0) ORDER BY Created_At DESC"></asp:SqlDataSource>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
