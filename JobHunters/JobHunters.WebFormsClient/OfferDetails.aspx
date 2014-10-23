<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OfferDetails.aspx.cs" Inherits="JobHunters.WebFormsClient.OfferDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-danger" runat="server" id="errContainer" Visible="False"></div>

    <asp:FormView runat="server" ID="JobOfferDetails" CssClass="table table-striped"
        
                  ItemType="JobHunters.Models.JobPost"
                  SelectMethod="Select"
                  DataKeyNames="Id"
                  AutoGenerateRows="False">

        <ItemTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="text-info"><%#: Item.Title %></h3>
                </div>
                <div class="panel-body">
                    <div class="well">
                         <strong class="block big-text text-center">Description</strong>
                    <p class="text-center lead"><%#: Item.Description %></p>
                    </div>
                   <div class="well">
                       <strong>Posted on: </strong>
                    <%#: Item.CreatedOn %>
                   </div>
                    <div class="well">
                        <strong>Employer: </strong>
                    <%#: Item.Author.UserName %>
                    </div>
                    <div class="well">
                        <strong>City: </strong>
                    <%#: Item.City.Name %>
                    </div>
                    <div class="well">
                        <strong>Category: </strong>
                    <%#: Item.Category.Name %>
                    </div>
                    <div class="well">
                         <strong>Type: </strong>
                    <%#: Item.OfferType %>
                    </div>
                   <div class="well">
                       <strong>Level: </strong>
                    <%#: Item.HierarchyLevel %>
                   </div>
                    <div class="well">
                        <strong>Employement: </strong>
                <%#: Item.WorkEmployement %>
                    </div>
                    
                </div>
            </div>
            
        </ItemTemplate>
    </asp:FormView>
    <asp:LinkButton runat="server" ID="applyBtn" Text="Apply now!" CssClass="btn btn-lg btn-success btn-block" Visible="False"></asp:LinkButton>
</asp:Content>
