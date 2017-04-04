using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    public static class DAL
    {
        #region Propriétés
        public static Dictionary<string, Personne> DicoEmployés { get; }
        public static Dictionary<string, Activités> DicoActivités { get; }
        #endregion

        #region Constructeurs
        static DAL()
        {
            DicoEmployés = new Dictionary<string, Personne>();
            DicoActivités = new Dictionary<string, Activités>();
            DicoActivités.Add("DBE", Activités.DBE);
            DicoActivités.Add("ARF", Activités.ARF);
            DicoActivités.Add("ANF", Activités.ANF);
            DicoActivités.Add("DES", Activités.DES);
            DicoActivités.Add("INF", Activités.INF);
            DicoActivités.Add("ART", Activités.ART);
            DicoActivités.Add("ANT", Activités.ANT);
            DicoActivités.Add("DEV", Activités.DEV);
            DicoActivités.Add("RPT", Activités.RPT);
            DicoActivités.Add("TES", Activités.TES);
            DicoActivités.Add("GDP", Activités.GDP);
        }
        #endregion

        #region Méthodes privées
        private static void ChargerEmployés(string path = null)
        {
            if (path == null)
            {
                DicoEmployés.Add("GL", new Personne("GL", "Geneviève", "LECLERCQ", Métiers.ANA));
                DicoEmployés.Add("AF", new Personne("AF", "Angèle", "FERRAND", Métiers.ANA));
                DicoEmployés.Add("BN", new Personne("BN", "Balthazar", "NORMAND", Métiers.CDP));
                DicoEmployés.Add("RF", new Personne("RF", "Raymond", "FISHER", Métiers.DEV));
                DicoEmployés.Add("LB", new Personne("LB", "Lucien", "BUTLER", Métiers.DEV));
                DicoEmployés.Add("RB", new Personne("RB", "Roseline", "BEAUMONT", Métiers.DEV));
                DicoEmployés.Add("MW", new Personne("MW", "Marguerite", "WEBER", Métiers.DES));
                DicoEmployés.Add("HK", new Personne("HK", "Hilaire", "KLEIN", Métiers.TES));
                DicoEmployés.Add("NP", new Personne("NP", "Nino", "PALMER", Métiers.TES));
            }
            else
            {
                throw new NotImplementedException();
                // TODO (optionnel) DAL::ChargerEmployés() charger fichier employés.
            }
        }

        private static DateTime StringToDateTime(string date)
        {
            string[] res = date.Split('/');
            return new DateTime(int.Parse(res[2]), int.Parse(res[1]), int.Parse(res[0]));
        }

        private static Projet ChargerDonnées(string path)
        {
            Projet projet = new Projet();
            string[] data = File.ReadAllLines(path);
            string[] ligne;
            for (int i = 1; i < data.Length; i++)
            {
                ligne = data[i].Split('\t');
                projet.AjouterTache(new TacheProd(int.Parse(ligne[0]), 
                        ligne[1], DicoEmployés[ligne[2]], DicoActivités[ligne[3]], 
                    ligne[4], DateTime.Parse(ligne[5]), int.Parse(ligne[6]), int.Parse(ligne[7]), int.Parse(ligne[8])));
            }

            //genomica.AjouterTache(new TacheAnnexe(1));

            return projet;
        }


        #endregion

        #region Méthodes publiques

        public static Projet ChargerProjet(string path, string pathDataEmployés = null)
        {
            ChargerEmployés(pathDataEmployés);
            return ChargerDonnées(path);

        }
        #endregion
    }
}
