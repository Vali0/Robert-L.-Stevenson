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
                    Under construction..
                </div>
            </div>
          <asp:Button runat="server" ID="applyBtn" Text="Apply now!" CssClass="btn btn-lg btn-success"/>
        </ItemTemplate>
       
    </asp:FormView>

   
    
</asp:Content>
