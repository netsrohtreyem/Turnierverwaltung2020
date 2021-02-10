<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="RankingAnzeige.aspx.cs" Inherits="Turnierverwaltung2020.Views.RankingAnzeige" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="font-weight: bold">Turnier - Ergebnisse anzeigen</h2>
    <asp:Panel ID="pnlTurnierauswahl" runat="server" >
    <br />
    <asp:Label ID="lblTurnier" runat="server" Text="Wählen Sie ein Turnier aus:" Font-Bold="True" Font-Size="Large"></asp:Label>
    <br />
    <br />
    <asp:DropDownList ID="drpListTurniere" runat="server" Font-Size="Medium">
        <asp:ListItem>kein Turnier vorhanden</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnTurnierAuswahl" runat="server" Text="auswählen" OnClick="btnTurnierAuswahl_Click"/>
    <br />
    <br />
    </asp:Panel>
    <asp:Panel ID="pnlGruppen" runat="server" Visible="false">
        <asp:Label ID="lblGruppentitel" runat="server" Text="Wählen Sie eine Gruppe aus:" Font-Size="Medium" Font-Bold="True"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="drplistGruppen" runat="server">
            <asp:ListItem Text="keine Gruppe vorhanden" ></asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnGruppenauswahl" runat="server" Text="Auswahl bestätigen" OnClick="btnGruppenauswahl_Click" />
        <br />
        <br />
    </asp:Panel>
    <asp:Label ID="lblTurniername" runat="server" Text="Tabelle für das Turnier " Font-Bold="True" Font-Size="Large" Visible="false"></asp:Label>
    <br />
    <br />
    <asp:Table ID="tblTurnier" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="1" CellSpacing="5" Font-Bold="True" Font-Size="Medium" GridLines="Both" HorizontalAlign="Center" Visible="false" Width="100%">
        <asp:TableHeaderRow ID="HeaderRow" BackColor="#66FFFF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle" Width="100%">
        </asp:TableHeaderRow>
    </asp:Table>

</asp:Content>
