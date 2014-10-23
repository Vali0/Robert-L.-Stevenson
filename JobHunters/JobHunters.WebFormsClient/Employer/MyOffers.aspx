<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyOffers.aspx.cs" Inherits="JobHunters.WebFormsClient.Employer.MyOffers" %>

<%@ Import Namespace="JobHunters.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ListView ID="ListViewMyOffers" runat="server"
        SelectMethod="ListViewMyOffers_Select"
        UpdateMethod="Update"
        DeleteMethod="Delete"
        ItemType="JobHunters.Models.JobPost"
        InsertItemPosition="None"
        DataKeyNames="Id"
        OnSorting="ListViewMyOffers_Sorting"
        AutoGenerateEditButton="true"
        AutoGenerateColumns="false">

        <LayoutTemplate>
            <table class="table table-bordered table-hover table-responsive table-striped">
                <thead>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="Title">Title</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="Description">Description</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="City.Name">City</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="Category.Name">Category</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="OfferType">Offer Type</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="HierarchyLevel">Hierarchy Level</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="WorkEmployement">Employment</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="CreatedOn">Date Created</asp:LinkButton></th>
                        <th>
                            <asp:LinkButton runat="server" CommandName="sort" CommandArgument="Views">View Count</asp:LinkButton></th>
                    </tr>
                </thead>
                <tbody>
                    <div id="itemPlaceholder" runat="server"></div>
                </tbody>

            </table>
            <div class="pagerLine">
                <ul class="pagination">
                    <asp:DataPager ID="DataPagerCustomers" runat="server" PageSize="10">

                        <Fields>

                            <asp:NextPreviousPagerField ShowFirstPageButton="True"
                                ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            <asp:NumericPagerField />

                            <asp:NextPreviousPagerField ShowLastPageButton="True"
                                ShowNextPageButton="False" ShowPreviousPageButton="False" />

                        </Fields>


                    </asp:DataPager>
                </ul>
            </div>
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
                <td>
                    <asp:Button ID="ButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-sm btn-primary" /></td>
                <td>
                    <asp:Button ID="ButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-primary" /></td>


            </tr>
        </ItemTemplate>

        <EditItemTemplate>
            <div class="editItem form-horizontal">



                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="JobTitle" CssClass="col-md-2 control-label">Title</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="JobTitle" CssClass="form-control" Text='<%# BindItem.Title %>' />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="JobTitle"
                            CssClass="text-danger" ErrorMessage="The Job Title field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="Description" TextMode="MultiLine" Rows="5" CssClass="form-control" Text="<%# BindItem.Description %>" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Description"
                            CssClass="text-danger" ErrorMessage="The description field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-2 control-label">City</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList runat="server" ID="City" CssClass="form-control"
                            DataTextField="Name" DataValueField="Id"
                            SelectMethod="Select_Cities"
                            SelectedValue="<%# BindItem.CityId %>" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Category" CssClass="col-md-2 control-label">Category</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList runat="server" ID="Category" CssClass="form-control"
                            DataTextField="Name" DataValueField="Id"
                            SelectMethod="Select_Categories"
                            SelectedValue="<%# BindItem.CategoryId %>" AppendDataBoundItems="True" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="JobType" CssClass="col-md-2 control-label">Type</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList runat="server" ID="JobType" CssClass="form-control"
                            DataTextField="Text"
                            DataValueField="Value"
                            SelectMethod="Select_Type"
                            SelectedValue="<%# BindItem.OfferType  %>" AppendDataBoundItems="True" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="HierarchyLvl" CssClass="col-md-2 control-label">Hierarchy Level</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList runat="server" ID="HierarchyLvl" CssClass="form-control"
                            DataTextField="Text"
                            DataValueField="Value"
                            SelectMethod="Select_Hierarchy"
                            SelectedValue="<%# BindItem.HierarchyLevel  %>" AppendDataBoundItems="True" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Employment" CssClass="col-md-2 control-label">Work Employment</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList runat="server" ID="Employment" CssClass="form-control"
                            DataTextField="Text"
                            DataValueField="Value"
                            SelectMethod="Select_Employmemnt"
                            SelectedValue="<%# BindItem.WorkEmployement  %>" AppendDataBoundItems="True" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-1 pull-right">
                        <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-success"
                            CommandName="Update" Text="Update" />
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-md-1 pull-right">
                        <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-primary"
                            CommandName="Cancel" Text="Cancel" />
                    </div>
                </div>
            </div>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
