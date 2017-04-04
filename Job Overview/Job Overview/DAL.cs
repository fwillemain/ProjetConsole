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
        public static Dictionary<string, Personne> DicoEmployés { get; }    // Liste des employés référencés par leur code
        public static Dictionary<string, ActivitésProd> DicoActivitésProd { get; }  // Liste des activités de production référencées par leur code
        public static Dictionary<string, ActivitésAnnexes> DicoActivitésAnnexes { get; }    // Liste des activités annexes référencées par leur code
        #endregion

        #region Constructeurs
        static DAL()
        {
            // Initialise les trois listes et fournis les valeurs en dure des listes qui n'évolueront pas : Activités prod et annexe (temporaire)
            DicoEmployés = new Dictionary<string, Personne>();

            DicoActivitésProd = new Dictionary<string, ActivitésProd>();
            DicoActivitésProd.Add("DBE", ActivitésProd.DBE);
            DicoActivitésProd.Add("ARF", ActivitésProd.ARF);
            DicoActivitésProd.Add("ANF", ActivitésProd.ANF);
            DicoActivitésProd.Add("DES", ActivitésProd.DES);
            DicoActivitésProd.Add("INF", ActivitésProd.INF);
            DicoActivitésProd.Add("ART", ActivitésProd.ART);
            DicoActivitésProd.Add("ANT", ActivitésProd.ANT);
            DicoActivitésProd.Add("DEV", ActivitésProd.DEV);
            DicoActivitésProd.Add("RPT", ActivitésProd.RPT);
            DicoActivitésProd.Add("TES", ActivitésProd.TES);
            DicoActivitésProd.Add("GDP", ActivitésProd.GDP);

            // TODO (optionnel) : DAL::DAL() gérer une liste évolutive des activités annexes
            DicoActivitésAnnexes = new Dictionary<string, ActivitésAnnexes>();
            DicoActivitésAnnexes.Add("INDEFINI", ActivitésAnnexes.Indéfinie);
            DicoActivitésAnnexes.Add("AIDE", ActivitésAnnexes.AideCollègue);
            DicoActivitésAnnexes.Add("APPUI", ActivitésAnnexes.AppuiAutreService);
            DicoActivitésAnnexes.Add("REUNION", ActivitésAnnexes.Réunion);
            DicoActivitésAnnexes.Add("DDP", ActivitésAnnexes.TravailDDP);
            DicoActivitésAnnexes.Add("EVENT", ActivitésAnnexes.Event);
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Initialise la liste des employés, si un fichier n'est pas indiqué, fournira des données par défaut
        /// </summary>
        /// <param name="path"></param>
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
        /// <summary>
        /// Charge les données du fichier passé en paramètre et renvoi le projet instancié
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Projet ChargerDonnées(string path)
        {
            Projet projet = new Projet();
            string[] data = File.ReadAllLines(path);
            string[] ligne; // Tableau dont chaque élément contiendra toutes les informations pour instancier une tache de production
            for (int i = 1; i < data.Length; i++)
            {
                ligne = data[i].Split('\t');
                projet.AjouterTache(new TacheProd(int.Parse(ligne[0]), 
                        ligne[1], DicoEmployés[ligne[2]], DicoActivitésProd[ligne[3]], 
                    ligne[4], DateTime.Parse(ligne[5]), int.Parse(ligne[6]), int.Parse(ligne[7]), int.Parse(ligne[8])));
            }

            return projet;
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Retourne le projet instancié en chargeant les données du fichier passé en paramètre ainsi que les informations 
        /// sur les employés si un ficheir est fourni
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pathDataEmployés"></param>
        /// <returns></returns>
        public static Projet ChargerProjet(string path, string pathDataEmployés = null)
        {
            ChargerEmployés(pathDataEmployés);
            return ChargerDonnées(path);
        }
        #endregion
    }
}
