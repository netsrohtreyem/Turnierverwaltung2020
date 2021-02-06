using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public partial class SiteMaster : MasterPage
    {
        private Controller _verwalter;

        public Controller Verwalter { get => _verwalter; set => _verwalter = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Configuration.ConfigurationManager.AppSettings["SessionWarning"] = "25";
            if (this.Session.Count > 0)
            {
                Verwalter = (Controller)this.Session["Verwalter"];
            }
            else
            {

            }
        }
    }
}