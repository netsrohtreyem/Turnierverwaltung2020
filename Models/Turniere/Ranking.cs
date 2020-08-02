using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class Ranking
    {
        #region Eigenschaften
        private List<string> _Titelzeile;
        private List<TableRow> _Zeilen;
        private sportart _Sportart;
        #endregion

        #region Accessoren/Modifier
        public List<string> Titelzeile { get => _Titelzeile; set => _Titelzeile = value; }
        public List<TableRow> Zeilen { get => _Zeilen; set => _Zeilen = value; }
        public sportart Sportart { get => _Sportart; set => _Sportart = value; }
        #endregion

        #region Konstruktoren
        public Ranking()
        {
            this.Sportart = new sportart();
            this.Titelzeile = null;
            this.Zeilen = null;
        }
        public Ranking(sportart value, List<string> titel, List<TableRow> rows)
        {
            this.Sportart = value;
            this.Titelzeile = titel;
            this.Zeilen = rows;
        }

        public Ranking(Ranking value)
        {
            this.Sportart = value.Sportart;
            this.Titelzeile = value.Titelzeile;
            this.Zeilen = value.Zeilen;
        }
        #endregion

        #region Worker
        public void setTitel(List<string> zeile)
        {
            this.Titelzeile = zeile;
        }
        public void addZeile(TableRow row)
        {
            this.Zeilen.Add(row);
        }

        public void clearZeilen()
        {
            this.Zeilen.Clear();
        }
        #endregion
    }
}