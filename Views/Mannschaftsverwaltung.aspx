<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Mannschaftsverwaltung.aspx.cs" Inherits="Turnierverwaltung2020.Views.Mannschaftsverwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="font-weight: bold">Mannschaft- oder Einzelverwaltung</h1>
    <h2 style="font-weight: bold">Hinzufügen oder Bearbeiten einer Turnier Mannschaft oder einer Einzel - Gruppe</h2>
    <asp:Button ID="btnXMLsichern1" runat="server" Text="Mannschaftsliste als XML File downloaden" Font-Bold="True" Font-Size="Medium" OnClick="btnXMLsichern_Click1" />
    <asp:Button ID="btnXMLsichern2" runat="server" Text="Gruppenliste als XML File downloaden" Font-Bold="True" Font-Size="Medium" OnClick="btnXMLsichern_Click2" />
    <br />
    <br />
    <asp:FileUpload ID="fileupload" runat="server" Font-Bold="True" Font-Size="Medium" />
    <br />
    <asp:Button ID="btnUpload1" runat="server" Font-Bold="True" Font-Size="Medium" Text="Mannschaftsliste aus XML uploaden" OnClick="btnUpload_Click1" />
    <asp:Button ID="btnUpload2" runat="server" Font-Bold="True" Font-Size="Medium" Text="Gruppenliste aus XML uploaden" OnClick="btnUpload_Click2" />


    <p style="font-weight: bold; font-size: large;">Auswählen ob eine Mannschaft oder eine Turniergruppe angelegt oder geändert werden soll</p>
    <asp:RadioButtonList ID="rbListArt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbListArt_SelectedIndexChanged" Font-Size="Large">
        <asp:ListItem Selected="True" Text ="Mannschaft"></asp:ListItem>
        <asp:ListItem>Gruppen</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Label ID="lblsportart" runat="server" Text="Eine Sportart für die Mannschaft auswählen" Font-Bold="True" Font-Size="Large"></asp:Label>
    <br />
    <asp:DropDownList ID="drpSportart1" runat="server" AutoPostBack="false" Font-Bold="True" Font-Size="Medium" ToolTip="Hinzufügen via Personenverwaltung!"></asp:DropDownList>
    <br />
    <br />
    <asp:Table ID="tblEingabetabelle" runat="server" Enabled="true" Width="100%">
        <asp:TableRow Width="100%">
            <asp:TableCell Width="50%" Font-Bold="True" Font-Size="Medium" Text="Mannschaftsname: ">
                <asp:TextBox ID="txtName" runat="server" AutoCompleteType="Disabled" Width="100%" TabIndex ="0"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table ID="tblEingabetabelle2" runat="server" Enabled="true" Width="100%">
        <asp:TableRow Width="100%">
            <asp:TableCell Width="10%" Font-Bold="True" Font-Size="Medium" Text="Mitglieder der Mannschaft:"></asp:TableCell>
            <asp:TableCell Width="10%" Font-Bold ="True" Font-Size="Medium" Text =" "></asp:TableCell>
            <asp:TableCell Width="30%"  Font-Bold ="True" Font-Size="Medium" Text="verfügbare Personen:"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="45%" >
                <asp:ListBox ID="lstVorhandeneMitglieder" runat="server" Width="100%" Font-Bold ="True" Font-Size="Medium">
                        <asp:ListItem>bisher keine Mitglieder</asp:ListItem>
                </asp:ListBox>
            </asp:TableCell>
            <asp:TableCell Width="8%" >
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="imgBtnhinzu1" runat="server" ImageUrl="~\Images\Pfeillinks.png" ToolTip="Person hinzufügen" OnClick="imgBtnhinzu1_Click" />
                &nbsp;&nbsp;
                <asp:ImageButton ID="imgBtnweg1" runat="server" ImageUrl="~\Images\Pfeilrechts.png" ToolTip="Person entfernen" OnClick="imgBtnweg1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell Width="45%" >
                <asp:ListBox ID="lstVorhandenePersonen" runat="server" Width="100%" Font-Bold ="True" Font-Size="Medium">
                    <asp:ListItem>Niemand vorhanden</asp:ListItem>
                </asp:ListBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
       <br /> 
    <asp:Button ID="btnHinzufuegenAendern" runat="server" Text="Mannschaft hinzufügen" BackColor="Lime" Font-Bold="True" Font-Size="Medium" OnClick="btnHinzufuegen_Aendern_Click" />    
    <br />
    <br />
    <br />
    <asp:Label ID="lblAnzeige" runat="server" Visible="true" Text="Vorhandene Mannschaften:" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    <asp:Table ID="tblAnzeige" runat="server" Visible="true" Width="100%" BorderStyle="Solid" BorderWidth="2px" CellPadding="1" CellSpacing="5" BorderColor="Black" Font-Bold="True" Font-Size="Medium" GridLines="Both" HorizontalAlign="Center" >
        <asp:TableHeaderRow BackColor="#66FFFF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle" Width="100%">
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="25%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="10%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="25%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    </asp:Content>
