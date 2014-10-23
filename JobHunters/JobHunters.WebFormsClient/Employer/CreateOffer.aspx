<%@ Page Title="Create Offer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateOffer.aspx.cs" Inherits="JobHunters.WebFormsClient.Employer.CreateOffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Create a new offer</h1>
    <div class="form-horizontal">

        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="JobTitle" CssClass="col-md-2 control-label">Title</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="JobTitle" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="JobTitle"
                    CssClass="text-danger" ErrorMessage="The Job Title field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Description" TextMode="MultiLine" Rows="5" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Description"
                    CssClass="text-danger" ErrorMessage="The description field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="City" CssClass="form-control"
                    DataTextField="Name" DataValueField="Id"
                    SelectMethod="Select_Cities" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Category" CssClass="col-md-2 control-label">Category</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="Category" CssClass="form-control"
                    DataTextField="Name" DataValueField="Id"
                    SelectMethod="Select_Categories" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="JobType" CssClass="col-md-2 control-label">Type</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="JobType" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="HierarchyLvl" CssClass="col-md-2 control-label">Hierarchy Level</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="HierarchyLvl" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Employment" CssClass="col-md-2 control-label">Work Employment</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="Employment" CssClass="form-control"
                    DataTextField="Name" DataValueField="Id" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateOffer_Click" Text="Publish" CssClass="btn btn-default" />
            </div>
        </div>
             <asp:FileUpload ID="UploadImage" runat="server" />
    </div>
</asp:Content>
