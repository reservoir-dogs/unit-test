using Donnee;
using FluentAssertions;
using Framework.Error;
using Framework.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using Moq;
using Ressource.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Traitement.Solution;

namespace Traitement.Tests.Solution
{
    public class ClasseurTest : IDisposable
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IDossierRepository> moqDossierRepository;
        private readonly Mock<IFichierRepository> moqFichierRepository;
        private static Stopwatch stopWatch;
        private readonly Classeur cible;
        private readonly Mock<TimeProvider> moqTimeProvider;

        public ClasseurTest()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            moqDossierRepository = mockRepository.Create<IDossierRepository>();
            moqFichierRepository = mockRepository.Create<IFichierRepository>();
            cible = new Classeur(moqDossierRepository.Object, moqFichierRepository.Object);

            moqTimeProvider = mockRepository.Create<TimeProvider>();
            TimeProvider.Current = moqTimeProvider.Object;
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

        [TestMethod]
        public void Creer_Nominal_Succes()
        {
            // Initialisation
            var nomDossier = "01-02-2017 - Dossier Hernandez";
            var fichiers = new List<string> { "Proposition", "Devis" };
            var attendu = new Dossier
            {
                Id = 1,
                Nom = "01-02-2017 - Dossier Hernandez",
                DateCreation = new DateTime(2018, 7, 1),
                Fichiers = new List<Fichier>{
                    new Fichier{ Id = 10, Nom = "Proposition", DateCreation = new DateTime(2018, 7, 1)},
                    new Fichier{ Id = 20, Nom = "Devis", DateCreation = new DateTime(2018, 7, 1)},
                }
            };

            moqDossierRepository.Setup(x => x.Enregistrer(It.IsAny<Dossier>())).Callback<Dossier>(x => x.Id = 1);
            moqTimeProvider.SetupGet(x => x.UtcNow).Returns(new DateTime(2018, 7, 1));
            moqFichierRepository.Setup(x => x.Enregistrer(It.Is<Fichier>(f => f.Nom == "Proposition"))).Callback<Fichier>(x => x.Id = 10);
            moqFichierRepository.Setup(x => x.Enregistrer(It.Is<Fichier>(f => f.Nom == "Devis"))).Callback<Fichier>(x => x.Id = 20);

            // Execution
            var actuel = cible.Creer(nomDossier, fichiers);

            // Vérification
            Verify(attendu, actuel);
        }

        [TestMethod]
        public void Creer_MauvaisFormatNomDossier_Erreur()
        {
            // Initialisation
            var nomDossier = "Dossier Hernandez";
            var fichiers = new List<string> { "Proposition", "Devis" };
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
            var fichiers = new List<string>();
            var attendu = Resources.MessageDossierVide;

            // Execution
            Action actuel = () => cible.Creer(nomDossier, fichiers);

            // Vérification
            actuel.Should().Throw<BusinessException>(attendu).And.Message.Should().Be(Resources.MessageDossierVide);
        }

        private static void Verify(IList<Dossier> attendu, IList<Dossier> actuel)
        {
            actuel.Select(x => x.Nom).Should().Equal(attendu.Select(x => x.Nom));
        }

        private static void Verify(Dossier attendu, Dossier actuel)
        {
            actuel.Id.Should().Be(attendu.Id);
            actuel.Nom.Should().Be(attendu.Nom);
            actuel.DateCreation.Should().Be(attendu.DateCreation);
            actuel.Fichiers.Select(x => x.Id).Should().Equal(attendu.Fichiers.Select(x => x.Id));
            actuel.Fichiers.Select(x => x.Nom).Should().Equal(attendu.Fichiers.Select(x => x.Nom));
            actuel.Fichiers.Select(x => x.DateCreation).Should().Equal(attendu.Fichiers.Select(x => x.DateCreation));
        }

        [TestMethod]
        public void Archiver_Nominal_Succes()
        {
            // Initialisation
            var dossier = new Dossier { SiArchive = false };

            var dossierSetup = moqDossierRepository.Setup(x => x.Enregistrer(It.IsAny<Dossier>()));

            // Execution
            cible.Archiver(dossier);

            // Vérification
            dossierSetup.Callback<Dossier>(x => x.SiArchive.Should().BeTrue());
        }

        public void Dispose()
        {
            mockRepository.VerifyAll();
        }
    }
}
