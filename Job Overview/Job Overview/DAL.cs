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
        #region Champs privés
        private static Dictionary<string, Personne> _employés;
        private static Dictionary<string, Activités> _activités;
        #endregion

        #region Propriétés
        #endregion

        #region Constructeurs
        static DAL()
        {
            _employés = new Dictionary<string, Personne>();
            _activités = new Dictionary<string, Activités>();
            _activités.Add("DBE", Activités.DBE);
            _activités.Add("ARF", Activités.ARF);
            _activités.Add("ANF", Activités.ANF);
            _activités.Add("DES", Activités.DES);
            _activités.Add("INF", Activités.INF);
            _activités.Add("ART", Activités.ART);
            _activités.Add("ANT", Activités.ANT);
            _activités.Add("DEV", Activités.DEV);
            _activités.Add("RPT", Activités.RPT);
            _activités.Add("TES", Activités.TES);
            _activités.Add("GDP", Activités.GDP);
        }
        #endregion

        #region Méthodes privées
        private static void ChargerEmployés(string path = null)
        {
            if (path == null)
            {
                _employés.Add("GL", new Personne("GL", "Geneviève", "LECLERCQ", Métiers.ANA));
                _employés.Add("AF", new Personne("AF", "Angèle", "FERRAND", Métiers.ANA));
                _employés.Add("BN", new Personne("BN", "Balthazar", "NORMAND", Métiers.CDP));
                _employés.Add("RF", new Personne("RF", "Raymond", "FISHER", Métiers.DEV));
                _employés.Add("LB", new Personne("LB", "Lucien", "BUTLER", Métiers.DEV));
                _employés.Add("RB", new Personne("RB", "Roseline", "BEAUMONT", Métiers.DEV));
                _employés.Add("MW", new Personne("MW", "Marguerite", "WEBER", Métiers.DES));
                _employés.Add("HK", new Personne("HK", "Hilaire", "KLEIN", Métiers.TES));
                _employés.Add("NP", new Personne("NP", "Nino", "PALMER", Métiers.TES));
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
                        ligne[1], _employés[ligne[2]], _activités[ligne[3]], 
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
