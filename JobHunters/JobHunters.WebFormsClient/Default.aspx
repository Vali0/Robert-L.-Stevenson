<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JobHunters.WebFormsClient._Default" %>
<%@ Import Namespace="JobHunters.Models" %>

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
        <div class="panel panel-primary">
            <div class="panel-heading">
                     <h2>Latest Job Offers:</h2>
            </div>
            <div class="panel-body">
                         <asp:ListView ID="ListViewMyOffers" runat="server"
        SelectMethod="ListViewMyOffers_Select"
        ItemType="JobHunters.Models.JobPost"
        InsertItemPosition="None"
        DataKeyNames="Id"
        >

        <LayoutTemplate>
            <table class="table table-bordered table-hover table-responsive table-striped">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Description</th>
                        <th>City</th>
                        <th>Category</th>
                        <th>Offer Type</th>
                        <th>Hierarchy Level</th>
                        <th>Employment</th>
                        <th>Date Created</th>
                        <th>View Count</th>
                    </tr>
                </thead>
                <tbody>
                     <div  id="itemPlaceholder" runat="server"></div>
                </tbody>
               
            </table>
        </LayoutTemplate>

        <EmptyDataTemplate>
            <div class="alert alert-warning">No Items To Show.</div>
        </EmptyDataTemplate>

        <ItemTemplate>
            <tr class="item">
                <td class="text-primary"><a href="/OfferDetails.aspx/<%#: Item.Id %>"><%#: Item.Title %></a></td>
                <td><%#: Item.Description.Substring(0,Item.Description.Length>10?10:Item.Description.Length)+"..." %></td>

                <td class="text-info"><%#: Item.City.Name %></td> 
                <td><%#: Item.Category.Name %></td>
                <td><%#: Enum.GetName(typeof(OfferType),Item.OfferType) %></td>
                <td><%#: Enum.GetName(typeof(HierarchyLevel),Item.HierarchyLevel) %></td>
                <td><%#: Enum.GetName(typeof(WorkEmployment),Item.WorkEmployement) %></td>
                <td><%#: Item.CreatedOn %></td>
                <td><%#: Item.Views %></td>
               
              
            </tr>
        </ItemTemplate>
    </asp:ListView>
            </div>
        </div>
   
        

    </div>

</asp:Content>
