﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Personenverwaltung.aspx.cs" Inherits="Turnierverwaltung2020.Views.Personenverwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 >Personenverwaltung</h2>
    <h3 style="font-weight: bold">Hinzufügen oder Bearbeiten von Personen</h3> 
    <asp:Button Visible="false" ID="btnXMLsichern" runat="server" Text="Personenliste als XML-Datei downloaden" Font-Bold="True" Font-Size="Medium" OnClick="btnXMLsichern_Click" />
    <br />
    <br />
    <asp:FileUpload Visible="false" ID="fileupload" runat="server" Font-Bold="True" Font-Size="Medium" />
    <br />
    <asp:Button Visible="false" ID="btnUpload" runat="server" Font-Bold="True" Font-Size="Medium" Text="Personenliste als XML-Datei hochladen" OnClick="btnUpload_Click1" />

    <h3>Auswahl des Personen Typs:</h3>
    <asp:RadioButtonList ID="rdbtnList1" runat="server">
        <asp:ListItem>Fussballspieler</asp:ListItem>
        <asp:ListItem>Handballspieler</asp:ListItem>
        <asp:ListItem>Tennisspieler</asp:ListItem>
        <asp:ListItem>anderer Spielertyp</asp:ListItem>
        <asp:ListItem>Physiotherapeut</asp:ListItem>
        <asp:ListItem>Trainer</asp:ListItem>        
        <asp:ListItem>Person mit anderen Aufgaben</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="btnBestaetigen" runat="server" Text="Typ Auswahl bestätigen" BackColor="Lime" Font-Bold="True" Font-Size="Medium" OnClick="btnBestaetigen_Click"/>
    <br /> 
    <br />
    <asp:Table ID="tblEingabetabelle" runat="server" Enabled="false">
        <asp:TableRow Width="100%">
            <asp:TableCell Text="Vorname" Width="10%" Font-Bold="True" Font-Size="Medium"></asp:TableCell>
            <asp:TableCell Width="25%" Font-Bold="True" Font-Size="Medium">
                <asp:TextBox ID="txtVorname" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Text="Name" Width="10%" Font-Bold="True" Font-Size="Medium"></asp:TableCell>
            <asp:TableCell Width="20%" Font-Bold="True" Font-Size="Medium">
                <asp:TextBox ID="txtName" runat="server"  AutoCompleteType="Disabled"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Text="Geburtsdatum:" Width="20%" Font-Bold="True" Font-Size="Medium"></asp:TableCell>
            <asp:TableCell Width="25%" Font-Bold="True" Font-Size="Medium">
                <asp:TextBox ID="txtGeburtsdatum" TextMode="Date" runat="server"></asp:TextBox>
                <asp:CompareValidator ErrorMessage="(tt/mm/yyyy)" Display="Dynamic" ID="valcDate" ControlToValidate="txtGeburtsdatum" Operator="DataTypeCheck" Type="Date" runat="server"></asp:CompareValidator>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Button ID="btnSichern" runat="server" Text="Eingaben sichern" Enabled="false" BackColor="Lime" Font-Bold="True" Font-Size="Medium" OnClick="btnSichern_Click"/>
    <br />
    <br />
    <h3>Verfügbare Personen (bearbeiten und löschen):</h3> 
    <h4>sortieren: entsprechende Headerspalte anclicken</h4>
    <br />
    <asp:Table ID="tblAusgabetabelle" runat="server"  Width="100%" BorderStyle="Solid" BorderWidth="2px" CellPadding="1" CellSpacing="5" BorderColor="Black" Font-Bold="True" Font-Size="Medium" GridLines="Both" HorizontalAlign="Center" >
        <asp:TableHeaderRow BackColor="#66FFFF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle">
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="15%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="15%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="15%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="10%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="12%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>         
</asp:Content>
