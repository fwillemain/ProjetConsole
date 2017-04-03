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
        private List<TacheProd> _tachesProd;
        private SortedDictionary<int, TacheAnnexe> _tachesAnnexes;
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
                throw new NotImplementedException();
                //TODO : Projet::AjouterTacheAnnexe, gérer si code déjà existant
            }
        }
        #endregion

        #region Méthodes publiques
        public void AjouterTache(Tache t)
        {
            if (t is TacheProd)
                AjouterTacheProd(((TacheProd)t));
            else
            {
                TacheAnnexe ta = (TacheAnnexe)t;
                AjouterTacheAnnexe(ta.Code, ta);
            }
        }

        public string RenvoyerStatutTache(int code)
        {
            return _tachesProd[code - 1].RenvoyerStatut();
        }
        #endregion
    }
}
