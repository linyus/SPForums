<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SPForumsVWPReply.ascx.cs" Inherits="SPForumsSourceCode.SPForumsVWPReply.SPForumsVWPReply" %>
<div id="ReplyZone">
    <div id="AllReply" runat="server"></div>
</div>
<div class="replyTitleCss">Leave Message:</div>
<div>
    <asp:TextBox ID="tb_Reply" runat="server" Rows="6" TextMode="MultiLine" Width="500px"></asp:TextBox>
</div>
<div class="replyTitleCss">Inviter:</div>
<div>
    <SharePoint:ClientPeoplePicker Required="false" ValidationEnabled="true" ID="pplPickerSiteRequestor" UseLocalSuggestionCache="true" PrincipalAccountType="User" runat="server" VisibleSuggestions="3" Rows="1" AllowMultipleEntities="true" Width="485" CssClass="ms-long ms-spellcheck-true user-block" ErrorMessage="" />
</div>
<div style="padding-top: 10px;">
    <asp:Button ID="btn_Submit" runat="server" Text="Submit" OnClick="btn_Submit_Click" />
</div>
