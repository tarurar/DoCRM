<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="ParamList.aspx.cs" Inherits="DoCRM.FormParamList" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <table  width="100%">
    <tr>
        <td>
            <table id="table_page_menu" width="100%">
                <tr>
                    <td>  
                        <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новый параметр</asp:LinkButton>                        
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
                            DataKeyNames="prParam1" onrowdatabound="GridView1_RowDataBound">            
                            <PagerSettings PageButtonCount="3" Mode="NumericFirstLast" />
                            <RowStyle BorderColor="#CCCCCC" BorderWidth="1px" Height="30px" />
                            <Columns>
                                <asp:BoundField DataField="prParam1" HeaderText="prRef" Visible="false">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prParam2" HeaderText="prDataType" Visible="false">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="100px"/>
                                </asp:BoundField>

                                <asp:BoundField DataField="prParam4" HeaderText="Название">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="200px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prParam5" HeaderText="Заголовок">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="200px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prParam3" HeaderText="Тип данных">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="60px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prParam6" HeaderText="Создал">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="60px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="prParam7" HeaderText="Дата создания">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewcelll"  Width="60px"/>
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
            <div>
                <asp:Panel ID="panPager" runat="server">
                    <table>
                        <tr style="color: #808080">
                            <td style="width: 100px">                    
                                <asp:LinkButton ID="linkPrew" runat="server" SkinId="LinkButtonGray" 
                                    onclick="linkPrew_Click">Предыдущая</asp:LinkButton>
                            </td>
                            <td style="width: 100px">
                                Страница&nbsp;<asp:Label ID="lPageNumber" runat="server" SkinID="GrayLabelSkin" Text="1"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="linkNext" runat="server" SkinId="LinkButtonGray" 
                                    onclick="linkNext_Click">Следующая</asp:LinkButton>
                            </td>
                            <td style="width: 200px">
                                Строк на странице
                                &nbsp;<asp:LinkButton ID="linkRPP5" runat="server" SkinId="LinkButtonGray" 
                                    onclick="linkRPP5_Click">5</asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="linkRPP10" runat="server" SkinId="LinkButtonGray" 
                                    onclick="linkRPP10_Click">10</asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="linkRPP20" runat="server" SkinId="LinkButtonGray" 
                                    onclick="linkRPP20_Click">20</asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="linkRPP50" runat="server" SkinId="LinkButtonGray" 
                                    onclick="linkRPP50_Click">50</asp:LinkButton>
                            </td>
                            <td style="width: 100px">
                                Страниц&nbsp;<asp:Label ID="lPageCount" runat="server" SkinID="GrayLabelSkin" Text="1"></asp:Label>
                            </td>
                            <td class="style7">
                                Записей&nbsp;<asp:Label ID="lRowCount" runat="server" SkinID="GrayLabelSkin" Text="1"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
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
