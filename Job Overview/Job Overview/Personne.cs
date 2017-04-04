using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    public enum Métiers
    {
        ANA,
        CDP,
        DEV,
        DES,
        TES
    }

    public struct Personne
    {
        #region Propriétés
        public string Code { get; }
        public string Prénom { get; }
        public string Nom { get; }
        public Métiers Métier { get; }
        #endregion

        #region Constructeurs
        public Personne(string code, string prénom, string nom, Métiers métier)
        {
            Code = code;
            Prénom = prénom;
            Nom = nom;
            Métier = métier;
        }
        #endregion

        #region Opérateurs
        /// <summary>
        /// Retourne vrai si les deux personnes ont le même nom et prénom
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        static public bool operator ==(Personne p1, Personne p2)
        {
            return p1.Nom == p2.Nom && p1.Prénom == p2.Prénom;
        }
        /// <summary>
        /// Retourne vrai si les deux personnes ont un nom et/ou un prénom différent
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        static public bool operator !=(Personne p1, Personne p2)
        {
            return p1.Nom != p2.Nom || p1.Prénom != p2.Prénom;
        }
        #endregion

        #region Méthodes publiques
        public override string ToString()
        {
            //TODO (optionnel) : Personne::ToString() gérer un affichage plus complet
            return string.Format("{0} {1}", Prénom, Nom);
        }
        #endregion
    }
}
