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
        public Personne( string code, string prénom, string nom, Métiers métier )
        {
            Code = code;
            Prénom = prénom;
            Nom = nom;
            Métier = métier;
        }
        #endregion

        static public bool operator ==(Personne p1, Personne p2)
        {
            return p1.Nom == p2.Nom && p1.Prénom == p2.Prénom;
        }

        static public bool operator !=(Personne p1, Personne p2)
        {
            return p1.Nom != p2.Nom || p1.Prénom != p2.Prénom;
        }

    }
}
