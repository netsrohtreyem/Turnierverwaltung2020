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

        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session.Count > 0)
            {
                Verwalter = (Controller)this.Session["Verwalter"];
                //Verwalter.loadData();
            }
            else
            {
                Verwalter = new Controller();
                this.Session["Verwalter"] = Verwalter;             
                #region Testdaten
                //Standart setzen
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
                #endregion
            }
            if(!Verwalter.UserAuthentificated)
            {
                this.Response.Redirect(@"~\Views\login.aspx");
                return;
            }
            else
            {
                if(this.Verwalter.AuthentifactionRole)
                {

                }
                else
                {
                    this.drpdwList1.Visible = false;
                    this.btnloeschen.Visible = false;
                    this.CheckBox1.Visible = false;
                    this.CheckBox2.Visible = false;
                    this.lblsieg.Visible = false;
                    this.lbllost.Visible = false;
                    this.lblunentschieden.Visible = false;
                    this.lblbezeichnung.Visible = false;
                    this.txtlost.Visible = false;
                    this.txtsieg.Visible = false;
                    this.txtunentschieden.Visible = false;
                    this.txtSportart.Visible = false;
                    this.btnSportHinzu.Visible = false;
                }
            }

            if (this.IsPostBack)
            {

            }
            else
            {
                LoadSportarten();
            }
        }

        protected void btnSportHinzu_Click(object sender, EventArgs e)
        {
            sportart neu = new sportart();
            if (this.txtSportart.Text != "")
            {
                bool vorhanden = false;
                foreach (ListItem ls in this.drpdwList1.Items)
                {
                    if (ls.Text == this.txtSportart.Text)
                    {
                        vorhanden = true;
                        break;
                    }
                    else
                    { }
                }
                if (!vorhanden)
                {
                    neu.name = this.txtSportart.Text;
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
                    this.txtSportart.Text = "";
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
    }
}