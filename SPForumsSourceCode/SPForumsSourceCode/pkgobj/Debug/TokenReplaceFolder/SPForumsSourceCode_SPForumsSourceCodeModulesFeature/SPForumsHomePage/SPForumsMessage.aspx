<%@ Register TagPrefix="WpNs0" Namespace="SPForumsSourceCode.SPForumsVWPLeftMenu" Assembly="SPForumsSourceCode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=518ac1dab453508f" %>
<%-- _lcid="1033" _version="15.0.4420" _dal="1" --%>
<%-- _LocalBinding --%>

<%@ Page Language="C#" MasterPageFile="../_catalogs/masterpage/SPForums.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" meta:webpartpageexpansion="full" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:ListItemProperty ID="ListItemProperty1" Property="BaseName" MaxLength="40" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta name="GENERATOR" content="Microsoft SharePoint" />
    <meta name="ProgId" content="SharePoint.WebPartPage.Document" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="CollaborationServer" content="SharePoint Team Web Site" />
    <SharePoint:ScriptBlock ID="ScriptBlock1" runat="server">
        var navBarHelpOverrideKey = &quot;WSSEndUser&quot;;
    </SharePoint:ScriptBlock>
    <SharePoint:StyleBlock ID="StyleBlock1" runat="server">
        body #s4-leftpanel {
	display:none;
}
.s4-ca {
	margin-left:0px;
}
    </SharePoint:StyleBlock>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderSearchArea" runat="server">
    <SharePoint:DelegateControl ID="DelegateControl1" runat="server"
        ControlId="SmallSearchInputBox" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <SharePoint:ProjectProperty ID="ProjectProperty1" Property="Description" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <style type="text/css">
        .ms-webpartPage-root
        {
            border-spacing: 0px !important;
        }
    </style>
    <table class="ms-core-tableNoSpace ms-webpartPage-root" width="100%">
        <tr>
            <td id="Td1" name="_invisibleIfEmpty" valign="top" width="10%" height="100%" style="border-right: 1px solid silver">
                <WebPartPages:WebPartZone runat="server" Title="loc:LeftColumn" ID="Main" FrameType="TitleBarOnly">
                    <ZoneTemplate></ZoneTemplate>
                </WebPartPages:WebPartZone>
            </td>
        </tr>
        <SharePoint:ScriptBlock ID="ScriptBlock2" runat="server">
            if(typeof(MSOLayout_MakeInvisibleIfEmpty) == &quot;function&quot;) 
				{MSOLayout_MakeInvisibleIfEmpty();}
        </SharePoint:ScriptBlock>
    </table>
</asp:Content>
