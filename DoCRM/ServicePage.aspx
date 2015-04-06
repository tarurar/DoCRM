<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="ServicePage.aspx.cs" Inherits="DoCRM.ServicePage" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style71
        {
            padding:0px 4px 0px 4px;
            margin:0px;
            text-align: left;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table  width="100%">
        <tr>
            <td>
                <table id="table_page_menu" width="100%">
                    <tr>
                        <td>  
                            <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новая услуга</asp:LinkButton>
                            <asp:Label ID="l_split_1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkSave" runat="server" onclick="linkSave_Click">Сохранить</asp:LinkButton>
                            <asp:Label ID="l_split_2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>                                    
                            <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click">Удалить</asp:LinkButton>                                   
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="linkNewService" runat="server" onclick="linkNewPacientService_Click" Visible="false">Новая услуга</asp:LinkButton>
                            <asp:Label ID="l_split_3" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>                            
                            <asp:LinkButton ID="linkUse" runat="server" onclick="linkPacientServices_Click">Услуги</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table  id="table_filters"  width="100%">
                    <tr>
                        <td class="style71">
                            <asp:DropDownList ID="cbTemplates" runat="server" SkinId="GrayDropDownList" style="border: none; padding:0px;" 
                                Width="120px" Height="14px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Table ID="tab_service_header" runat="server" style="color: #808080">
                </asp:Table>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Table ID="tab_service_body" runat="server">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <table  id="table_bottom"  width="100%">
                    <tr>
                        <td>
                            <asp:HiddenField ID="hidformmode" runat="server" />
                        </td>                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
