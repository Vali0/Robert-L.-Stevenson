<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplyForJobOffer.aspx.cs" Inherits="JobHunters.WebFormsClient.ApplyForJobOffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label CssClass="alert alert-dismissable alert-danger " runat="server" ID="errContainer" visible="False" style="display: block">
       <button type="button" class="close" data-dismiss="alert" style="color: #333">x</button>
    </asp:Label>
     
    <div runat="server" id="itemFound">
        <h1 class="text-center">Apply for Offer -
            <span class="text-info">
                <asp:Literal runat="server" ID="OfferTitle"></asp:Literal>

            </span>

        </h1>

        <div class="form-horizontal" runat="server">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Comment" CssClass="col-md-2 control-label">Comment(optional):</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Comment" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Cv" CssClass="col-md-2 control-label">Upload your CV:</asp:Label>
                <div class="col-md-10">
                    <div class="btn btn-info btn-file">
                         Choose File <asp:FileUpload runat="server" ID="Cv" CssClass="form-control" />
                    </div>
                   
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Cv"
                    CssClass="text-danger" ErrorMessage="The Cv field is required."></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="Apply_Click" Text="Apply For This Job" CssClass="btn btn-default" />
            </div>
        </div>
        </div>
    </div>
    <script>
        $(document).on('change', '.btn-file :file', function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            input.trigger('fileselect', [numFiles, label]);
        });

        $(document).ready(function () {
            $('.btn-file :file').on('fileselect', function (event, numFiles, label) {
                $('#file-label').remove();
                $(this).parent().after($('<span class="label label-primary"/>').prop('id','file-label').css({ 'margin-left': '10px', 'padding': '4px' }).html(label));
            });
        });
    </script>
</asp:Content>

