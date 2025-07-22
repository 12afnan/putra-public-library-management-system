<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="BookCart.aspx.cs" Inherits="Putra_Public_Library_Management_System.BookCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- cart content-->
    <div class="container-fluid pt-5 mt-5 " style="min-height: calc(100vh - 195px);">
        <div class="container">
            <div class="row">
                <div class="col-xl-7 col-lg-7 col-md-7 col-sm-12 col-12 ">
                    <div class="row d-flex ms-auto">

                        <!-- Title -->
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4 class="my-4">My Book Cart </h4>
                                </center>
                            </div>
                        </div>
                        <!-- title end -->

                        <!-- line -->
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <p class="text-center">
                                    <asp:Label ID="lblCart" runat="server" Text="No books in cart"></asp:Label>
                                </p>
                            </div>
                        </div>

                        <div class="row d-flex justify-content-start">
                            <asp:Repeater ID="RepeaterCart" runat="server" DataSourceID="SqlDataSourceCart" OnItemCommand="RepeaterCart_ItemCommand">
                                <ItemTemplate>
                                    <!-- Publisher Header -->
                                    <div class="row mb-3">
                                        <div class="col-12">
                                            <h5 class="text-muted">by <%# Eval("Publisher_Name") %></h5>
                                        </div>
                                    </div>
                                    <!-- Book Item -->
                                    <div class="row mb-4">
                                        <div class="col-md-2">
                                            <asp:Image ID="ImageBook" runat="server"
                                                ImageUrl='<%# "data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("BookImage")) %>'
                                                CssClass="img-fluid rounded shadow-sm" />
                                        </div>
                                        <div class="col-md-8">
                                            <p class="fw-bolder fs-5"><%# Eval("BookName") %> </p>
                                            <p class="fst-italic"><%# Eval("Author_Name") %></p>
                                        </div>
                                        <div class="col-md-2 d-flex align-items-center">
                                            <asp:LinkButton ID="btnRemove" CssClass="btn btn-danger" runat="server" CommandName="RmvBook" CommandArgument='<%# Eval("BookID") %>'><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <asp:SqlDataSource ID="SqlDataSourceCart" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringBCart %>" SelectCommand="SELECT r.BorrowID, r.UserID, r.ReturnDate, l.CopyNumber, b.BookID, b.BookName, b.BookImage, b.Publisher_Name, b.Author_Name, b.BookGenre, r.IsCheckedOut FROM tbl_Borrow AS r INNER JOIN tbl_Borrow_List AS l ON r.BorrowID = l.BorrowID INNER JOIN tbl_BookMaster AS b ON l.BookID = b.BookID"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-xl-5 col-lg-5 col-md-5 col-sm-12 col-12">
                    
                </div>
            </div>
        </div>

    </div>

    <!-- fixed bottom navbar for check out -->
    <div class="row d-flex justify-content-center">
        <nav class="footer w-100 fixed-bottom" style="background-color: #9ACD32; padding: 10px 0;">
            <div class="row justify-content-center align-items-center px-4 px-md-0">

                <div class="col-xl-1 col-lg-2 d-none d-sm-block"></div>

                <div class="col-xl-10 col-lg-8 col-md-12 col-sm-12 col-12">

                    <div class="row">
                        <div class="col-6 d-flex justify-content-start align-items-center" style="font-size: 18px; padding: 10px 0; padding-left: 20px;">
                            <div class="d-flex align-items-center" style="font-weight: bold;">
                                <asp:Label ID="lblTotalBook" runat="server" Text="Label"></asp:Label>
                                &nbsp
                                <asp:Label ID="lblBIC" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>

                        <div class="col-6 d-flex justify-content-end align-items-center" style="font-size: 18px; padding: 10px 0; padding-right: 20px; cursor: pointer;">
                            <asp:Button ID="BtnCheckout" class="btn navbar-brand" runat="server" Text="Check out" OnClick="BtnCheckout_Click" />
                        </div>

                    </div>
                </div>

                <div class="col-xl-1 col-lg-2 d-none d-sm-block"></div>

            </div>
        </nav>

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


</asp:Content>
