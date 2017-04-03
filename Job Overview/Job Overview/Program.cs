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

            // Console.WriteLine(genomica.RenvoyerStatutTache(138));

            Results resultats = new Results(genomica);

            Console.WriteLine(resultats.RetournerDuréesTravail("2.00"));

            Console.WriteLine();
            Console.WriteLine(resultats.RetournerDuréesVersion("1.00"));

            Console.WriteLine();
            Console.WriteLine(resultats.RetournerDuréesActivitésVersion("2.00"));

            Console.ReadKey();
        }
    }
}
