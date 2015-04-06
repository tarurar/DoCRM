<%@ Page Title="ServiceList Page" Language="C#" MasterPageFile="~/DoCRM.master" AutoEventWireup="true"
    CodeBehind="ServiceList.aspx.cs" Inherits="DoCRM.FormServiceList" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table  width="100%">
        <tr>
            <td>
                <table id="table_page_menu" width="100%">
                    <tr>
                        <td>  
                            <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новая услуга</asp:LinkButton>
                            <asp:Label ID="l_split_1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click" Visible="false">Удалить</asp:LinkButton>                                   
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="linkCommand1" runat="server" onclick="linkCommand1_Click" Visible="false">Command1</asp:LinkButton>
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkCommand2" runat="server" onclick="linkCommand2_Click" Visible="false">Command2</asp:LinkButton>
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
                                Enabled="False" CssClass="gridview" Width="100%"
                                DataKeyNames="prServiceRef,prPacientRef" onrowdatabound="GridView1_RowDataBound">            
                                <PagerSettings PageButtonCount="3" Mode="NumericFirstLast" />
                                <RowStyle BorderColor="#CCCCCC" BorderWidth="1px" Height="30px" />
                                <Columns>
                                    <asp:BoundField DataField="prServiceRef" HeaderText="prServiceRef" Visible="false">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prPacientRef" HeaderText="prPacientRef" Visible="false">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prServiceNum" HeaderText="№№">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcelll"  Width="100px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prDate" HeaderText="Дата">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcellc" Width="100px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prTime" HeaderText="Время">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcellc" Width="100px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prPacient" HeaderText="Клиент">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prUserName" HeaderText="Автор">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcelll" Width="200px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prCaption" HeaderText="Услуга">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewcelll"  Width="200px"/>
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

</asp:Content>
