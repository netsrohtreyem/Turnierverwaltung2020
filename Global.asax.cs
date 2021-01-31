using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Turnierverwaltung2020
{
    public class Global : HttpApplication
    {
        private static List<Controller> _Verwalterliste;
        private static List<HttpSessionState> _Sessionliste;
        public static List<Controller> VerwalterListe { get => _Verwalterliste; set => _Verwalterliste = value; }
        public static List<HttpSessionState> SessionListe { get => _Sessionliste; set => _Sessionliste = value; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            VerwalterListe = new List<Controller>();
            SessionListe = new List<HttpSessionState>();
        }

        public static Controller getVerwalter()
        {
            foreach(Controller verw in VerwalterListe)
            {
                if(verw.HTTPSession.Equals(HttpContext.Current.Session.SessionID))
                {
                    return verw;
                }
                else
                { }
            }
            return null;
        }

        protected void Session_OnStart(Object sender, EventArgs e)
        {
            if (!SessionListe.Contains(HttpContext.Current.Session))
            {
                string session = HttpContext.Current.Session.SessionID;
                Controller neu = new Controller();
                neu.HTTPSession = session;
                VerwalterListe.Add(neu);
                SessionListe.Add(HttpContext.Current.Session);
            }
            else
            {

            }
        }
        protected void Session_OnEnd(Object sender, EventArgs e)
        {

            foreach (Controller c in VerwalterListe)
            {
                if (c.HTTPSession.Equals(Session.SessionID))
                {
                    VerwalterListe.Remove(c);
                    break;
                }
                else
                {
                }
            }
            foreach (HttpSessionState state in SessionListe)
            {
                if (state.SessionID.Equals(Session.SessionID))
                {
                    SessionListe.Remove(state);
                    break;
                }
                else
                { }
            }
        }
    }
}