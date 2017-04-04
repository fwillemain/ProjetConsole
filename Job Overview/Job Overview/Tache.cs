using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    [Flags]
    public enum Activités
    {
        Aucune = 0,
        DBE = 1,
        ARF = 2,
        ANF = 4,
        DES = 8,
        INF = 16,
        ART = 32,
        ANT = 64,
        DEV = 128,
        RPT = 256,
        TES = 512,
        GDP = 1024
    }

    public abstract class Tache
    {
        #region Propriétés
        public int NumTache { get; }
        public string LibTache { get; }
        public DateTime DateDébut { get; protected set; }
        #endregion

        #region Constructeurs
        public Tache(int numTache, string libTache, DateTime dateDébut)
        {
            NumTache = numTache;
            LibTache = libTache;
            DateDébut = dateDébut;
        }
        #endregion

    }

    public class TacheProd : Tache
    {

        #region Propriétés
        public string VersionProjet { get; }
        public Personne Personne { get; }
        public Activités Activité { get; }
        public int DuréePrévue { get; }
        public int DuréeRéalisée { get; set; }
        public int DuréeRestante { get; }
        #endregion

        #region Constructeurs
        public TacheProd(int numTache, string vProj, Personne pers, Activités act, string lib, DateTime dateDeb,
                            int duréePrev, int duréeReal, int duréeRest) : base(numTache, lib, dateDeb)
        {
            if (EstCompétent(pers, act))
            {
                VersionProjet = vProj;
                Personne = pers;
                Activité = act;
                DuréePrévue = duréePrev;
                DuréeRéalisée = duréeReal;
                DuréeRestante = duréeRest;
            }
            else
            {
                // TODO (optionnel) : TacheProd::TacheProd(...) améliorer message exception
                throw new ArgumentException(string.Format("{0} {1} n'est pas habilitée en tant que {2} à faire du {3}.", pers.Prénom, pers.Nom, pers.Métier, act));
            }
        }
        #endregion

        #region Méthodes privées
        private bool EstCompétent(Personne p, Activités a)
        {
            Activités masqueANA, masqueCDP, masqueDEV, masqueDES, masqueTES;
            masqueANA = Activités.DBE | Activités.ARF | Activités.ANF;
            masqueCDP = Activités.ARF | Activités.ANF | Activités.ART | Activités.TES | Activités.GDP;
            masqueDEV = Activités.ANF | Activités.ART | Activités.ANT | Activités.DEV | Activités.TES;
            masqueDES = Activités.ANF | Activités.DES | Activités.INF;
            masqueTES = Activités.RPT | Activités.TES;

            switch(p.Métier)
            {
                case Métiers.ANA:
                    return (masqueANA & a) == a;
                case Métiers.CDP:
                    return (masqueCDP & a) == a;
                case Métiers.DEV:
                    return (masqueDEV & a) == a;
                case Métiers.DES:
                    return (masqueDES & a) == a;
                case Métiers.TES:
                    return (masqueTES & a) == a;
                default:
                    return false;
            }
        }
        #endregion

        #region Méthodes publiques
        public override string ToString()
        {
            return string.Format("T.Prod ({0}) : {1} (v.{2}), a débuté le {3:dd/MM/yyyy}, estimation de {4} jours nécessaire au total dont {5} jours effectifs et {6} restants",
                NumTache, LibTache, VersionProjet, DateDébut, DuréePrévue, DuréeRéalisée, DuréeRestante);
        }
        #endregion
    }

    public class TacheAnnexe : Tache
    {

        #region Propriétés
        public DateTime DateFin { get; }
        public bool EstFinie
        {
            get { return DateFin <= DateTime.Today; }
        }
        public List<Personne> ListePersonnes { get; }
        #endregion

        #region Constructeurs
        public TacheAnnexe(int code, string libellé, string codePers = null) : base(code, libellé, DateTime.Today)
        {
            ListePersonnes = new List<Personne>();
            DateFin = DateTime.MaxValue;

            if (!string.IsNullOrEmpty(codePers)) {
                if (DAL.DicoEmployés.ContainsKey(codePers))
                    ListePersonnes.Add(DAL.DicoEmployés[codePers]);
                else
                    throw new ArgumentException(string.Format("Le code personnel \"{0}\" n'existe pas.", codePers));
            }
        }
        public TacheAnnexe(int code, string libellé, DateTime dateDébut, string codePers = null) : this(code, libellé, codePers)
        {
            DateDébut = dateDébut;
        }
        public TacheAnnexe(int code, string libellé, DateTime dateDébut, DateTime dateFin, string codePers = null) : this(code, libellé, dateDébut, codePers)
        {
            DateFin = dateFin;
        }
        #endregion

        #region Méthodes publiques
        public void AjouterPersonne(Personne p)
        {
            ListePersonnes.Add(p);
        }

        public override string ToString()
        {
            string stringListePersonne = string.Empty;
            foreach (var p in ListePersonnes)
                stringListePersonne += p.ToString() + " ";

            return string.Format("Tache annexe N°{0} : {1}. Commence le {2:dd/MM/yyyy} termine le {3}. Personnes participantes {4}",
                                    NumTache, LibTache, DateDébut, (DateFin < DateTime.MaxValue) ? DateFin.ToString("dd/MM/yyyy") : "{Non spécifié}", stringListePersonne);
        }
        #endregion
    }
}
