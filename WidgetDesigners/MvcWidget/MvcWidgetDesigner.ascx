<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>

<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto; ">
<ol>        
    <li class="sfFormCtrl">        
    <asp:Label runat="server" AssociatedControlID="LoginPlaceholder" CssClass="sfTxtLbl">Login</asp:Label>
    <asp:TextBox ID="LoginPlaceholder" runat="server" CssClass="sfTxt" />
    <asp:Label runat="server" AssociatedControlID="UsernamePlaceholder" CssClass="sfTxtLbl">Username</asp:Label>
    <asp:TextBox ID="UsernamePlaceholder" runat="server" CssClass="sfTxt" />
        <asp:Label runat="server" AssociatedControlID="PasswordPlaceholder" CssClass="sfTxtLbl">Password</asp:Label>
    <asp:TextBox ID="PasswordPlaceholder" runat="server" CssClass="sfTxt" />
    </li>
    
</ol>
</div>
