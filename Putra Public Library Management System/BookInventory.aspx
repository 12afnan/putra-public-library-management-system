<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="BookInventory.aspx.cs" Inherits="Putra_Public_Library_Management_System.BookInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //datatable table styling
        $(document).ready(function ()
        {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });

        //read image file
        function readUrl(input)
        {
            if (input.files && input.files[0])
            {
                var reader = new FileReader();
                reader.onload = function (e)
                {
                    $('#imgPreview').attr('src', e.target.result);                    
                };
                reader.readAsDataURL(input.files[0]);
            }
        }   
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/management-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container-fluid pt-5 pb-5">
            <div class="row">
                <!-- left card -->
                <div class="col-md-5">
                    <div class="card">

                        <div class="card-body">

                            <!-- Title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>Book Details </h4>
                                    </center>
                                </div>
                            </div>

                            <!-- Image -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img id="imgPreview" src="BookInventory_imgs\book-stack.png" width="100" />
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
                                <div class="col">
                                    <asp:FileUpload class="form-control border border-black" ID="FileUploadImg" runat="server" onchange="readUrl(this);" />
                                </div>
                            </div>

                            <br />

                            <!-- 2ND ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="input-group">
                                        <div class="form-floating">
                                            <asp:TextBox class="form-control border border-black" ID="TextBoxBID" runat="server" placeholder="Book ID"></asp:TextBox>
                                            <label for="TextBoxBID">Book ID </label>
                                        </div>
                                        <asp:Button class="btn btn-info" ID="ButtonGo" runat="server" Text="Go" OnClick="ButtonGo_Click" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxBID" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-8">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxBName" runat="server" placeholder="Book Name"></asp:TextBox>
                                        <label for="TextBoxBName">Book Name </label>
                                    </div>
                                </div>

                            </div>


                            <!-- 3RD & 4TH ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListLang" runat="server">
                                            <asp:ListItem>English</asp:ListItem>
                                            <asp:ListItem>Malay</asp:ListItem>
                                            <asp:ListItem>Tamil</asp:ListItem>
                                            <asp:ListItem>Mandarin</asp:ListItem>
                                            <asp:ListItem>Arabic</asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="DropDownListLang">Language </label>
                                    </div>

                                    <br />

                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListPubName" runat="server" DataTextField="Publisher_Name" DataValueField="Publisher_Name" DataSourceID="SqlDataSource3">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringPubN %>" ProviderName="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringPubN.ProviderName %>" SelectCommand="SELECT [Publisher_Name] FROM [tbl_PublisherMaster]"></asp:SqlDataSource>
                                        <label for="DropDownListPubName">Publisher Name </label>
                                    </div>
                                </div>

                                <br />

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListAuthName" runat="server" DataTextField="Author_Name" DataValueField="Author_Name" DataSourceID="SqlDataSource4"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringAuthN %>" ProviderName="<%$ ConnectionStrings:PPLIBRARYDBConnectionStringAuthN.ProviderName %>" SelectCommand="SELECT [Author_Name] FROM [tbl_AuthorMaster]"></asp:SqlDataSource>
                                        <label for="DropDownListAuthName">Author Name </label>
                                    </div>

                                    <br />

                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxPubDate" runat="server" placeholder="Publish Date" TextMode="Date"></asp:TextBox>
                                        <label for="TextBoxPubDate">Publish Date </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:ListBox class="form-control border border-black" ID="ListBoxGen" runat="server" SelectionMode="Multiple" DataSourceID="SqlDataSource2" DataTextField="GenreName" DataValueField="GenreName" Height="150px"></asp:ListBox>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionString5 %>" ProviderName="<%$ ConnectionStrings:PPLIBRARYDBConnectionString5.ProviderName %>" SelectCommand="SELECT [GenreName] FROM [tbl_GenreMaster]"></asp:SqlDataSource>
                                        <label for="ListBoxGen">Genre </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- 5TH ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxEdi" runat="server" placeholder="Edition" TextMode="SingleLine"></asp:TextBox>
                                        <label for="TextBoxEdi">Edition </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxCost" runat="server" placeholder="RM" TextMode="Number"></asp:TextBox>
                                        <label for="TextBoxCost">Book Cost per Unit </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxPage" runat="server" placeholder="Pages" TextMode="Number"></asp:TextBox>
                                        <label for="TextBoxPage">Pages </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- 6TH ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxActCopy" runat="server" placeholder="Actual Copy" TextMode="Number"></asp:TextBox>
                                        <label for="TextBoxActCopy">Actual Copy </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxAvaCopy" runat="server" placeholder="Available Copy" TextMode="Number" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxAvaCopy">Available Copy </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxIssCopy" runat="server" placeholder="Issued Copy" TextMode="Number" ReadOnly="True"></asp:TextBox>
                                        <label for="TextBoxIssCopy">Issued Copy </label>
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- 7TH ROW-->
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxBDesc" runat="server" placeholder="Book Description" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        <label for="TextBoxBDesc">Book Description </label>
                                    </div>
                                </div>

                            </div>

                            <br />
                            <!-- Button -->
                            <div class="row">
                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-primary w-100" ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" />
                                </div>

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-warning w-100" ID="ButtonUpd" runat="server" Text="Update" OnClick="ButtonUpd_Click" />
                                </div>

                                <div class="col-12 col-md-4 mb-3">
                                    <asp:Button class="btn btn-danger w-100" ID="ButtonDel" runat="server" Text="Delete" OnClick="ButtonDel_Click" />
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
                                        <h4>Book List </h4>
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
                                        <asp:GridView class="table table-striped table-bordered table-responsive" ID="GridViewBook" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="BookID" OnSelectedIndexChanged="GridViewBook_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                                <asp:BoundField DataField="BookID" HeaderText="BookID" SortExpression="BookID" ReadOnly="True" ItemStyle-Font-Bold="True"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="container-fluid">

                                                            <div class="row">

                                                                <!-- 2nd column-->
                                                                <div class="col-lg-10">

                                                                    <!-- 1st row -->
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <asp:Label ID="LabelBName" runat="server" Text='<%# Eval("BookName")%>' Font-Bold="True" Font-Size="Large"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <!-- 2nd row -->
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <span>Author:&nbsp;</span>
                                                                            <asp:Label ID="LabelAuthName" runat="server" Text='<%# Eval("Author_Name")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                            &nbsp; | 
                                                                    <span>
                                                                        <span>&nbsp;</span>
                                                                        Genre: 
                                                                    </span>
                                                                            <asp:Label ID="LabelGen" runat="server" Text='<%# Eval("BookGenre") %>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                            &nbsp; |
                                                                    <span>Language: 
                                                                        <span>&nbsp;</span>
                                                                        <asp:Label ID="LabelLang" runat="server" Text='<%# Eval("Language")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                    </span>
                                                                        </div>
                                                                    </div>
                                                                    <!-- 3rd row -->
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <span>Publisher: </span>
                                                                            <asp:Label ID="LabelPub" runat="server" Text='<%# Eval("Publisher_Name")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                            &nbsp; | 
                                                                    <span>Publish Date: 
                                                                    </span>
                                                                            <asp:Label ID="LabelPDate" runat="server" Text='<%# Eval("PublishYear","{0:d MMM yyyy}") %>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                            &nbsp; |
                                                                    <span>Pages: 
                                                                        <asp:Label ID="LabelPage" runat="server" Text='<%# Eval("PageNumber")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                    </span>
                                                                            &nbsp; |
                                                                    <span>Edition: 
                                                                        <asp:Label ID="LabelEdi" runat="server" Text='<%# Eval("Edition")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                    </span>
                                                                        </div>
                                                                    </div>

                                                                    <!-- 4th row -->
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <span>Cost: </span>
                                                                            <asp:Label ID="LabelCost" runat="server" Text='<%# Eval("BookCost")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                            &nbsp; | 
                                                                    <span>
                                                                        <span>&nbsp;</span>
                                                                        Actual Copy: 
                                                                    </span>
                                                                            <asp:Label ID="LabelActCopy" runat="server" Text='<%# Eval("CopyNumber") %>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                            &nbsp; |
                                                                    <span>Available Copy: 
                                                                        <span>&nbsp;</span>
                                                                        <asp:Label ID="LabelAvaCopy" runat="server" Text='<%# Eval("CopyAvailable")%>' Font-Bold="True" Font-Size="Smaller"></asp:Label>
                                                                    </span>
                                                                        </div>
                                                                    </div>

                                                                    <!-- 5th row -->
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <span>Description: </span>
                                                                            <asp:Label ID="LabelDesc" runat="server" Text='<%# Eval("BookDescription")%>' Font-Bold="True" Font-Size="Smaller" Font-Italic="true"></asp:Label>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <!-- 3rd column-->
                                                                <div class="col-lg-2">
                                                                    <asp:Image class="img-fluid p-2" ID="ImageB" runat="server" ImageUrl='<%# "data:Image/jpg;base64," + Convert.ToBase64String((byte [])Eval("BookImage")) %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PPLIBRARYDBConnectionString4 %>" SelectCommand="SELECT * FROM [tbl_BookMaster]"></asp:SqlDataSource>

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
