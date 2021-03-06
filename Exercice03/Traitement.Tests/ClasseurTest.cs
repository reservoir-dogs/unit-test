﻿using Framework.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Traitement.Tests
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
        public void Nettoyer_Nominal_Succes()
        {
            // Initialisation
            var dossiers = new List<Dossier> {
                new Dossier{ Nom = "un dossier quelconque"},
                new Dossier { Nom = "01-02-2017 - Dossier Hernandez" }
            };
            var attendu = new List<Dossier> {
                new Dossier { Nom = "01-02-2017 - Dossier Hernandez" }
            };

            // Execution
            var actuel = cible.Nettoyer(dossiers);

            // Vérification
            Verify(attendu, actuel);
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
            Verify(attendu, actuel);
        }

        private static void Verify(IList<Dossier> attendu, IList<Dossier> actuel)
        {
            Assert.That.AreDeepEqual(attendu, actuel);
        }
    }
}
