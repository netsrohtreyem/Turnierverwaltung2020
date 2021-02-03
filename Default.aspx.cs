using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public partial class _Default : Page
    {
        private Controller _Verwalter;

        public Controller Verwalter { get => _Verwalter; set => _Verwalter = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session.Count > 0)
            {
                Verwalter = (Controller)this.Session["Verwalter"];                
            }
            else
            {
                Verwalter = Global.getVerwalter();
                if (Verwalter != null)
                {
                    this.Session["Verwalter"] = Verwalter;
                }
                else
                {
                    Response.Redirect("http://www.google.com");
                }
                /*#region Testdaten
                //Standart setzen
                #region Sportarten
                sportart neu = new sportart("Fussball", true, false, 3, 0, 1);
                neu.id = 1;
                this.Verwalter.Sportarten.Clear();
                this.Verwalter.Sportarten.Add(neu);
                neu = new sportart("Handball", true, false, 2, 2, 1);
                neu.id = 2;
                this.Verwalter.Sportarten.Add(neu);
                neu = new sportart("Tennis", true, true, 0, 0, 0);
                neu.id = 3;
                this.Verwalter.Sportarten.Add(neu);
                neu = new sportart("Tischtennis", false, true, 1, 1, 0);
                neu.id = 4;
                this.Verwalter.Sportarten.Add(neu);
                #endregion

                #region Personen
                Teilnehmer teilnehmer1 = new Fussballspieler("Müller", "Thomas", new DateTime(1989, 9, 13), 123, "Mittelfeld", 23, this.Verwalter.Sportarten[0]);
                this.Verwalter.AddPerson((Person)teilnehmer1);
                Teilnehmer teilnehmer2 = new Fussballspieler("Lewandowski", "Robert", new DateTime(1988, 8, 21), 220, "Stürmer", 95, this.Verwalter.Sportarten[0]);
                this.Verwalter.AddPerson((Person)teilnehmer2);
                Teilnehmer teilnehmer3 = new Fussballspieler("Hitz", "Marvin", new DateTime(1987, 9, 18), 96, "Torwart", 0, this.Verwalter.Sportarten[0]);
                this.Verwalter.AddPerson((Person)teilnehmer3);
                Teilnehmer teilnehmer4 = new Fussballspieler("Haaland", "Erling", new DateTime(200, 7, 21), 43, "Torwart", 31, this.Verwalter.Sportarten[0]);
                this.Verwalter.AddPerson((Person)teilnehmer4);
                Teilnehmer teilnehmer5 = new WeitererSpieler("Lustig", "Peter", new DateTime(1974, 12, 4),this.Verwalter.Sportarten[3],123,12);
                this.Verwalter.AddPerson((Person)teilnehmer5);
                Teilnehmer teilnehmer6 = new WeitererSpieler("Bärig", "Bernd", new DateTime(1978, 4, 6), this.Verwalter.Sportarten[3],245, 46);
                this.Verwalter.AddPerson((Person)teilnehmer6);
                #endregion

                #region Mannschaften
                Mannschaft man1 = new Mannschaft("FC Augsburg", this.Verwalter.Sportarten[0]);
                Mannschaft man2 = new Mannschaft("Hertha BSC", this.Verwalter.Sportarten[0]);
                Mannschaft man3 = new Mannschaft("Union Berlin", this.Verwalter.Sportarten[0]);
                Mannschaft man4 = new Mannschaft("Arminia Bielefeld", this.Verwalter.Sportarten[0]);
                Mannschaft man5 = new Mannschaft("Werder Bremen", this.Verwalter.Sportarten[0]);
                Mannschaft man6 = new Mannschaft("Borussia Dortmund", this.Verwalter.Sportarten[0]);
                Mannschaft man7 = new Mannschaft("Eintracht Frankfurt", this.Verwalter.Sportarten[0]);
                Mannschaft man8 = new Mannschaft("SC Freiburg", this.Verwalter.Sportarten[0]);
                Mannschaft man9 = new Mannschaft("TSG Hoffenheim", this.Verwalter.Sportarten[0]);
                Mannschaft man10 = new Mannschaft("1.FC Köln", this.Verwalter.Sportarten[0]);
                Mannschaft man11 = new Mannschaft("RB Leibzig", this.Verwalter.Sportarten[0]);
                Mannschaft man12 = new Mannschaft("Bayer 04 Leverkusen", this.Verwalter.Sportarten[0]);
                Mannschaft man13 = new Mannschaft("FSV Mainz 05", this.Verwalter.Sportarten[0]);
                Mannschaft man14 = new Mannschaft("Borussia Mönchengladbach", this.Verwalter.Sportarten[0]);
                Mannschaft man15 = new Mannschaft("FC Bayern München", this.Verwalter.Sportarten[0]);
                Mannschaft man16 = new Mannschaft("FC Schalke 04", this.Verwalter.Sportarten[0]);
                Mannschaft man17 = new Mannschaft("VFB Stuttgart", this.Verwalter.Sportarten[0]);
                Mannschaft man18 = new Mannschaft("VFL Wolfsburg", this.Verwalter.Sportarten[0]);

                man15.Add((Person)this.Verwalter.getPerson(1));
                man15.Add((Person)this.Verwalter.getPerson(2));
                man6.Add((Person)this.Verwalter.getPerson(3));
                man6.Add((Person)this.Verwalter.getPerson(4));
                this.Verwalter.AddMannschaft(man1);
                this.Verwalter.AddMannschaft(man2);
                this.Verwalter.AddMannschaft(man3);
                this.Verwalter.AddMannschaft(man4);
                this.Verwalter.AddMannschaft(man5);
                this.Verwalter.AddMannschaft(man6);
                this.Verwalter.AddMannschaft(man7);
                this.Verwalter.AddMannschaft(man8);
                this.Verwalter.AddMannschaft(man9);
                this.Verwalter.AddMannschaft(man10);
                this.Verwalter.AddMannschaft(man11);
                this.Verwalter.AddMannschaft(man12);
                this.Verwalter.AddMannschaft(man13);
                this.Verwalter.AddMannschaft(man14);
                this.Verwalter.AddMannschaft(man15);
                this.Verwalter.AddMannschaft(man16);
                this.Verwalter.AddMannschaft(man17);
                this.Verwalter.AddMannschaft(man18);
                #endregion

                #region Gruppen
                Gruppe neuegruppe = new Gruppe("Testgruppe1", this.Verwalter.Sportarten[3]);
                neuegruppe.Add((Person)this.Verwalter.getPerson(5));
                neuegruppe.Add((Person)this.Verwalter.getPerson(6));
                this.Verwalter.AddGruppe(neuegruppe, null);
                #endregion

                #region Turniere
                #region Bundesliga 2020
                Turnier testneu = new MannschaftsTurnier("Fussball Bundesliga 2020/21", this.Verwalter.Sportarten[0]);
                testneu.addTeilnehmer(man1);
                testneu.addTeilnehmer(man2);
                testneu.addTeilnehmer(man3);
                testneu.addTeilnehmer(man4);
                testneu.addTeilnehmer(man5);
                testneu.addTeilnehmer(man6);
                testneu.addTeilnehmer(man7);
                testneu.addTeilnehmer(man8);
                testneu.addTeilnehmer(man9);
                testneu.addTeilnehmer(man10);
                testneu.addTeilnehmer(man11);
                testneu.addTeilnehmer(man12);
                testneu.addTeilnehmer(man13);
                testneu.addTeilnehmer(man14);
                testneu.addTeilnehmer(man15);
                testneu.addTeilnehmer(man16);
                testneu.addTeilnehmer(man17);
                testneu.addTeilnehmer(man18);
                
                this.Verwalter.AddTurnier(testneu);
                this.Verwalter.SelectedTurnierSpieltag = 1;
                this.Verwalter.SelectedTurnierIndex = 1;
                this.Verwalter.SelectedTurnier = this.Verwalter.Turniere[0];

                #region Spieltag1
                Spiel neuesSpiel = new Mannschaftsspiel(testneu, man15, man16, 1);
                neuesSpiel.setErgebniswert1("8");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man7, man4, 1);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man5, man2, 1);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("4");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man10, man9, 1);
                neuesSpiel.setErgebniswert1("2");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man17, man8, 1);
                neuesSpiel.setErgebniswert1("2");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man3, man1, 1);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man6, man14, 1);
                neuesSpiel.setErgebniswert1("3");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man11, man13, 1);
                neuesSpiel.setErgebniswert1("3");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);

                neuesSpiel = new Mannschaftsspiel(testneu, man18, man12, 1);
                neuesSpiel.setErgebniswert1("0");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                #endregion

                #region Spieltag2
                neuesSpiel = new Mannschaftsspiel(testneu, man2, man7, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man1, man6, 2);
                neuesSpiel.setErgebniswert1("2");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man4, man10, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man16, man5, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man8, man18, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man12, man11, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man14, man3, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man13, man17, 2);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("4");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man9, man15, 2);
                neuesSpiel.setErgebniswert1("2");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                #endregion

                #region Spieltag3
                neuesSpiel = new Mannschaftsspiel(testneu, man3, man13, 3);
                neuesSpiel.setErgebniswert1("4");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man17, man12, 3);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man10, man14, 3);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man7, man9, 3);
                neuesSpiel.setErgebniswert1("2");
                neuesSpiel.setErgebniswert2("1");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man6, man8, 3);
                neuesSpiel.setErgebniswert1("4");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man5, man4, 3);
                neuesSpiel.setErgebniswert1("1");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man18, man1, 3);
                neuesSpiel.setErgebniswert1("0");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man15, man2, 3);
                neuesSpiel.setErgebniswert1("4");
                neuesSpiel.setErgebniswert2("3");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                neuesSpiel = new Mannschaftsspiel(testneu, man11, man16, 3);
                neuesSpiel.setErgebniswert1("4");
                neuesSpiel.setErgebniswert2("0");
                this.Verwalter.AddSpielToMannschaftsTurnier(neuesSpiel);
                #endregion
                #endregion


                testneu = new GruppenTurnier("Tischtennisturnier 2021", this.Verwalter.Sportarten[3]);
                testneu.addTeilnehmer(neuegruppe);
                this.Verwalter.AddTurnier(testneu);


                this.Verwalter.SelectedTurnierSpieltag = 1;
                this.Verwalter.SelectedTurnierIndex = -1;
                this.Verwalter.SelectedTurnier = null;
                #endregion
                #endregion*/
            }
            if (this.IsPostBack)
            {

            }
            else
            {
                LoadSportarten();
            }
            if (this.Verwalter.SelectedSportart != null)
            {
                this.drpdwList1.SelectedValue = this.Verwalter.SelectedSportart;
            }
            else
            {

            }

            if (this.CheckBox1.Checked)
            {
                this.CheckBox2.Checked = false;
            }
            else
            {
                this.CheckBox2.Checked = true;
            }           
        }

        protected void btnSportHinzu_Click(object sender, EventArgs e)
        {
            sportart neu = new sportart();
            if (this.txtname.Text != "")
            {
                bool vorhanden = false;
                foreach (ListItem ls in this.drpdwList1.Items)
                {
                    if (ls.Text == this.txtname.Text)
                    {
                        vorhanden = true;
                        break;
                    }
                    else
                    { }
                }
                if (!vorhanden)
                {
                    neu.name = this.txtname.Text;
                    neu.Mannschaft = this.CheckBox1.Checked;
                    neu.Einzel = this.CheckBox2.Checked;
                    if (txtsieg.Text != "")
                    {
                        neu.PluspunkteproSpiel = Convert.ToInt32(txtsieg.Text);
                    }
                    else
                    {
                        neu.PluspunkteproSpiel = 0;
                    }
                    if (txtlost.Text != "")
                    {
                        neu.MinupunkteproSpiel = Convert.ToInt32(txtlost.Text);
                    }
                    else
                    {
                        neu.MinupunkteproSpiel = 0;
                    }
                    if (txtunentschieden.Text != "")
                    {
                        neu.UnentschiedenpunkteproSpiel = Convert.ToInt32(txtunentschieden.Text);
                    }
                    else
                    {
                        neu.UnentschiedenpunkteproSpiel = 0;
                    }
                    if (neu.IsOK())
                    {
                        this.Verwalter.AddSportArt(neu);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Die Angaben sind fehlerhaft!');", true);
                    }
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Diese Sportart gibt es schon!');", true);
                }
            }
            else
            { }
        }

        private void LoadSportarten()
        {
            this.drpdwList1.Items.Clear();
            this.drpdwList1.Items.Add("Sportart wählen");
            if (Verwalter.Sportarten.Count > 0)
            {
                foreach (sportart art in Verwalter.Sportarten)
                {
                    this.drpdwList1.Items.Add(art.name);
                }
            }
            else
            {
                this.drpdwList1.Items.Add("keine vorhanden");
            }
        }

        protected void btnloeschen_Click(object sender, EventArgs e)
        {
            string name = this.drpdwList1.SelectedValue;
            if(name != "Sportart wählen")
                {
                bool erfolg = this.Verwalter.DeleteSportart(name);
                if (!erfolg)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Die Sportart konnte nicht gelöscht werden, sie wird noch verwendet!');", true);
                }
                else
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            { }
        }

        //Änderungen speichern
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(this.txtlost.Text!="" &&
                this.txtsieg.Text != "" &&
                this.txtname.Text != "" &&
                this.txtunentschieden.Text != "" )
            {
                if (CheckBox1.Checked)
                {
                    this.Verwalter.SportartAktualisieren(this.txtname.Text,
                                                         this.txtlost.Text,
                                                         this.txtsieg.Text,
                                                         this.txtunentschieden.Text,true);
                }
                else
                {
                    this.Verwalter.SportartAktualisieren(this.txtname.Text,
                                                         this.txtlost.Text,
                                                         this.txtsieg.Text,
                                                         this.txtunentschieden.Text, false);

                }
            }
            else
            { }
            this.txtname.Enabled = true;
            this.Button1.Enabled = false;
        }

        //Sportart auswählen
        protected void drpdwList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpdwList1.SelectedValue != "Sportart wählen")
            {
                foreach (sportart sp in this.Verwalter.Sportarten)
                {
                    if (sp.name.Equals(drpdwList1.SelectedValue))
                    {
                        this.txtname.Enabled = false;
                        this.txtname.Text = sp.name;
                        this.txtlost.Text = sp.MinupunkteproSpiel.ToString();
                        this.txtsieg.Text = sp.PluspunkteproSpiel.ToString();
                        this.txtunentschieden.Text = sp.UnentschiedenpunkteproSpiel.ToString();
                        if (sp.Mannschaft)
                        {
                            this.CheckBox1.Checked = true;
                            this.CheckBox2.Checked = false;
                        }
                        else
                        {
                            this.CheckBox1.Checked = false;
                            this.CheckBox2.Checked = true;
                        }
                        break;
                    }
                    else
                    {

                    }
                }
                this.Button1.Enabled = true;
            }
            else
            {
                this.txtname.Text = "";
                this.txtlost.Text = "";
                this.txtsieg.Text = "";
                this.txtunentschieden.Text = "";
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBox1.Checked = true;
            this.CheckBox2.Checked = false;
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBox1.Checked = false;
            this.CheckBox2.Checked = true;
        }

        protected void btnListeladen_Click(object sender, EventArgs e)
        {
            if (fileupload.FileName != "")
            {
                Verwalter.SportartenAlsXMLLaden(fileupload, this);
                Response.Redirect(Request.RawUrl);
            }
            else
            { }
        }

        protected void btnListespeichern_Click(object sender, EventArgs e)
        {
            Verwalter.SportartenAlsXMLSichern(this);
        }
    }
}