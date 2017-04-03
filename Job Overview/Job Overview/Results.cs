using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    public class Results
    {
        #region Champs privés

        #endregion

        #region Propriétés
        public Projet Projet { get; private set; }
        #endregion

        #region Constructeurs
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

            return string.Format("Sur la version {0}, {1} {2} a réalisé {3} jours de travail, et il lui reste {4} jours de planifiés.",
                vers, p.Prénom, p.Nom, dReal, dRest);
        }
        #endregion

        #region Méthodes publiques
        public void ChargerProjet(Projet p)
        {
            Projet = p;
        }

        public string RetournerDuréesTravail(string vers)
        {
            //TODO : Results::Retourner... enlever le "version 2.00 partout"
            string res = string.Empty;
            var personnesVers = Projet.TachesProd.Where(t => (t.VersionProjet == vers)).Select(p => p.Personne).Distinct();

            foreach (var p in personnesVers)
                res += RetournerDuréesTravail(vers, p) + "\n";

            return res;
        }
        public string RetournerDuréesVersion(string vers)
        {
            var tachesVers = Projet.TachesProd.Where(t => (t.VersionProjet == vers));

            int duréeRéaliséeTot = tachesVers.Sum(t => t.DuréeRéalisée);
            int duréePrévueTot = tachesVers.Sum(t => t.DuréePrévue);
            int diff = duréePrévueTot - duréeRéaliséeTot;

            return string.Format("Sur la version {0}, la durée de travail réalisé a fini {1}j {2} la durée prévue, ce qui représente un pourcentage proche de {3}%.",
                vers, Math.Abs(diff), diff < 0 ? "avant" : "après", diff * 100 / duréeRéaliséeTot);

        }
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
                    case Activités.DBE:
                        res += string.Format("\t - Définition des besoins : {0}j\n", durée);
                        break;
                    case Activités.ARF:
                        res += string.Format("\t - Architecture fonctionnelle : {0}j\n", durée);
                        break;
                    case Activités.ANF:
                        res += string.Format("\t - Analyse fonctionnelle : {0}j\n", durée);
                        break;
                    case Activités.DES:
                        res += string.Format("\t - Design : {0}j\n", durée);
                        break;
                    case Activités.INF:
                        res += string.Format("\t - Infographie : {0}j\n", durée);
                        break;
                    case Activités.ART:
                        res += string.Format("\t - Architecture technique : {0}j\n", durée);
                        break;
                    case Activités.ANT:
                        res += string.Format("\t - Analyse technique : {0}j\n", durée);
                        break;
                    case Activités.DEV:
                        res += string.Format("\t - Développement : {0}j\n", durée);
                        break;
                    case Activités.RPT:
                        res += string.Format("\t - Rédaction de plan de test : {0}j\n", durée);
                        break;
                    case Activités.TES:
                        res += string.Format("\t - Test : {0}j\n", durée);
                        break;
                    case Activités.GDP:
                        res += string.Format("\t - Gestion de projet : {0}j\n", durée);
                        break;
                    default:
                        break;
                }
            }

            return res;
        }
        #endregion
    }
}
