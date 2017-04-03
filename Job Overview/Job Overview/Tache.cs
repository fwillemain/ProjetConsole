using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    public enum Activités
    {
        DBE,
        ARF,
        ANF,
        DES,
        INF,
        ART,
        ANT,
        DEV,
        RPT,
        TES,
        GDP
    }

    public abstract class Tache
    {
        #region Champs privés

        #endregion

        #region Propriétés

        #endregion

        #region Constructeurs

        #endregion

        #region Méthodes privées

        #endregion

        #region Méthodes publiques

        #endregion
    }

    public class TacheProd : Tache
    {

        #region Propriétés
        public int NumTache { get; }
        public string VersionProjet { get; }
        public Personne Personne { get; }
        public Activités Activité { get; }
        public string LibTache { get; }
        public DateTime DateDébut { get; }
        public int DuréePrévue { get; }
        public int DuréeRéalisée { get; set; }
        public int DuréeRestante { get; }
        #endregion

        #region Constructeurs
        public TacheProd(int numTache, string vProj, Personne pers, Activités act, string lib, DateTime dateDeb,
                            int duréePrev, int duréeReal, int duréeRest) : base()
        {
            NumTache = numTache;
            VersionProjet = vProj;
            Personne = pers;
            Activité = act;
            LibTache = lib;
            DateDébut = dateDeb;
            DuréePrévue = duréePrev;
            DuréeRéalisée = duréeReal;
            DuréeRestante = duréeRest;
        }
        #endregion

        #region Méthodes privées

        #endregion

        #region Méthodes publiques
        public string RenvoyerStatut()
        {
            return string.Format("T.Prod ({0}) : {1} (v.{2}), a débuté le {3:dd/MM/yyyy}, estimation de {4} jours nécessaire au total dont {5} jours effectifs et {6} restants",
                NumTache, LibTache, VersionProjet, DateDébut, DuréePrévue, DuréeRéalisée, DuréeRestante);
        }
        #endregion
    }

    public class TacheAnnexe : Tache
    {
        #region Champs privés

        #endregion

        #region Propriétés
        public int Code { get; }
        #endregion

        #region Constructeurs
        public TacheAnnexe(int code) : base()
        {
            Code = code;
        }
        #endregion

        #region Méthodes privées

        #endregion

        #region Méthodes publiques

        #endregion
    }
}
