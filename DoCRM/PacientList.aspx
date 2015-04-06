<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="PacientList.aspx.cs" Inherits="DoCRM.FormPacientList" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <table  width="100%">
    <tr>
        <td>
            <table id="table_page_menu" width="100%">
                <tr>
                    <td>  
                        <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новый клиент</asp:LinkButton>
                        <asp:Label ID="l_split_1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                        <asp:LinkButton ID="linkUse" runat="server" onclick="linkUse_Click" Visible="false">Выбрать</asp:LinkButton>
                        <asp:Label ID="l_split_2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                        <asp:LinkButton ID="linkOpen" runat="server" onclick="linkOpen_Click" Visible="false">Открыть</asp:LinkButton>
                        <asp:Label ID="l_split_3" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>                                    
                        <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click" Visible="false">Удалить</asp:LinkButton>                                   
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
                        <asp:Label runat="server">Укажите фильтр:&nbsp;</asp:Label>
                        <asp:TextBox runat="server" ID="FilterTextBox"></asp:TextBox>
                        <asp:Button runat="server" Text="Применить"/>
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
                            DataKeyNames="prPacientRef" onrowdatabound="GridView1_RowDataBound" AllowSorting="true" OnSorting="GridView1_Sorting">            
                            <PagerSettings PageButtonCount="3" Mode="NumericFirstLast" />
                            <RowStyle BorderColor="#CCCCCC" BorderWidth="1px" Height="30px" />
                            <Columns>
                                <asp:BoundField DataField="prPacientRef" HeaderText="prPacientRef" Visible="false">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prFIO" HeaderText="ФИО" SortExpression="prFIO">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="300px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prDateBirth" HeaderText="д.р." SortExpression="prDateBirth">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcellc" Width="100px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prPhoneNumber" HeaderText="Телефон" SortExpression="prPhoneNumber">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll" Width="150px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prCreateDate" HeaderText="Создан" SortExpression="prCreateDate">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcellc" Width="100px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prDateLast" HeaderText="Посл. услуга" SortExpression="prDateLast">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcellc" Width="100px"/>
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
