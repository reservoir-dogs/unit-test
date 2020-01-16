using FluentAssertions;
using Framework.Error;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Traitement.Tests
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
        public void CalculerImpotSurBeneficePME_BusinessException_Erreur(int montantCA, int annee, string attendu)
        {
            // Exécution
            Action actuel = () => cible.CalculerImpotSurBeneficePME(montantCA, annee);

            // Vérification
            actuel.Should().Throw<BusinessException>().And.Message.Should().Be(attendu);
        }
    }
}