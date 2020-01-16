using FluentAssertions;
using Framework.Error;
using Framework.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using Ressource.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Traitement.Solution;

namespace Traitement.Tests.Solution
{
    [TestClass]
    public class ClasseurTest
    {
        private static Stopwatch stopWatch;
        private readonly Classeur cible;

        public ClasseurTest()
        {
            cible = new Classeur();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            using (var writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"Les tests ont duré : {stopWatch.Elapsed}");
                stopWatch.Stop();
            }
        }

        [TestMethod]
        public void Trier_Nominal_Succes()
        {
            // Initialisation
            var dossiers = new List<Dossier> {
                new Dossier { Nom = "01-03-2017 - Dossier Polivaro" },
                new Dossier { Nom = "01-02-2017 - Dossier Hernandez" }
            };
            var attendu = new List<Dossier> {
                new Dossier { Nom = "01-02-2017 - Dossier Hernandez" },
                new Dossier { Nom = "01-03-2017 - Dossier Polivaro" }
            };

            // Execution
            var actuel = cible.Trier(dossiers);

            // Vérification
            Assert.That.AreDeepEqual(attendu, actuel);
        }

        [TestMethod]
        public void Creer_Nominal_Succes()
        {
            // Initialisation
            var nomDossier = "01-02-2017 - Dossier Hernandez";
            var fichiers = new List<Fichier> {
                new Fichier{ Nom = "Proposition"},
                new Fichier{ Nom = "Devis"},
            };
            var attendu = new Dossier
            {
                Nom = "01-02-2017 - Dossier Hernandez",
                Fichiers = new List<Fichier>{
                    new Fichier{ Nom = "Proposition"},
                    new Fichier{ Nom = "Devis"},
                }
            };

            // Execution
            var actuel = cible.Creer(nomDossier, fichiers);

            // Vérification
            Assert.That.AreDeepEqual(attendu, actuel);
        }

        [TestMethod]
        public void Creer_MauvaisFormatNomDossier_Erreur()
        {
            // Initialisation
            var nomDossier = "Dossier Hernandez";
            var fichiers = new List<Fichier> {
                new Fichier{ Nom = "Proposition"},
                new Fichier{ Nom = "Devis"},
            };
            var attendu = Resources.MessageMauvaisFormatDossier;

            // Execution
            Action actuel = () => cible.Creer(nomDossier, fichiers);

            // Vérification
            actuel.Should().Throw<BusinessException>(attendu).And.Message.Should().Be(Resources.MessageMauvaisFormatDossier);
        }

        [TestMethod]
        public void Creer_DossierVide_Erreur()
        {
            // Initialisation
            var nomDossier = "01-02-2017 - Dossier Hernandez";
            var fichiers = new List<Fichier>();
            var attendu = Resources.MessageDossierVide;

            // Execution
            Action actuel = () => cible.Creer(nomDossier, fichiers);

            // Vérification
            actuel.Should().Throw<BusinessException>(attendu).And.Message.Should().Be(Resources.MessageDossierVide);
        }
    }
}
