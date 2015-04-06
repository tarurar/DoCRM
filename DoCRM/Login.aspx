<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DoCRM.LoginForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div style="margin-left: 10px">
    
        <br />
        Имя пользователя:<br />
        <asp:TextBox ID="tbText1" runat="server" CssClass="textEntry1"></asp:TextBox>
        <br />
        <br />
        Пароль:<br />
        <asp:TextBox ID="tbText2" runat="server" CssClass="textEntry1" 
            TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btEntry" runat="server" Text="Вход" onclick="btEntry_Click" 
            Width="100px" CssClass="buttonlog" />
        <br />
        <br />
        <asp:Label ID="lRespMessage" runat="server" Text="RespMessage" 
            SkinID="OrangeLabelSkin"></asp:Label>
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
