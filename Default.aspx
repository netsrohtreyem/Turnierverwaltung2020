<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Turnierverwaltung2020._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Turnierverwaltung</h1>
    <asp:Table runat="server" Width="100%" >
        <asp:TableRow>
            <asp:TableCell Width="33%">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Views/Verwaltung"><h3>Verwaltung</h3></asp:HyperLink>
            </asp:TableCell>
            <asp:TableCell Width="33%" HorizontalAlign="Center">
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Views/Spieleverwaltung"><h3>Turnier durchführen</h3></asp:HyperLink>
            </asp:TableCell>
            <asp:TableCell Width="33%" HorizontalAlign="Center">
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Views/RankingAnzeige"><h3>Tabellen</h3></asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
 <h3>Verfügare Sportarten:</h3>
    <asp:DropDownList ID="drpdwList1" runat="server" Font-Bold="True" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="drpdwList1_SelectedIndexChanged"></asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnloeschen" runat="server" Text="markierte löschen" OnClick="btnloeschen_Click" Font-Bold="True" Font-Size="Medium" />
 <h3>Sportart hinzufügen:</h3>
    <asp:CheckBox ID="CheckBox1" runat="server" Text="Mannschafts - Sport" Checked="true" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
    &nbsp;&nbsp;&nbsp;
    <asp:CheckBox ID="CheckBox2" runat="server" Text="Einzel - Sport" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"/>
    <br />
    <br />
    <asp:Label ID="lblsieg" runat="server" Font-Bold="True" Font-Size="Medium">Plus - Punkte bei Sieg:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <asp:TextBox ID="txtsieg" runat="server" Font-Bold="True" Font-Size="Medium" type="number" min="0" Height="25px" Width="50px" ></asp:TextBox>
    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txtsieg" FilterType="Numbers" />
    <br />
    <br />
    <asp:Label ID="lbllost" runat="server" Font-Bold="True" Font-Size="Medium">Minus - Punkte bei Niederlage:</asp:Label> &nbsp; <asp:TextBox ID="txtlost" runat="server" Font-Bold="True" Font-Size="Medium" type="number" min="0" Height="25px" Width="50px"></asp:TextBox>
    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txtlost" FilterType="Numbers" />
    <br />
    <br />
    <asp:Label ID="lblunentschieden" runat="server" Font-Bold="True" Font-Size="Medium">Punkte bei Unentschieden:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:TextBox ID="txtunentschieden" runat="server" Font-Bold="True" Font-Size="Medium" type="number" min="0" Height="25px" Width="50px"></asp:TextBox>
    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txtunentschieden" FilterType="Numbers" />
    <br />
 <br />
 <asp:Label ID="lblbezeichnung" runat="server" Font-Bold="True" Font-Size="Medium">Bezeichnung:</asp:Label>&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtname" runat="server"></asp:TextBox>
    &nbsp;&nbsp;
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Änderungen speichern" Font-Bold="True" />
    &nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Button ID="btnSportHinzu" runat="server" Text="hinzufügen" Font-Bold="True" Font-Size="Medium" OnClick="btnSportHinzu_Click"/>
    <br />
    </asp:Content>
