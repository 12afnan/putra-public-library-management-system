<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="ReturnNotification.aspx.cs" Inherits="Putra_Public_Library_Management_System.ReturnNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/library-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
    <div class="container mt-5 pt-5" style="min-height: calc(100vh - 129px);">
        <div class="row">
            <div class="col-md-7 mx-auto">
                <div class="card shadow-lg" style="background-color: #e9ecef;">
                    <div class="card-body" style="background-color: #dee2e6;">

                        <!-- title -->
                        <div class="row">
                            <div class="col pb-5">
                                <center>
                                    <h3>Return Approval Status</h3>
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

                                <asp:Repeater ID="RepeaterReturn" runat="server" OnItemCommand="RepeaterReturn_ItemCommand" OnItemDataBound="RepeaterReturn_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <p><%# Eval("Message") %></p>
                                            </div>
                                            <div class="col-md-4 ms-auto text-right">
                                                <asp:Button ID="BtnRead" runat="server" Text="Mark as read" CssClass="btn btn-sm text-body-secondary" CommandName="MarkAsRead" CommandArgument='<%# Eval("Not_ID") %>' />
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
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div class="row pt-4 pb-4">
                                            <div class="col text-center">
                                                <h4><span class="badge text-bg-info">In progress of approval</span></h4>
                                                <br />
                                                <div class="spinner-border text-info mt-3" role="status" style="width: 3rem; height: 3rem;">
                                                    <span class="visually-hidden">Loading...</span>
                                                </div>
                                            </div>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    </div>

</asp:Content>
