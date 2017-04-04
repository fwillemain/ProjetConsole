using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Job_Overview
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // Tester si une exception est levée lorsqu'une tache annexe est créée avec un code déjà existant
        public void TestAjouterTacheAnnexe()
        {
            Projet p = new Projet();
            p.AjouterTache(new TacheAnnexe(10, "coucou10"));
            // Ajout d'une tache annexe avec un code déjà existant
            p.AjouterTache(new TacheAnnexe(10, "coucou20"));
        }

         [TestMethod]
         // Création d'une tache annexe : vérifier que le cas où le code saisi n'est pas un entier, est bien géré
         public void TestSaisirDonnéesTacheAnnexe()
        {
            int code;
            string codeSaisi = "fsqfqsf";
            Assert.AreEqual(false, int.TryParse(codeSaisi, out code));
        }



    
    }
}
