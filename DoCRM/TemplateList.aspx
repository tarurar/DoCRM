<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="DoCRM.FormTemplateList" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <table  width="100%">
    <tr>
        <td>
            <table id="table_page_menu" width="100%">
                <tr>
                    <td>  
                        <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новый шаблон</asp:LinkButton>                                                          
                    </td>
                    <td align="right">                         
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
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table  width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CssClass="gridview myclass" Width="100%"
                            DataKeyNames="prValue" onrowdatabound="GridView1_RowDataBound">            
                            <PagerSettings PageButtonCount="3" Mode="NumericFirstLast" />
                            <RowStyle BorderColor="#CCCCCC" BorderWidth="1px" Height="30px" />
                            <Columns>
                                <asp:BoundField DataField="prValue" HeaderText="prValue" Visible="false">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prName" HeaderText="Название">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="300px"/>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table  width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table>
<tr>
<td>
</td>
</tr>
</table>



</asp:Content>
