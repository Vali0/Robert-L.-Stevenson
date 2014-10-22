<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JobHunters.WebFormsClient._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Welcome to Job Hunters!</h1>
        <p class="lead">Here you can find the perfect job oportunity.Try it out today!</p>
    </div>
    <div class="row">
        <div class="col-md-4 text-center">
            <strong class="block">Users</strong>
            <asp:Label runat="server" CssClass="block" ID="statUsers">
            </asp:Label>
        </div>
        <div class="col-md-4 text-center">
            <strong class="block">Job Offers</strong>
            <asp:Label runat="server" CssClass="block" ID="statOffers">
            </asp:Label>
        </div>
        <div class="col-md-4 text-center">
            <strong class="block">Employers</strong>
            <asp:Label runat="server" CssClass="block" ID="statEmployers">
            </asp:Label>
        </div>
    </div>
    <div class="row text-center">
        <h2>Latest Job Offers:</h2>
    </div>

</asp:Content>
