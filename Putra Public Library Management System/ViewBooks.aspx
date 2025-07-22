<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="ViewBooks.aspx.cs" Inherits="Putra_Public_Library_Management_System.ViewBooks" EnableEventValidation="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        // Function to filter books dynamically
        function filterBooks() {
            const searchInput = document.getElementById("txtSearch").value;
            const books = document.querySelectorAll('.book-card'); // Use the correct class for each book card

            books.forEach(book => {
                const bookName = book.querySelector('.card-title').textContent.toLowerCase();
                if (bookName.includes(searchInput)) {
                    book.style.display = 'block'; 
                } else {
                    book.style.display = 'none'; 
                }
            });
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('images/collection-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">

        <div class="container-fluid pt-5 pb-5 mt-5 mb-5" style="min-height: calc(100vh - 125px);">

            <!-- Search bar -->
            <nav class="navbar bg-body-tertiary">
                <div class="container-fluid">
                    <div class="d-flex" role="search">
                        <asp:TextBox ID="txtSearch" class="form-control border border-black me-2" type="text" placeholder="by title,genre,author..." runat="server"></asp:TextBox>
                        <asp:Button ID="BtnSearch" class="btn btn-outline-success" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                    </div>
                </div>
            </nav>

            <!-- Books display -->
            <div class="row">
                <div class="col-md-12 mx-auto">
                    <div class="card">
                        <div class="card-body">

                            <!-- Title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h3>Collection of Books</h3>
                                    </center>
                                </div>
                            </div>

                            <!-- Line -->
                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>

                            <div class="row" id="RepeaterContainer">
                                <asp:Repeater ID="RepeaterVwBks" runat="server" OnItemCommand="RepeaterVwBks_ItemCommand">
                                    <ItemTemplate>
                                        <div class="book-card col-md-2 mb-5 d-flex ">
                                            <div class="card" style="width: 100%;">
                                                <asp:Image ID="ImageBks" class="card-img-top" Style="object-fit: cover; height: 200px;" runat="server" ImageUrl='<%# "data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("BookImage")) %>' />
                                                <div class="card-body d-flex flex-column">
                                                    <h5 class="card-title text-truncate"><%# Eval("BookName") %></h5>
                                                    <asp:Button ID="buttonDetails" class="btn btn-primary mt-auto" runat="server" Text="Details" CommandName="VwDetails" CommandArgument='<%# Eval("BookID") %>' />
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>

                            <asp:SqlDataSource ID="SqlDataSourceVw" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringVwBks %>" SelectCommand="SELECT * FROM [tbl_BookMaster]"></asp:SqlDataSource>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- book details modal -->
        <div class="modal fade" id="bDetailsMod" tabindex="-1" aria-labelledby="bDetailsModLbl" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
            <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="modal-title" id="bDetailsModLbl">Book Summary</h3>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" id="bDetailsModBody">
                        <div class="row">

                            <!-- book image -->
                            <div class="col-md-4 text-center">
                                <asp:Image ID="imgDetails" runat="server" Style="width: 100%; max-width: 150px; height: auto;" />
                            </div>

                            <!-- BOOK DETAILS -->
                            <div class="col-md-8">
                                <asp:Label ID="bID" runat="server" Text="bID" Visible="False"></asp:Label>
                                <!-- title -->
                                <h4 class="fw-bold">
                                    <asp:Label ID="bTitle" runat="server" Text="bTitle"></asp:Label>
                                </h4>

                                <!-- author --->
                                <h6>
                                    <span class="text-muted">By </span>
                                    <asp:Label ID="bAuthor" runat="server" Text="bAuthor"></asp:Label>
                                </h6>

                                <!-- publisher-->
                                <h6>
                                    <span class="text-muted">From </span>
                                    <asp:Label ID="bPublisher" runat="server" Text="bPublisher"></asp:Label>
                                </h6>

                                <!-- genre -->
                                <h6>
                                    <span class="text-muted">Genre </span>
                                    <asp:Label ID="bGen" runat="server" Text="bGen"></asp:Label>
                                </h6>
                                <!-- publish year -->
                                <h6>
                                    <span class="text-muted">Publish Year </span>
                                    <asp:Label ID="pubYear" runat="server" Text="PYear"></asp:Label>
                                </h6>
                                <!-- book copy -->
                                <h6>
                                    <span class="text-muted">In stock</span>
                                    <asp:Label ID="bAvailability" runat="server" Text="bAvailability"></asp:Label>
                                </h6>

                                <!-- description -->
                                <div class="row">
                                    <div class="col">
                                        <p class="fw-medium text-justify" style="line-height: 1.5;">
                                            <asp:Label ID="descDetails" runat="server" Text="descDetails"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <button type="button" runat="server" class="btn btn-warning" id="BtnAddCart" onserverclick="BtnAddCart_ServerClick"><i class="fa-solid fa-cart-plus"></i></button>
                            </div>
                            <!-- col-md-8 end-->
                        </div>
                        <!-- row end-->
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
