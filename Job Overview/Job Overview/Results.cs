﻿using System;
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
        public void ChargerRésultats(Projet p)
        {
            Projet = p;
        }

        public string RetournerDuréesTravail(string vers)
        {
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
            string res = string.Empty;
            foreach (var a in activités)
            {
                durée = activitésVers.Where(av => av.Activité == a).Sum(d => d.DuréeRéalisée);

                switch (a)
                {
                    case Activités.DBE:

                        break;
                    case Activités.ARF:
                        break;
                    case Activités.ANF:
              //          string += string.Format("Analyse Fonctionnelle : {0}j\n", durée);
                        break;
                    case Activités.DES:
                        break;
                    case Activités.INF:
                        break;
                    case Activités.ART:
                        break;
                    case Activités.ANT:
                        break;
                    case Activités.DEV:
                        break;
                    case Activités.RPT:
                        break;
                    case Activités.TES:
                        break;
                    case Activités.GDP:
                        break;
                    default:
                        break;
                }
            }

            return "";
        }
        #endregion
    }
}