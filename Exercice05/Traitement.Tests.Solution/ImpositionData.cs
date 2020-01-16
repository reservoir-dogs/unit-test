using Modele;
using Ressource.Properties;
using System.Collections.Generic;

namespace Traitement.Tests.Solution
{
    public class ImpositionData
    {
        public static IEnumerable<object[]> Lister()
        {
            return new List<object[]>
            {
                 new object[] { 10000, 2018, new Impot { MontantCA = 10000, Annee = 2018, Montant = 1500 } },
                 new object[] { 50000, 2018, new Impot { MontantCA = 50000, Annee = 2018, Montant = (decimal) (5718 + 3326.4) }},
                 new object[] { 1000000, 2018, new Impot { MontantCA = 1000000, Annee = 2018, Montant = (decimal) (5718 + 129326.4 + 166650) } },
                 new object[] { 10000, 2019, new Impot { MontantCA = 10000, Annee = 2019, Montant = 1500 } },
                 new object[] { 50000, 2019, new Impot { MontantCA = 50000, Annee = 2019, Montant = (decimal) (5718 + 3326.4) } },
                 new object[] { 1000000, 2019, new Impot { MontantCA = 1000000, Annee = 2019, Montant = (decimal) (5718 + 129326.4 + 155000) } },
                 new object[] { 10000, 2020, new Impot { MontantCA = 10000, Annee = 2020, Montant = 1500 } },
                 new object[] { 50000, 2020, new Impot { MontantCA = 50000, Annee = 2020, Montant = (decimal) (5718 + 3326.4) } },
                 new object[] { 1000000, 2020, new Impot { MontantCA = 1000000, Annee = 2020, Montant = (decimal) (5718 + 269326.4) } },
                 new object[] { 10000, 2021, new Impot { MontantCA = 10000, Annee = 2021, Montant = 1500 } },
                 new object[] { 50000, 2021, new Impot { MontantCA = 50000, Annee = 2021, Montant = (decimal) (5718 + 3148.2) } },
                 new object[] { 1000000, 2021, new Impot { MontantCA = 1000000, Annee = 2021, Montant = (decimal) (5718 + 254898.2) } },
                 new object[] { 10000, 2022, new Impot { MontantCA = 10000, Annee = 2022, Montant = 1500 } },
                 new object[] { 50000, 2022, new Impot { MontantCA = 50000, Annee = 2022, Montant = 5718 + 2970 } },
                 new object[] { 1000000, 2022, new Impot { MontantCA = 1000000, Annee = 2022, Montant = 5718 + 240470 } }
            };
        }

        public static IEnumerable<object[]> ListerException()
        {
            return new List<object[]>
            {
                new object[] {10000, 2010, string.Format(Resources.MauvaiseAnneeImposition,2010)},
                new object[] {10000, 2030, string.Format(Resources.MauvaiseAnneeImposition,2030)},
            };
        }
    }
}
