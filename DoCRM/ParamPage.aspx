<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="ParamPage.aspx.cs" Inherits="DoCRM.ParamPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="table_main" width="100%">
        <tr>
            <td>
                <table id="table_page_menu" width="100%">
                    <tr>
                        <td>  
                            <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новый параметр</asp:LinkButton>
                            <asp:Label ID="l_split_1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkSave" runat="server" onclick="linkSave_Click">Сохранить параметр</asp:LinkButton>
                            <asp:Label ID="l_split_2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>                                    
                            <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click">Удалить параметр</asp:LinkButton>                                   
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="linkCommand1" runat="server" onclick="linkParamList_Click">Параметры</asp:LinkButton>
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

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>            
            <td>
                <table  id="tab_object_content_1"  width="100%" style="color: #808080">
                    <tr>
                        <td width="100px">Название</td>
                        <td>
                            <asp:TextBox ID="tb_ParamName" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td width="100px">Заголовок</td>
                        <td>
                            <asp:TextBox ID="tb_ParamCaption" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td> 
                    </tr>
                </table>
                <table  id="tab_object_content_2"  width="100%" style="color: #808080">
                    <tr>
                        <td width="100px">Тип данных</td>
                        <td>
                            <asp:DropDownList ID="cbDataTypeList" runat="server" CssClass="cbstyle" style="padding:0px;" 
                                Width="100px" Height="14px">
                            </asp:DropDownList>
                        </td> 
                    </tr>
                    <tr>
                        <td width="100px">Создал</td>
                        <td>
                            <asp:TextBox ID="tb_ParamCreateUser" runat="server" Width="100px" CssClass="textEntry1"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td width="100px">Дата создания</td>
                        <td>
                            <asp:TextBox ID="tb_ParamCreateDate" runat="server" Width="100px" CssClass="textEntry1"></asp:TextBox>
                        </td> 
                    </tr>
                </table>
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