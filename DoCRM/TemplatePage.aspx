<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="TemplatePage.aspx.cs" Inherits="DoCRM.TemplatePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .cbstyle
        {
            padding:0px 4px 0px 4px;
            margin:0px;
            text-align: left;
        } 
    </style>

    <script type="text/javascript">
        function linkAddParam_Click() {
            var a = document.getElementById("div_add_param");
            a.style.visibility = "visible";
            a.style.height = "16px";
            var b = document.getElementById("div_new_param");
            b.style.visibility = "hidden";
            b.style.height = "0px";
        }
        function linkNewParam_Click() {
            var a = document.getElementById("div_add_param");
            a.style.visibility = "hidden";
            a.style.height = "0px";
            var b = document.getElementById("div_new_param");
            b.style.visibility = "visible";
            b.style.height = "16px";
        }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="table_main" width="100%">
        <tr>
            <td>
                <table id="table_page_menu" width="100%">
                    <tr>
                        <td>  
                            <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новый шаблон</asp:LinkButton>
                            <asp:Label ID="l_split_1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkSave" runat="server" onclick="linkSave_Click">Сохранить шаблон</asp:LinkButton>
                            <asp:Label ID="l_split_2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>                                    
                            <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click">Удалить шаблон</asp:LinkButton>                                   
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="linkCommand1" runat="server" onclick="linkCommand1_Click" Visible="false">Command1</asp:LinkButton>
                            <asp:Label ID="l_split_3" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="linkParamList_Click">Параметры</asp:LinkButton>
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
                <asp:Table ID="tab_object_header" runat="server" style="color: #808080">
                </asp:Table>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Table ID="tab_object_body" runat="server">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="table_body_menu" >
                    <tr>
                        <td class="style71" style="width: 300px; text-align:left;"> 
                            <a onclick="linkAddParam_Click()" class="linkaction">Добавить параметр из справочника</a>
                        </td>
                        <td class="style71" style="width: 300px; text-align:left;"> 
                            <a onclick="linkNewParam_Click()" class="linkaction">Создать новый параметр</a>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="table_new_rows" width="100%" >
                    <tr>
                        <td>     
                            <div id="div_add_param" style="visibility:hidden">
                                <asp:DropDownList ID="cbParamList" runat="server" class="cbstyle" style="padding:0px;" 
                                    Width="120px" Height="14px">
                                </asp:DropDownList>        
                                <asp:Button ID="btAddParam" class="btok" runat="server" Text="OK" onclick="linkAddParam_Click"/>
        
                            </div> 
                            <div id="div_new_param" style="visibility:hidden; height:14px;">

                                <table  id="table_pacient"  width="100%" style="color: #808080">
                                    <tr>
                                        <td width="100px">Название</td>
                                        <td>
                                            <asp:TextBox ID="tb_NewParamName" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100px">Заголовок</td>
                                        <td>
                                            <asp:TextBox ID="tb_NewParamCaption" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100px">
                                            <asp:DropDownList ID="cbDataTypeList" runat="server" class="cbstyle" style="padding:0px;" 
                                            Width="100px" Height="14px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btNewParam" class="btok" runat="server" Text="OK" onclick="linkNewParam_Click" />
                                        </td>
                                    </tr>                                                                                                                                       
                                </table>
                            </div>                                                                            
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