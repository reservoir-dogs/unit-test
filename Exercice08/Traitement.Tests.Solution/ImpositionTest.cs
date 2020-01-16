using FluentAssertions;
using Framework.Error;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using Ressource.Properties;
using System;
using System.Collections.Generic;
using Traitement.Solution;

namespace Traitement.Tests.Solution
{
    [TestClass]
    public class ImpositionTest
    {
        private readonly Imposition cible;

        public ImpositionTest()
        {
            cible = new Imposition();
        }

        [TestMethod]
        [DataRow(10000, 2018, 1500)]
        [DataRow(50000, 2018, 5718 + 3326.4)]
        [DataRow(1000000, 2018, 5718 + 129326.4 + 166650)]
        [DataRow(10000, 2019, 1500)]
        [DataRow(50000, 2019, 5718 + 3326.4)]
        [DataRow(1000000, 2019, 5718 + 129326.4 + 155000)]
        [DataRow(10000, 2020, 1500)]
        [DataRow(50000, 2020, 5718 + 3326.4)]
        [DataRow(1000000, 2020, 5718 + 269326.4)]
        [DataRow(10000, 2021, 1500)]
        [DataRow(50000, 2021, 5718 + 3148.2)]
        [DataRow(1000000, 2021, 5718 + 254898.2)]
        [DataRow(10000, 2022, 1500)]
        [DataRow(50000, 2022, 5718 + 2970)]
        [DataRow(1000000, 2022, 5718 + 240470)]
        public void CalculerImpotSurBeneficePME_Nominal_Succes(int montantCA, int annee, double attendu)
        {
            // Exécution
            var actuel = cible.CalculerImpotSurBeneficePME(montantCA, annee);

            // Vérification
            actuel.Should().Be(Convert.ToDecimal(attendu));
        }

        [TestMethod]
        [DynamicData(nameof(ImpositionData.ListerException), typeof(ImpositionData), DynamicDataSourceType.Method)]
        [DynamicData(nameof(ListerCalculImpositionBusinessException), DynamicDataSourceType.Method)]
        public void CalculerImpotSurBeneficePME_BusinessException_Erreur(int montantCA, int annee, string attendu)
        {
            // Exécution
            Action actuel = () => cible.CalculerImpotSurBeneficePME(montantCA, annee);

            // Vérification
            actuel.Should().Throw<BusinessException>().And.Message.Should().Be(attendu);
        }

        [TestMethod]
        [DynamicData(nameof(ImpositionData.Lister), typeof(ImpositionData), DynamicDataSourceType.Method)]
        [DynamicData(nameof(ListerCalculImpositionv2Nominal), DynamicDataSourceType.Method)]
        public void CalculerImpotSurBeneficePMEv2_Nominal_Succes(int montantCA, int annee, Impot attendu)
        {
            // Exécution
            var actuel = cible.CalculerImpotSurBeneficePMEv2(montantCA, annee);

            // Vérification
            actuel.MontantCA.Should().Be(attendu.MontantCA);
            actuel.Annee.Should().Be(attendu.Annee);
            actuel.Montant.Should().Be(attendu.Montant);
        }

        public static IEnumerable<object[]> ListerCalculImpositionBusinessException()
        {
            yield return new object[] { 10000, 2010, string.Format(Resources.MauvaiseAnneeImposition, 2010) };
            yield return new object[] { 10000, 2030, string.Format(Resources.MauvaiseAnneeImposition, 2030) };
        }

        public static IEnumerable<object[]> ListerCalculImpositionv2Nominal()
        {
            yield return new object[] { 10000, 2018, new Impot { MontantCA = 10000, Annee = 2018, Montant = 1500 } };
            yield return new object[] { 50000, 2018, new Impot { MontantCA = 50000, Annee = 2018, Montant = (decimal)(5718 + 3326.4) } };
            yield return new object[] { 1000000, 2018, new Impot { MontantCA = 1000000, Annee = 2018, Montant = (decimal)(5718 + 129326.4 + 166650) } };
            yield return new object[] { 10000, 2019, new Impot { MontantCA = 10000, Annee = 2019, Montant = (decimal)1500 } };
            yield return new object[] { 50000, 2019, new Impot { MontantCA = 50000, Annee = 2019, Montant = (decimal)(5718 + 3326.4) } };
            yield return new object[] { 1000000, 2019, new Impot { MontantCA = 1000000, Annee = 2019, Montant = (decimal)(5718 + 129326.4 + 155000) } };
            yield return new object[] { 10000, 2020, new Impot { MontantCA = 10000, Annee = 2020, Montant = 1500 } };
            yield return new object[] { 50000, 2020, new Impot { MontantCA = 50000, Annee = 2020, Montant = (decimal)(5718 + 3326.4) } };
            yield return new object[] { 1000000, 2020, new Impot { MontantCA = 1000000, Annee = 2020, Montant = (decimal)(5718 + 269326.4) } };
            yield return new object[] { 10000, 2021, new Impot { MontantCA = 10000, Annee = 2021, Montant = 1500 } };
            yield return new object[] { 50000, 2021, new Impot { MontantCA = 50000, Annee = 2021, Montant = (decimal)(5718 + 3148.2) } };
            yield return new object[] { 1000000, 2021, new Impot { MontantCA = 1000000, Annee = 2021, Montant = (decimal)(5718 + 254898.2) } };
            yield return new object[] { 10000, 2022, new Impot { MontantCA = 10000, Annee = 2022, Montant = 1500 } };
            yield return new object[] { 50000, 2022, new Impot { MontantCA = 50000, Annee = 2022, Montant = 5718 + 2970 } };
            yield return new object[] { 1000000, 2022, new Impot { MontantCA = 1000000, Annee = 2022, Montant = 5718 + 240470 } };
        }
    }
}