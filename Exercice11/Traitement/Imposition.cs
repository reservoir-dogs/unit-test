using Framework.Error;
using Modele;
using Ressource.Properties;

namespace Traitement
{
    public class Imposition
    {
        public decimal CalculerImpotSurBeneficePME(int montantCA, int annee)
        {
            var result = default(decimal);

            if (montantCA >= 0)
            {
                result += (montantCA > 38120 ? 38120 : montantCA) * 15 / 100;
                switch (annee)
                {
                    case 2018:
                        if (montantCA > 38120) result += (montantCA > 500000 ? 500000 - 38120 : montantCA - 38120) * (decimal)28 / 100;
                        if (montantCA > 500000) result += (montantCA - 500000) * (decimal)33.33 / 100;
                        break;
                    case 2019:
                        if (montantCA > 38120) result += (montantCA > 500000 ? 500000 - 38120 : montantCA - 38120) * (decimal)28 / 100;
                        if (montantCA > 500000) result += (montantCA - 500000) * (decimal)31 / 100;
                        break;
                    case 2020:
                        if (montantCA > 38120) result += (montantCA - 38120) * (decimal)28 / 100;
                        break;
                    case 2021:
                        if (montantCA > 38120) result += (montantCA - 38120) * (decimal)26.5 / 100;
                        break;
                    case 2022:
                        if (montantCA > 38120) result += (montantCA - 38120) * (decimal)25 / 100;
                        break;
                    default:
                        throw new BusinessException(string.Format(Resources.MauvaiseAnneeImposition, annee));
                }
            }

            return result;
        }

        public Impot CalculerImpotSurBeneficePMEv2(int montantCA, int annee)
        {
            var result = new Impot
            {
                MontantCA = montantCA,
                Annee = annee,
                Montant = CalculerImpotSurBeneficePME(montantCA, annee)
            };

            return result;
        }
    }
}
