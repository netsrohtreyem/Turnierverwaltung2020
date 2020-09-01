﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Turnierverwaltung.aspx.cs" Inherits="Turnierverwaltung2020.Views.Turnierverwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h1 style="font-weight: bold">Turnierverwaltung</h1>--%>
    <asp:Label ID="lbltitel1" runat="server" Font-Bold="true" Font-Size="XX-Large" Text="Turnierverwaltung"></asp:Label>
    <br />
    <br />
    <%--<h2 style="font-weight: bold">Hinzufügen bzw Bearbeiten eines Turniers:</h2>--%>
    <asp:Label ID="lbltitel2" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Hinzufügen bzw Bearbeiten eines Turniers:"></asp:Label>
    <br />
    <br />
    <%--<p style="font-weight: bold; font-size: large;">Art des Turniers auswählen/ändern</p>--%>
    <asp:Label ID="lbltitel3" runat="server" Font-Bold="true" Font-Size="Large" Text="Art des Turniers auswählen/ändern"></asp:Label>
    <br />
    <br />
    <asp:RadioButtonList ID="rbListArt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbListArt_SelectedIndexChanged" Font-Size="Medium">
        <asp:ListItem Selected="True">Mannschaft</asp:ListItem>
        <asp:ListItem>Gruppen</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <br />
    <%--<h4 style="font-weight: bold">Eine Sportart für das Turnier auswählen bzw. ändern:</h4>--%>
    <asp:Label ID="lbltitel4" runat="server" Font-Bold="true" Font-Size="Large" Text="Eine Sportart für das Turnier auswählen bzw. ändern:"></asp:Label>
    <br />
    <br />
    <asp:DropDownList ID="drpSportart1" runat="server" AutoPostBack="false" Font-Bold="True" Font-Size="Medium"></asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Table ID="tblNameTurnier" runat="server" Width="100%">
        <asp:TableRow Width="100%">
            <asp:TableCell Width="10%" Font-Bold ="True" Font-Size="Medium" Text="Turnier Name:"></asp:TableCell>
           <asp:TableCell Width="90%" Font-Bold ="True" Font-Size="Medium">
                <asp:TextBox ID="txtNameTurnier" runat="server" AutoCompleteType="Disabled" Width="90%"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:Table ID="tblEingabetabellegr" runat="server" Width="100%">
        <asp:TableRow Width="100%">
            <asp:TableCell Width="10%" Font-Bold="True" Font-Size="Medium" Text="vorhandene Mannschaften:"></asp:TableCell>
            <asp:TableCell Width="10%" Font-Bold ="True" Font-Size="Medium" Text =" "></asp:TableCell>
            <asp:TableCell Width="30%"  Font-Bold ="True" Font-Size="Medium" Text="verfügbare Mannschaften:"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Width="100%">
             <asp:TableCell Width="45%" Font-Bold="True" Font-Size="Medium">
                <asp:ListBox ID="lstVorhandeneTeilnehmer" runat="server" Width="100%" Font-Bold ="True" Font-Size="Medium">
                    <asp:ListItem>bisher keine Mannschaften</asp:ListItem>
                </asp:ListBox>
            </asp:TableCell>
             <asp:TableCell Width="8%" HorizontalAlign="Center">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="imgBtnhinzu1" runat="server" ImageUrl="~\Images\Pfeillinks.png" ToolTip="Teilnehmer hinzufügen" OnClick="imgBtnhinzu1_Click" />
                &nbsp;&nbsp;
                <asp:ImageButton ID="imgBtnweg1" runat="server" ImageUrl="~\Images\Pfeilrechts.png" ToolTip="Teilnehmer entfernen" OnClick="imgBtnweg1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell Width="45%" Font-Bold="True" Font-Size="Medium">
                <asp:ListBox ID="lstVerfuegbareTeilnehmer" runat="server" Width="100%" Font-Bold ="True" Font-Size="Medium">
                    <asp:ListItem>keine Mannschaften vorhanden</asp:ListItem>
                </asp:ListBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:Button ID="btnTurnierHinzufuegen" runat="server" Text="Turnier hinzufügen" BackColor="Lime" Font-Bold="True" Font-Size="Medium" OnClick="btnTurnierHinzufuegenAendern_Click"/>    
    <br />
    <br />
    <hr />
    <br />
    <asp:Label ID="lblTurnierAnzeige" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Vorhandene Turniere:" Visible="true"></asp:Label>
    <asp:Table ID="tblTurnierAnzeige" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="1" CellSpacing="5" Font-Bold="True" Font-Size="Medium" GridLines="Both" HorizontalAlign="Center" Visible="true" Width="100%">
            <asp:TableHeaderRow BackColor="#66FFFF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Center" Width="5%"></asp:TableHeaderCell>
                <asp:TableHeaderCell Width="30%"></asp:TableHeaderCell>
                <asp:TableHeaderCell Width="30%"></asp:TableHeaderCell>
                <asp:TableHeaderCell Width="30%"></asp:TableHeaderCell>
                <asp:TableHeaderCell Width="5%"></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Width="5%">Edit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Width="5%">Delete</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
</asp:Content>
