<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemp.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Putra_Public_Library_Management_System.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <img src="images\Poster.png" class="img-fluid h-25" width="1550" />
    </section>

    <!-- Three columns of text above the carousel -->
    <div class="row pt-5">
        <div class="col-lg-4">
            <center>
                <img src="images\book-inv.png" width="90" />
                <h2>Digital Book Inventory</h2>
                <p class="text-justify">Explore our comprehensive digital book inventory, meticulously curated for your reading pleasure.</p>
            </center>
        </div> <!-- /.col-lg-4 -->
       
        <div class="col-lg-4">
            <center>
                <img src="images\book-search.png" width="90" />
                <h2>Search Books</h2>
                <p class="text-justify">Find your next great read with our powerful and intuitive book search feature.</p>
            </center>
        </div> <!-- /.col-lg-4 -->

        <div class="col-lg-4">
            <center>
                <img src="images\def-list.png" width="90" />
                <h2>Defaulter List</h2>
                <p class="text-justify">Stay informed with our up-to-date defaulter list, ensuring a smooth borrowing experience for all.</p>
            </center>
        </div> <!-- /.col-lg-4 -->
        
    </div> <!-- /.row pt-5-->

    <!-- carousel -->
    <div id="carouselAuto" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselAuto" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
            <button type="button" data-bs-target="#carouselAuto" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#carouselAuto" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="images/register.jpg" class="d-block w-100" alt="register">
                <div class="carousel-caption d-block">
                    <h4>Step 1: Sign Up </h4>
                    <p>Register an account to get started.</p>
                    <p><a class="btn btn-outline-primary" href="UserSignUp.aspx">Sign Up Today</a></p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="images/find-book.jpg" class="d-block w-100" alt="search book">
                <div class="carousel-caption d-block">
                    <h4>Step 2: Find a Book</h4>
                    <p>Explore our extensive collection of books.</p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="images/borrow-book.jpg" class="d-block w-100" alt="borrow book">
                <div class="carousel-caption d-block">
                    <h4>Step 3: Borrow a Book</h4>
                    <p>Borrow the books you love in just a few clicks.</p>
                </div>
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselAuto" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselAuto" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>


   <div class="container marketing">  

       <!-- START THE FEATURETTES -->  

       <hr class="featurette-divider">  

       <div class="row featurette">  
           <div class="col-md-7">  
               <h2 class="featurette-heading">Extensive Collection. <span class="text-muted">Discover new worlds.</span></h2>  
               <p class="lead">Our library offers a vast collection of books, journals, and digital resources to cater to all your reading and research needs.</p>  
           </div>  
           <div class="col-md-5">  
               <img src="images/collection-bg.jpg" class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto" width="500" height="500" alt="Extensive Collection">  
           </div>  
       </div>  

       <hr class="featurette-divider">  

       <div class="row featurette">  
           <div class="col-md-7 order-md-2">  
               <h2 class="featurette-heading">Community Events. <span class="text-muted">Join us.</span></h2>  
               <p class="lead">Participate in our community events, workshops, and reading sessions to connect with fellow book lovers and enhance your knowledge.</p>  
           </div>  
           <div class="col-md-5 order-md-1">  
               <img src="images/events.jpg" class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto" width="500" height="500" alt="Community Events">  
           </div>  
       </div>  

       <hr class="featurette-divider">  

       <div class="row featurette">  
           <div class="col-md-7">  
               <h2 class="featurette-heading">Online Resources. <span class="text-muted">Access anytime, anywhere.</span></h2>  
               <p class="lead">Our online resources provide you with access to e-books, audiobooks, and research databases from the comfort of your home.</p>  
           </div>  
           <div class="col-md-5">  
               <img src="images/online-resources.jpg" class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto" width="500" height="500" alt="Online Resources">  
           </div>  
       </div>  

       <hr class="featurette-divider">  

       <!-- /END THE FEATURETTES -->  

   </div>  
   <!-- /.container -->  



</asp:Content>

