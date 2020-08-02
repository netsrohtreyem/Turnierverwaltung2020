<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Verwaltung.aspx.cs" Inherits="Turnierverwaltung2020.Views.Verwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Turnierverwaltung:</h3>
    <br />
    <br />
    <asp:Table runat="server" Width="100%" >
        <asp:TableRow>
            <asp:TableCell Width="20%">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Views/Personenverwaltung"><h3>Personenverwaltung</h3></asp:HyperLink>
            </asp:TableCell>
            <asp:TableCell Width="20%" HorizontalAlign="Center">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Views/Mannschaftsverwaltung"><h3>Mannschaftsverwaltung</h3></asp:HyperLink>
            </asp:TableCell>
            <asp:TableCell Width="20%" HorizontalAlign="Center">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Views/Turnierverwaltung"><h3>Turnierverwaltung</h3></asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
