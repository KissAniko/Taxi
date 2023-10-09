using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi
{
    internal class Fuvar
    {
        int taxiAzonosito;
        string indulasIdopontja;
        int utazasIdotartam;
        double megtettTavolsag;
        double viteldij;
        double borravalo;
        string fizetesModja;

        public Fuvar(int taxiAzonosito, string indulasIdopontja, int utazasIdotartam, double megtettTavolsag, double viteldij, double borravalo, string fizetesModja)
        {
            this.TaxiAzonosito = taxiAzonosito;
            this.IndulasIdopontja = indulasIdopontja;
            this.UtazasIdotartam = utazasIdotartam;
            this.MegtettTavolsag = megtettTavolsag;
            this.Viteldij = viteldij;
            this.Borravalo = borravalo;
            this.FizetesModja = fizetesModja;
        }

        public int TaxiAzonosito { get => taxiAzonosito; set => taxiAzonosito = value; }
        public string IndulasIdopontja { get => indulasIdopontja; set => indulasIdopontja = value; }
        public int UtazasIdotartam { get => utazasIdotartam; set => utazasIdotartam = value; }
        public double MegtettTavolsag { get => megtettTavolsag; set => megtettTavolsag = value; }
        public double Viteldij { get => viteldij; set => viteldij = value; }
        public double Borravalo { get => borravalo; set => borravalo = value; }
        public string FizetesModja { get => fizetesModja; set => fizetesModja = value; }
    }
}
