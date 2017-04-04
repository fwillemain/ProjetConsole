using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    public class Results    
    {
        #region Propriétés
        public Projet Projet { get; private set; }  // Projet dont les résultats seront retournés l'appel des différentes méthodes de la classe
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur prenant en paramètre le projet dont les résultats seront affichés
        /// </summary>
        /// <param name="p"></param>
        public Results(Projet p)
        {
            Projet = p; 
        }
        #endregion

        #region Méthodes privées
        private string RetournerDuréesTravail(string vers, Personne p)
        {
            var res = Projet.TachesProd.Where(t => (t.VersionProjet == vers) && (t.Personne == p));
            int dReal = res.Sum(d => d.DuréeRéalisée);
            int dRest = res.Sum(d => d.DuréeRestante);

            return string.Format("{1} {2} a réalisé {3} jours de travail, et il lui reste {4} jours de planifiés. ",
                vers, p.Prénom, p.Nom, dReal, dRest);
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Charge un nouveau projet
        /// </summary>
        /// <param name="p"></param>
        public void ChargerProjet(Projet p)
        {
            Projet = p;
        }
        /// <summary>
        /// Retourne une chaine de caractère décrivant les durées de travail réalisé et restante de chaque personne
        /// travaillant sur la version indiquée.
        /// </summary>
        /// <param name="vers"></param>
        /// <returns></returns>
        public string RetournerDuréesTravail(string vers)
        {
            string res = string.Format("Sur la version {0}, ", vers);
            var personnesVers = Projet.TachesProd.Where(t => (t.VersionProjet == vers)).Select(p => p.Personne).Distinct();

            foreach (var p in personnesVers)
                res += RetournerDuréesTravail(vers, p) + "\n";

            return res;
        }
        /// <summary>
        /// Retourne une chaine de caractère décrivant le nombre de jour et le pourcentage d'avance ou de retard sur la
        /// version indiquée
        /// </summary>
        /// <param name="vers"></param>
        /// <returns></returns>
        public string RetournerDuréesVersion(string vers)
        {
            var tachesVers = Projet.TachesProd.Where(t => (t.VersionProjet == vers));

            int duréeRéaliséeTot = tachesVers.Sum(t => t.DuréeRéalisée);
            int duréePrévueTot = tachesVers.Sum(t => t.DuréePrévue);
            int diff = duréePrévueTot - duréeRéaliséeTot;

            return string.Format("Sur la version {0}, la durée de travail réalisé a fini {1}j {2} la durée prévue, ce qui représente un pourcentage proche de {3}%.",
                vers, Math.Abs(diff), diff < 0 ? "avant" : "après", diff * 100 / duréeRéaliséeTot);

        }
        /// <summary>
        /// Retourne une chaine de caractère décrivant la durée total de travail réalisé sur chaque activité
        /// de la version indiquée
        /// </summary>
        /// <param name="vers"></param>
        /// <returns></returns>
        public string RetournerDuréesActivitésVersion(string vers)
        {
            var activitésVers = Projet.TachesProd.Where(t => (t.VersionProjet == vers));
            var activités = activitésVers.Select(a => a.Activité).Distinct();
            int durée;
            string res = string.Format("Quelques durées de travail réalisées sur la version {0} :\n", vers);
            foreach (var a in activités)
            {
                durée = activitésVers.Where(av => av.Activité == a).Sum(d => d.DuréeRéalisée);

                switch (a)
                {
                    case ActivitésProd.DBE:
                        res += string.Format("\t - Définition des besoins : {0}j\n", durée);
                        break;
                    case ActivitésProd.ARF:
                        res += string.Format("\t - Architecture fonctionnelle : {0}j\n", durée);
                        break;
                    case ActivitésProd.ANF:
                        res += string.Format("\t - Analyse fonctionnelle : {0}j\n", durée);
                        break;
                    case ActivitésProd.DES:
                        res += string.Format("\t - Design : {0}j\n", durée);
                        break;
                    case ActivitésProd.INF:
                        res += string.Format("\t - Infographie : {0}j\n", durée);
                        break;
                    case ActivitésProd.ART:
                        res += string.Format("\t - Architecture technique : {0}j\n", durée);
                        break;
                    case ActivitésProd.ANT:
                        res += string.Format("\t - Analyse technique : {0}j\n", durée);
                        break;
                    case ActivitésProd.DEV:
                        res += string.Format("\t - Développement : {0}j\n", durée);
                        break;
                    case ActivitésProd.RPT:
                        res += string.Format("\t - Rédaction de plan de test : {0}j\n", durée);
                        break;
                    case ActivitésProd.TES:
                        res += string.Format("\t - Test : {0}j\n", durée);
                        break;
                    case ActivitésProd.GDP:
                        res += string.Format("\t - Gestion de projet : {0}j\n", durée);
                        break;
                    default:
                        break;
                }
            }

            return res;
        }
        /// <summary>
        /// Retourne une chaine de caractère contenant les informations de toutes les taches annexes existantes
        /// </summary>
        /// <returns></returns>
        public string RetournerInfosTachesAnnexes()
        {
            string res = string.Empty;

            foreach (var a in Projet.TachesAnnexes)
                res += a.Value.ToString() + "\n";

            return res;
        }
        public string RetournerCumulTempsMoisTachesAnnexes()
        {
            // TODO : à implémenter
            return "";
        }
        #endregion
    }
}
