﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailedSearch.aspx.cs" Inherits="JobHunters.WebFormsClient.DetailedSearch" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-6">
            <div class="well bs-component">
                <form class="form-horizontal">
                    <fieldset>
                        <legend>Detailed search</legend>
                        <div class="form-group">
                            <label for="inputCity" class="col-lg-2 control-label">City</label>
                            <div class="col-lg-10">
                                <asp:DropDownList ID="DropDownListCities" runat="server"
                                    DataTextField="Name" DataValueField="Id" SelectMethod="Select_Cities">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <label for="inputCategory" class="col-lg-2 control-label">Category</label>
                            <div class="col-lg-10">
                                <asp:DropDownList ID="DropDownListCategories" runat="server"
                                    DataTextField="Name" DataValueField="Id" SelectMethod="Select_Categories">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <label for="Level" class="col-lg-2 control-label">Hierarchy Level</label>
                            <div class="col-lg-10">
                                <asp:CheckBoxList ID="CheckBoxListLevels" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>
                        <br />
                        <div class="form-group pull-right" style="margin-top:-250px">
                            <label for="Employement" class="col-lg-3 control-label">Employement</label>
                            <div class="col-lg-11 pull-right">
                                <asp:RadioButtonList runat="server" ID="RadioButtonListEployements"></asp:RadioButtonList>
                            </div>
                        </div>
                        <br />
                        <div class="form-group pull-right" style="margin-top:-140px; margin-left:290px;">
                            <label for="KeyWords" class="col-lg-3 control-label">Key Words?</label>
                            <div class="col-lg-11">
                                <asp:TextBox ID="TextBoxKeyWords" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="form-group ">
                            <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="btn btn-success" />
                        </div>
                    </fieldset>
                </form>
            </div>
        </div>
        <div class="col-lg-4 col-lg-offset-1">
        </div>
    </div>
</asp:Content>
