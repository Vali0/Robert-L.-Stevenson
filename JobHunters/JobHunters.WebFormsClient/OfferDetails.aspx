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
                    <strong>Description: </strong>
                    <%#: Item.Description %>
                    <br />
                    <strong>Posted on: </strong>
                    <%#: Item.CreatedOn %>
                    <br />
                    <strong>Employer: </strong>
                    <%#: Item.Author.UserName %>
                    <br />
                    <strong>City: </strong>
                    <%#: Item.City.Name %>
                    <br />
                    <strong>Category: </strong>
                    <%#: Item.Category.Name %>
                    <br />
                    <strong>Type: </strong>
                    <%#: Item.OfferType %>
                    <br />
                    <strong>Level: </strong>
                    <%#: Item.HierarchyLevel %>
                    <br />
                    <strong>Employement: </strong>
                <%#: Item.WorkEmployement %>
                </div>
            </div>
            <asp:Button runat="server" ID="applyBtn" Text="Apply now!" CssClass="btn btn-lg btn-success"/>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
