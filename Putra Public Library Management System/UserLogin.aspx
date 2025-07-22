<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Putra_Public_Library_Management_System.UserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('images/library-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container mt-5 pt-4" style="min-height: calc(100vh - 125px);">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="card">

                        <div class="card-body">

                            <!-- image -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img src="images\user.png" width="125" />
                                    </center>
                                </div>
                            </div>

                            <!-- title -->
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h3>User Login </h3>
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

                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black " ID="TextBoxUserName" runat="server" placeholder="Username"></asp:TextBox>
                                        <label for="TextBoxUserName">Username </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field must not be empty" ControlToValidate="TextBoxUserName" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TextBoxPass" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                        <label for="TextBoxPass">Password </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Field must not be empty" ControlToValidate="TextBoxPass" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <!-- button -->
                                    <div class="d-grid gap-2 col-6 mx-auto">
                                        <asp:Button class="btn btn-success btn-block " ID="ButtonLgIn" runat="server" Text="Login" OnClick="ButtonLgIn_Click" />
                                        <a class="btn btn-primary" id="Button2" href="UserSignUp.aspx" role="button">Sign Up</a>
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
            </div>
        </div>

        <!-- Message Modal -->
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

    </div>

</asp:Content>
