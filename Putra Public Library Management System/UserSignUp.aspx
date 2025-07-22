<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="UserSignUp.aspx.cs" Inherits="Putra_Public_Library_Management_System.UserSignUp" EnableEventValidation="False" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('images/sign-up-bg.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat;">
        <div class="container pt-5 pb-5">
            <div class="row">
                <div class="col-md-8 mx-auto">
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
                                        <h4>User Sign Up </h4>
                                    </center>
                                </div>
                            </div>

                            <!-- Line -->
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>

                            <!-- 1ST ROW-->
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-floating ">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxFName" runat="server" placeholder="First Name"></asp:TextBox>
                                        <label for="TxtBxFName">First Name</label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your first name" ControlToValidate="TxtBxFName" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxLName" runat="server" placeholder="Last Name"></asp:TextBox>
                                        <label for="TxtBxLName">Last Name</label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter your last name" ControlToValidate="TxtBxLName" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                            </div>

                            <!-- 2ND ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxNRIC" runat="server" placeholder="IC Number" TextMode="SingleLine"></asp:TextBox>
                                        <label for="TxtBxNRIC">NRIC Number  </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter your NRIC (without '-')" ControlToValidate="TxtBxNRIC" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxPhoneNo" runat="server" placeholder="Phone Num" TextMode="Phone"></asp:TextBox>
                                        <label for="TxtBxPhoneNo">Phone Num </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter your phone number" ControlToValidate="TxtBxPhoneNo" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxEmail" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
                                        <label for="TxtBxEmail">Email </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter your email" ControlToValidate="TxtBxEmail" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <!-- 3RD ROW-->
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxDOB" runat="server" placeholder="DOB" TextMode="Date"></asp:TextBox>
                                        <label for="TxtBxDOB">Date Of Birth </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter your date of birth" ControlToValidate="TxtBxDOB" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxGen" runat="server" placeholder="Gender"></asp:TextBox>
                                        <label for="TxtBxGen">Gender </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter your gender" ControlToValidate="TxtBxGen" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListRace" runat="server">
                                            <asp:ListItem Value="Malay"></asp:ListItem>
                                            <asp:ListItem Value="Chinese"></asp:ListItem>
                                            <asp:ListItem Value="Indian"></asp:ListItem>
                                            <asp:ListItem Value="Others"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="DropDownListRace">Race </label>
                                    </div>
                                </div>

                            </div>

                            <!-- 4TH ROW-->
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListState" runat="server">
                                            <asp:ListItem Text="Select State" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="DropDownListState">State</label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Select state" ControlToValidate="DropDownListState" InitialValue="0" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListCity" runat="server">
                                            <asp:ListItem Text="Select City" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="DropDownListCity">City </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select city" ControlToValidate="DropDownListCity" InitialValue="0" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-floating">
                                        <asp:DropDownList class="form-control border border-black" ID="DropDownListPCode" runat="server">
                                            <asp:ListItem Text="Select Postcode" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="DropDownListPCode">Postcode </label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Select postcode" ControlToValidate="DropDownListPCode" InitialValue="0" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxUsrName" runat="server" placeholder="Username"></asp:TextBox>
                                        <label for="TxtBxUsrName">Username</label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Required" ControlToValidate="TxtBxUsrName" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox class="form-control border border-black" ID="TxtBxPwd" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                        <label for="TxtBxPwd">Password</label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required" ControlToValidate="TxtBxPwd" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <br />

                            <!-- Button -->
                            <div class="row">
                                <div class="col">
                                    <div class="d-grid gap-2 col-6 mx-auto">
                                        <asp:Button runat="server" ID="ButtonSgnUp" class="btn btn-success" Text="Sign Up" OnClick="ButtonSgnUp_Click" />
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

    <script>
        class IC {
            static POBCodes = {
                // (POBCodes data omitted for brevity)
            };
            constructor(icNumber) {
                this.icNumber = icNumber;
                this.length = this.getLength();
                this.dob = this.getDOB();
                this.gender = this.getGender();
            }
            getLength() {
                return this.icNumber.length;
            }
            getDOB() {
                var month = parseInt(this.icNumber.substring(2, 4));
                var day = parseInt(this.icNumber.substring(4, 6));
                var century = (parseInt(this.icNumber.substring(8, 9)) >= 5) ? 19 : 20;
                var year = century * 100 + parseInt(this.icNumber.substring(0, 2));
                var dateStr = year.toString() + '-' + month.toString().padStart(2, '0') + '-' + day.toString().padStart(2, '0');
                if (!moment(dateStr).isValid()) return false;
                return dateStr;
            }
            getGender() {
                var gendercode = parseInt(this.icNumber.slice(-1));
                return (gendercode % 2 == 0) ? 'Female' : 'Male';
            }
        }

        $(document).ready(function () {
            $('#<%= TxtBxNRIC.ClientID %>').keyup(function () {
                var icStr = $(this).val().replace(/\D/g, '');
                $(this).val(icStr);
                var ic = new IC(icStr);
                if (ic.length == 12) {
                    $('#<%= TxtBxDOB.ClientID %>').val(ic.dob);
                    $('#<%= TxtBxGen.ClientID %>').val(ic.gender);
                }
            });
        });
    </script>

    <script>
        const stateSelect = document.getElementById("<%= DropDownListState.ClientID %>");
        const citySelect = document.getElementById("<%= DropDownListCity.ClientID %>");
        const postcodeSelect = document.getElementById("<%= DropDownListPCode.ClientID %>");

        // Load states
        malaysiaPostcodes.getStates().forEach((state) => {
            const option = new Option(state, state);
            stateSelect.appendChild(option);
        });

        // Update cities when a state is selected
        stateSelect.addEventListener("change", function () {
            citySelect.innerHTML = "";
            /*postcodeSelect.innerHTML = ""*/;

            const placeHolderOptionCt = new Option("Select City", "", true, true);
            const placeHolderOptionPc = new Option("Select Postcode", "", true, true);
            placeHolderOptionCt.disabled = true;
            placeHolderOptionPc.disabled = true;

            citySelect.appendChild(placeHolderOptionCt);
            postcodeSelect.appendChild(placeHolderOptionPc);

            const state = stateSelect.value;

            malaysiaPostcodes.getCities(state).forEach((city) => {
                const option = new Option(city, city);
                citySelect.appendChild(option);
            });
        });

        // Update postcodes when a city is selected
        citySelect.addEventListener("change", function () {
            postcodeSelect.innerHTML = "";

            const placeHolderOption = new Option("Select Postcode", "", true, true);
            placeHolderOption.disabled = true;
            postcodeSelect.appendChild(placeHolderOption);

            const city = citySelect.value;
            malaysiaPostcodes.getPostcodes(stateSelect.value, city).forEach((postcode) => {
                const option = new Option(postcode, postcode);
                postcodeSelect.appendChild(option);
            });
        });
    </script>

</asp:Content>
