using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Overview
{
    class Program
    {
        static void Main(string[] args)
        {
            Projet genomica = DAL.ChargerProjet(@"D:\ProjetConsole\ProjetConsole\Job Overview\Feuille de calcul dans D  ProjetConsole Projet JobOverview - 1.txt");

            Results resultats = new Results(genomica);
            Console.WriteLine(resultats.RetournerDuréesTravail("2.00"));

            Console.WriteLine();
            Console.WriteLine(resultats.RetournerDuréesVersion("1.00"));

            Console.WriteLine();
            Console.WriteLine(resultats.RetournerDuréesActivitésVersion("2.00"));

            Console.WriteLine();

            string réponse;
            bool continuer = true;
            while (continuer)
            {
                continuer = false;
                Console.WriteLine("Voulez-vous saisir des activités annexes? (O/N)");
                réponse = Console.ReadLine().ToLower();
                if (réponse == "o")
                {
                    SaisirDonnéesTacheAnnexe(genomica);
                    continuer = true;
                }
                else if (réponse != "n")
                    continuer = true;
            }

            Console.Clear();
            Console.WriteLine(resultats.RetournerInfosTachesAnnexes());

            // TODO : Main, gérer la demande d'un nouveau CodeTache si il existe déjà
            Console.ReadKey();

        }

        public static void SaisirDonnéesTacheAnnexe(Projet p)
        {
            Console.WriteLine("Saisir les informations suivantes :\n\t- un numéro CodeTache (doit être unique)");
            int codeTache;
            while (!int.TryParse(Console.ReadLine(), out codeTache))
                Console.WriteLine("Mauvaise saisie.");

            Console.WriteLine("\t- Libellé :");
            string libellé = Console.ReadLine();

            Console.WriteLine("\t- Code personne (peut être spécifié plus tard) :");
            string codePers = Console.ReadLine();

            Console.WriteLine("\t- Date de début au format DD/MM/YYYY ou \"Aucune\" (si aucune : aujourd'hui) :");
            DateTime dateDéb;
            string rep = Console.ReadLine();
            bool boolDateDéb;
            while (!(boolDateDéb = DateTime.TryParse(rep, out dateDéb)) && rep != "Aucune")
            {
                Console.WriteLine("Saisie invalide");
                rep = Console.ReadLine();
            }

            bool boolDateFin = false;
            DateTime dateFin = DateTime.MaxValue;
            if (boolDateDéb)
            {
                Console.WriteLine("\t- Date de fin au format DD/MM/YYYY ou \"Aucune\" (si aucune, la tache n'a pas de date de fin) :");
                
                rep = Console.ReadLine();
                while (!(boolDateFin = DateTime.TryParse(rep, out dateFin)) && rep != "Aucune")
                {
                    Console.WriteLine("Saisie invalide");
                    rep = Console.ReadLine();
                }
            }
            // TODO (optionnel) : gérer si DateFin < DateDébut

            try
            {
                TacheAnnexe tacheA = new TacheAnnexe(codeTache, libellé, codePers); ;
                if (boolDateDéb)
                    if (boolDateFin)
                    {
                        tacheA = new TacheAnnexe(codeTache, libellé, dateDéb, dateFin, codePers);
                    }
                    else
                        tacheA = new TacheAnnexe(codeTache, libellé, dateDéb, codePers);
                p.AjouterTache(tacheA);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("La tache n'a pas pu être ajoutée.");
            }
        }

    }

}


