<%@ Page Title="" Language="C#" MasterPageFile="~/DoCRM.Master" AutoEventWireup="true" CodeBehind="PacientPage.aspx.cs" Inherits="DoCRM.PacientPageForm" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table  width="100%">
        <tr>
            <td>
                <table id="table_page_menu" width="100%">
                    <tr>
                        <td>  
                            <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">Новый клиент</asp:LinkButton>
                            <asp:Label ID="l_split_1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkSave" runat="server" onclick="linkSave_Click">Сохранить</asp:LinkButton>
                            <asp:Label ID="l_split_2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>                                    
                            <asp:LinkButton ID="linkDelete" runat="server" onclick="linkDelete_Click">Удалить</asp:LinkButton>                                   
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="linkUse" runat="server" onclick="linkPacientServices_Click">Услуги</asp:LinkButton>
                            <asp:Label ID="l_split_3" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:LinkButton ID="linkNewService" runat="server" onclick="linkNewPacientService_Click">Новая услуга</asp:LinkButton>
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
                <table  id="table_pacient"  width="100%" style="color: #808080">
                    <tr>
                        <td width="100px">Фамилия</td>
                        <td>
                            <asp:TextBox ID="tb_SurName" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">Телефон</td>
                        <td>
                            <asp:TextBox ID="tb_PhoneNumber" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Имя</td>
                        <td>
                            <asp:TextBox ID="tb_FirstName" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">ЭлПочта</td>
                        <td>
                            <asp:TextBox ID="tb_EMail" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Отчество</td>
                        <td>
                            <asp:TextBox ID="tb_FatherName" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">ДатаСоздания</td>
                        <td>
                            <asp:TextBox ID="tb_CreateDate" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">ДатаРождения</td>
                        <td>
                            <asp:TextBox ID="tb_DateBirth" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">Создал</td>
                        <td>
                            <asp:TextBox ID="tb_CreateUser" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Пол</td>
                        <td>
                            <asp:TextBox ID="tb_Sex" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">Источник</td>
                        <td>
                            <asp:TextBox ID="tb_Source" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Паспорт</td>
                        <td>
                            <asp:TextBox ID="tb_Passport" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">Посл. услуга</td>
                        <td>
                            <asp:TextBox ID="tb_LastDate" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">КодКарты</td>
                        <td>
                            <asp:TextBox ID="tb_CardCode" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table style="color: #808080">
                    <tr>
                        <td width="100px">Адрес регистр.</td>
                        <td>
                            <asp:TextBox ID="tb_AddressReg" runat="server" Width="500px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Адрес прож.</td>
                        <td>
                            <asp:TextBox ID="tb_AddressHome" runat="server" Width="500px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Комментарий</td>
                        <td>
                            <asp:TextBox ID="tb_Comment" runat="server" Width="500px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="color: #808080">
                    <tr>
                        <td width="100px">ГруппаКрови</td>
                        <td>
                            <asp:TextBox ID="tb_BloodGroup" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">РезусФакторКрови</td>
                        <td>
                            <asp:TextBox ID="tb_BloodRezus" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">ххх</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="300px" CssClass="textEntry1"></asp:TextBox>
                        </td>
                        <td width="100px">ДР тест</td>
                        <td>
                            <input id="tb_DateBirth_1" type="text" />
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
                            <ajaxToolkit:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
                            <ajaxToolkit:MaskedEditExtender ID="me_tb_DateBirth" runat="server"
                                TargetControlID="tb_DateBirth"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                InputDirection="RightToLeft"
                                AcceptNegative="Left"
                                DisplayMoney="None"
                                ErrorTooltipEnabled="True" />
                            <ajaxToolkit:MaskedEditExtender ID="me_tb_PhoneNumber" runat="server"
                                TargetControlID="tb_PhoneNumber"
                                Mask="(999)999-99-99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="None"
                                InputDirection="RightToLeft"
                                DisplayMoney="None"
                                ClipboardEnabled="true"
                                AutoComplete="true"
                                ClearMaskOnLostFocus="false"
                                ErrorTooltipEnabled="True" />
                        </td>                                                                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
