<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Turnierverwaltung2020.Views.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">        
        <div>
            <h1> login Turnierverwaltung </h1>
            <br />
            <asp:Label ID="lblBenutzername" runat="server" Text="Benutzername: "></asp:Label><asp:TextBox ID="txtBenutzername" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblPaswort" runat="server" Text="Passwort: "></asp:Label><asp:TextBox ID="txtpasswd" runat="server" TextMode="Password" TabIndex="0"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnlogin" runat="server" Text="login" OnClick="btnlogin_Click" TabIndex="1"/>
            <br />
            <br />
            <asp:Label ID="lblStatus" runat="server" Text="" Visible =" false"></asp:Label>
        </div>
    </form>
</body>
</html>
