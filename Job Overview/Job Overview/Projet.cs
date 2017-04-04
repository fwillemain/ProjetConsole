using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    public class Projet
    {
        #region Champs privés
        private List<TacheProd> _tachesProd;    // Liste des taches de production liées au projet
        private SortedDictionary<int, TacheAnnexe> _tachesAnnexes;      // Liste des taches annexes liées au projet (classé par identifiant
        #endregion

        #region Propriétés
        public List<TacheProd> TachesProd
        {
            get { return _tachesProd; }
        }
        public SortedDictionary<int, TacheAnnexe> TachesAnnexes
        {
            get { return _tachesAnnexes; }
        }
        #endregion

        #region Constructeurs
        public Projet()
        {
            _tachesProd = new List<TacheProd>();
            _tachesAnnexes = new SortedDictionary<int, TacheAnnexe>();
        }

        // TODO (optionnel) : Projet::Projet() éventuel ctor avec list de taches en paramètre
        #endregion

        #region Méthodes privées
        private void AjouterTacheProd(TacheProd t)
        {
            _tachesProd.Add(t);
        }

        private void AjouterTacheAnnexe(int code, TacheAnnexe t)
        {
            if (!_tachesAnnexes.ContainsKey(code))
                _tachesAnnexes.Add(code, t);
            else
            {
                throw new ArgumentException(string.Format("La tache avec le code {0} existe déjà.", code));
            }
        }
        
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Ajoute une tache (annexe ou de production) au projet
        /// </summary>
        /// <param name="t"></param>
        public void AjouterTache(Tache t)
        {
            if (t is TacheProd)
                AjouterTacheProd(((TacheProd)t));
            else
            {
                TacheAnnexe ta = (TacheAnnexe)t;
                AjouterTacheAnnexe(ta.NumTache, ta);
            }
        }
        #endregion
    }
}
