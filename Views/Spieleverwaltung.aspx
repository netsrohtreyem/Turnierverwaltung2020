<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Spieleverwaltung.aspx.cs" Inherits="Turnierverwaltung2020.Views.Spieleverwaltung" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="font-weight: bold">Turnier durchführen</h2>
    <br />
    <h3>Wählen Sie ein Turnier aus:</h3>
    <br />
    <asp:DropDownList ID="drpListTurniere" runat="server" Font-Size="Medium">
        <asp:ListItem>kein Turnier vorhanden</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnTurnierAuswahl" runat="server" Text="auswählen" OnClick="btnTurnierAuswahl_Click" />
    <br />
    <br />
    <asp:Label ID="lblspieltag" runat="server" Font-Size="Large" Text="Spieltag:" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="drplistSpieltag" runat="server" Font-Size="Medium" Visible="false">
        <asp:ListItem>-</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnspieltagauswaehlen" runat="server" Text="auswählen" Visible="false" OnClick="btnspieltagauswaehlen_Click" />
     <br />
     <br />
    <asp:Button ID="btnNeu" runat="server" Text="Ein neues Spiel anlegen" Visible="false" OnClick="btnNeu_Click"/>
    <br />
    <br />
    <asp:Button ID="btnAutomatik" runat="server" Text ="automatisch alle Spiele erzeugen" OnClick="btnAutomatik_Click" />
     &nbsp;&nbsp;
    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="Hin- und Rückspiele" OnCheckedChanged="CheckBox1_CheckedChanged" Visible="False" AutoPostBack="True" />
     &nbsp;&nbsp;
    <asp:CheckBox ID="CheckBox2" runat="server" Text="Nur Hinspiele" OnCheckedChanged="CheckBox2_CheckedChanged" Visible="False" AutoPostBack="True" />
    <br />
     <br />
    <asp:Label ID="lblTitelTable" runat="server" Visible="false" Font-Bold="true" Font-Size="X-Large">Vorhandene Spiele</asp:Label>
    <br />
    <br />
    <asp:Table ID="tblSpiele" runat="server" Visible="false" Width="100%" BorderStyle="Solid" BorderWidth="2px" CellPadding="1" CellSpacing="5" BorderColor="Black" Font-Bold="True" Font-Size="Medium" GridLines="Both" HorizontalAlign="Center" >
        <asp:TableHeaderRow BackColor="#66FFFF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle" Width="100%">
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="25%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="25%"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="5%" HorizontalAlign="Center"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    
    <asp:Label ID="lbldummy" runat="server" style="display: none"/> 
    <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server"  
                                    TargetControlID ="lbldummy" 
                                    PopupControlID="pnlpopup" 
                                    CancelControlID="btnCancel" 
                                    BackgroundCssClass="modalBackground" 
                                    DropShadow="true" />

    <asp:panel id="pnlpopup" style="display: none" runat="server">
	    <div class="ModalWindow">
                <div class="PopupHeader" id="PopupHeader">
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: medium; font-weight: bold">Ein neues Spiel erzeugen</p>
                </div>
                <div class="PopupBody">
                    <p style="font-size: medium; font-weight: bold; font-family: Arial">Geben Sie die Daten des Spiels ein:</p>
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold">Spieltag (oder Runde):</p>
                    <asp:TextBox ID="txtnumber" runat="server" Font-Bold="true" type="number" min="0" max="50" step="1" Width="30px" Font-Size="Small"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txtnumber" FilterType="Numbers" />
                    <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" Enabled="true" 
                        TargetControlID="txtnumber"
                        Minimum="1"
                        Maximum="99"
                        Step ="1"
                        Width="100"/>
                    <br />
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold">Mannschaft1:</p>
                    <asp:DropDownList ID="drplstMannschaft1" runat="server" Font-Bold="True" Font-Size="Small"></asp:DropDownList>
                    <br />
                    Tore:<asp:TextBox ID="txttore1" runat="server"></asp:TextBox>
                    <br />
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold">Mannschaft2:</p>
                    <asp:DropDownList ID="drplstMannschaft2" runat="server" Font-Bold="True" Font-Size="Small"></asp:DropDownList>
                    <br />
                    Tore:<asp:TextBox ID="txttore2" runat="server"></asp:TextBox>
                    <br />
                </div>
            
                <div class="Controls">
                    <asp:Button ID="btnOkay" runat="server" Text="Speichern" OnClick="btnSpeichern_Click" Font-Bold="True" Font-Size="Small" />
                    <input id="btnCancel" type="button" value="Abbrechen" style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold" />
		        </div>
        </div>
    </asp:panel>
    
    <asp:Label ID="lbldummy2" runat="server" style="display: none"/> 
    <ajaxToolkit:ModalPopupExtender ID="mpe2" runat="server"  
                                    TargetControlID ="lbldummy2" 
                                    PopupControlID="pnlpopup2" 
                                    CancelControlID="btnCancel2" 
                                    BackgroundCssClass="modalBackground" 
                                    DropShadow="true" />

    <asp:panel id="pnlpopup2" style="display: none" runat="server">
	    <div class="ModalExt1Window">
                <div class="PopupHeader" id="PopupHeader2">
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: medium; font-weight: bold">Ein neues Spiel erzeugen</p>
                </div>
                <div class="PopupBody">
                    <p style="font-size: medium; font-weight: bold; font-family: Arial">Geben Sie die Daten des Spiels ein:</p>
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold">Teilnehmer1:</p>
                    <asp:DropDownList ID="drplstGruppe1" runat="server" Font-Bold="True" Font-Size="Small"></asp:DropDownList>
                    <br />
                    <br />
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold">Teilnehmer2:</p>
                    <asp:DropDownList ID="drplstGruppe2" runat="server" Font-Bold="True" Font-Size="Small"></asp:DropDownList>
                    <br />
                    <br />
                </div>
            
                <div class="Controls">
                    <asp:Button ID="btnOkay2" runat="server" Text="Speichern" OnClick="btnSpeichern2_Click" Font-Bold="True" Font-Size="Small" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnCancel2" type="button" value="Abbrechen" style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold" />
		        </div>
        </div>
    </asp:panel>

    <asp:Label ID="lbldummy3" runat="server" style="display: none"/> 
    <ajaxToolkit:ModalPopupExtender ID="mpe3" runat="server"  
                                    TargetControlID ="lbldummy3" 
                                    PopupControlID="pnlpopup3" 
                                    CancelControlID="btnCancel3" 
                                    BackgroundCssClass="modalBackground" 
                                    DropShadow="true" />

    <asp:panel id="pnlpopup3" style="display: none" runat="server">
	    <div class="ModalExt2Window">
                <div class="PopupHeader" id="PopupHeader3">
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: medium; font-weight: bold">Spiele Daten ändern</p>
                </div>

                <div class="PopupBody">
                    <p style="font-size: medium; font-weight: bold; font-family: Arial">Geben Sie die Daten des Spiels ein:</p>
                    <br />
                    <asp:Label ID="Teilnehmer1" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblteil" runat="server" Font-Bold="True" Font-Size="Small"> : </asp:Label>
                    &nbsp;
                    <asp:Label ID="Teilnehmer2" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="Ergebnis1" runat="server" Font-Bold="True" Font-Size="Small" Width="50" TabIndex ="0" AutoCompleteType="Disabled"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="Ergebnis1" FilterType="Numbers" />
                    &nbsp;
                    <asp:Label ID="lblerg" runat="server" Font-Bold="True" Font-Size="Small"> : </asp:Label>
                    &nbsp;
                    <asp:TextBox ID="Ergebnis2" runat="server" Font-Bold="True" Font-Size="Small" Width="50" TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="Ergebnis2" FilterType="Numbers" />
                    <br />
                    <br />
                </div>
            
                <div class="Controls">
                    <asp:Button ID="btnOkay3" runat="server" TabIndex="2" Text="Speichern" OnClick="btnSpeichern3_Click" Font-Bold="True" Font-Size="Small" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnCancel3" type="button" tabindex="3" value="Abbrechen" style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold" />
		        </div>
        </div>
    </asp:panel>

        
    <asp:Label ID="lbldummy4" runat="server" style="display: none"/> 
    <ajaxToolkit:ModalPopupExtender ID="mpe4" runat="server"  
                                    TargetControlID ="lbldummy4" 
                                    PopupControlID="pnlpopup4" 
                                    CancelControlID="btnCancel4" 
                                    BackgroundCssClass="modalBackground" 
                                    DropShadow="true" />

    <asp:panel id="pnlpopup4" style="display: none" runat="server">
	    <div class="ModalExt3Window">
                <div class="PopupHeader" id="PopupHeader4">
                    <p style="font-family: Arial, Helvetica, Sans-Serif; font-size: medium; font-weight: bold">Achtung Hinweis!!</p>
                </div>
                <div class="PopupBody">
                    <p style="font-size: medium; font-weight: bold; font-family: Arial">Wenn Sie fortfahren werden alle vorhandenen Spiele gelöscht!</p>                    
                </div>
            
                <div class="Controls">
                    <asp:Button ID="btnOkay4" runat="server" Text="Ok Weiter" OnClick="btnSpeichern4_Click" Font-Bold="True" Font-Size="Small" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnCancel4" type="button" value="Abbrechen" style="font-family: Arial, Helvetica, Sans-Serif; font-size: small; font-weight: bold" />
		        </div>
        </div>
    </asp:panel>
</asp:Content>

