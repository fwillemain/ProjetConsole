using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    [Flags]
    public enum ActivitésProd
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
    public enum ActivitésAnnexes
    {
        Indéfinie,
        AideCollègue,
        AppuiAutreService,
        Réunion,
        TravailDDP,
        Event
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
        public Personne Personne { get; }   // Personne travaillant sur cette tache (unique)
        public ActivitésProd Activité { get; }  // Activité liée à la tache
        public int DuréePrévue { get; }
        public int DuréeRéalisée { get; set; }
        public int DuréeRestante { get; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de TacheProd, renvoi une exception de type ArgumentException si 'pers' n'est pas apte à faire 'act'
        /// </summary>
        /// <param name="numTache"></param>
        /// <param name="vProj"></param>
        /// <param name="pers"></param>
        /// <param name="act"></param>
        /// <param name="lib"></param>
        /// <param name="dateDeb"></param>
        /// <param name="duréePrev"></param>
        /// <param name="duréeReal"></param>
        /// <param name="duréeRest"></param>
        public TacheProd(int numTache, string vProj, Personne pers, ActivitésProd act, string lib, DateTime dateDeb,
                            int duréePrev, int duréeReal, int duréeRest) : base(numTache, lib, dateDeb)
        {
            // Retourne vrai si la Personne pers est apte à réaliser l'Activité act
            // Si oui, initialiser la tache
            if (EstCompétent(pers, act))
            {
                VersionProjet = vProj;
                Personne = pers;
                Activité = act;
                DuréePrévue = duréePrev;
                DuréeRéalisée = duréeReal;
                DuréeRestante = duréeRest;
            }
            else // Si non, renvoyer une exception qui sera gérée lors de l'appel du constructeur
            {
                // TODO (optionnel) : TacheProd::TacheProd(...) améliorer message exception
                throw new ArgumentException(string.Format("{0} {1} n'est pas habilitée en tant que {2} à faire du {3}.", pers.Prénom, pers.Nom, pers.Métier, act));
            }
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Retourne vrai si la personne est apte à réaliser l'activité
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        private bool EstCompétent(Personne p, ActivitésProd a)
        {
            ActivitésProd masqueANA, masqueCDP, masqueDEV, masqueDES, masqueTES;
            masqueANA = ActivitésProd.DBE | ActivitésProd.ARF | ActivitésProd.ANF;
            masqueCDP = ActivitésProd.ARF | ActivitésProd.ANF | ActivitésProd.ART | ActivitésProd.TES | ActivitésProd.GDP;
            masqueDEV = ActivitésProd.ANF | ActivitésProd.ART | ActivitésProd.ANT | ActivitésProd.DEV | ActivitésProd.TES;
            masqueDES = ActivitésProd.ANF | ActivitésProd.DES | ActivitésProd.INF;
            masqueTES = ActivitésProd.RPT | ActivitésProd.TES;

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
        public List<Personne> ListePersonnes { get; }   // Personnes rattachées à la tache (possible d'en avoir plusieurs)
        public ActivitésAnnexes Activité { get; }  // Activité annexes rattachée à la tache
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de TacheAnnexe, renvoi une exception de type ArgumentException si le code personne ne correspond à aucun
        /// employé connu. Si l'activité annexe n'est pas connue elle sera initialisée à indéfini.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="libellé"></param>
        /// <param name="codePers">Optionnel : par défaut personne ne sera reliée à la tache</param>
        /// <param name="act">Optionnel : par défaut la tache sera une activité indéfinie</param>
        public TacheAnnexe(int code, string libellé, 
            string codePers = null, string act = null) : base(code, libellé, DateTime.Today)
        {
            ListePersonnes = new List<Personne>();
            DateFin = DateTime.MaxValue;
            Activité = ActivitésAnnexes.Indéfinie; // Valeur par défaut

            // Si codePers a été renseigné
            if (!string.IsNullOrEmpty(codePers))    
            {
                // Si le code correspond à un employé, ajouter la personne à la liste des personnes rattachée à la tache
                if (DAL.DicoEmployés.ContainsKey(codePers)) 
                    ListePersonnes.Add(DAL.DicoEmployés[codePers]);
                // Sinon retourner une exception qui sera gérée à l'appel du constructeur
                else
                    throw new ArgumentException(string.Format("Le code personnel \"{0}\" n'existe pas.", codePers));
            }

            // Si l'activité annexe a été renseignée
            if (!string.IsNullOrEmpty(act))
            {
                // Si l'activité annexe est connue, lier l'activité à la tache
                if (DAL.DicoActivitésAnnexes.ContainsKey(act))
                    Activité = DAL.DicoActivitésAnnexes[act];
            }
        }
        /// <summary>
        /// Constructeur de TacheAnnexe, renvoi une exception de type ArgumentException si le code personne ne correspond à aucun
        /// employé connu ou si l'activité annexe n'est pas connue.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="libellé"></param>
        /// <param name="dateDébut"></param>
        /// <param name="codePers"></param>
        /// <param name="act"></param>
        public TacheAnnexe(int code, string libellé, DateTime dateDébut, string codePers = null,
            string act = null) : this(code, libellé, codePers, act)
        {
            DateDébut = dateDébut;
        }
        /// <summary>
        /// Constructeur de TacheAnnexe, renvoi une exception de type ArgumentException si le code personne ne correspond à aucun
        /// employé connu ou si l'activité annexe n'est pas connue.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="libellé"></param>
        /// <param name="dateDébut"></param>
        /// <param name="dateFin"></param>
        /// <param name="codePers"></param>
        /// <param name="act"></param>
        public TacheAnnexe(int code, string libellé, DateTime dateDébut, DateTime dateFin,
            string codePers = null, string act = null) : this(code, libellé, dateDébut, codePers, act)
        {
            DateFin = dateFin;
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Ajoute une Personne p à la liste des personnes rattachées à la tache, ne fait rien si la personne est déjà rattachée
        /// </summary>
        /// <param name="p"></param>
        public void AjouterPersonne(Personne p)
        {
            if(!ListePersonnes.Contains(p))
                ListePersonnes.Add(p);
        }

        public override string ToString()
        {
            string stringListePersonne = string.Empty;
            foreach (var p in ListePersonnes)
                stringListePersonne += p.ToString() + " ";

            return string.Format("Tache annexe N°{0} : {1} ({2}). Commence le {3:dd/MM/yyyy} termine le {4}. Personnes participantes {5}",
                                    NumTache, LibTache, Activité, DateDébut, (DateFin < DateTime.MaxValue) ? DateFin.ToString("dd/MM/yyyy") : "{Non spécifié}", stringListePersonne);
        }
        #endregion
    }
}
