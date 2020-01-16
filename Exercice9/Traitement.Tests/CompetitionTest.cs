using Donnee;
using FluentAssertions;
using Modele;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Traitement.Tests
{
    [TestClass]
    public class CompetitionTest : IDisposable
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IRencontreRepository> rencontreRepositoryMock;
        private readonly Competition cible;

        public CompetitionTest()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            rencontreRepositoryMock = mockRepository.Create<IRencontreRepository>();
            cible = new Competition(rencontreRepositoryMock.Object);
        }

        public void Dispose()
        {
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Commencer_Nominalv1_Succes()
        {
            // Initialisation
            var rencontre = new Rencontre { Id = 1, Nom = "France vs Brésil", Date = new DateTime(2018, 7, 10) };
            var attendu = new Rencontre { Id = 1, Nom = "France vs Brésil", Date = new DateTime(2018, 7, 10), SiCommence = true };
            rencontreRepositoryMock.Setup(x => x.Enregistrer(It.IsAny<Rencontre>())).Returns(attendu);

            // Exécution
            var actuel = cible.Commencer(rencontre);

            // Vérification
            actuel.Should().BeOfType<Rencontre>();
            actuel.Id.Should().Be(attendu.Id);
            actuel.Nom.Should().Be(attendu.Nom);
            actuel.SiCommence.Should().Be(attendu.SiCommence);
        }

        [TestMethod]
        public void Commencer_Nominalv2_Succes()
        {
            // Initialisation
            var rencontre = new Rencontre { Id = 1, Nom = "France vs Brésil" };
            var attendu = new Rencontre { Id = 1, Nom = "France vs Brésil", SiCommence = true };
            rencontreRepositoryMock.Setup(x => x.Enregistrer(It.IsAny<Rencontre>())).Returns<Rencontre>(x => x);

            // Exécution
            var actuel = cible.Commencer(rencontre);

            // Vérification
            actuel.Should().Be(attendu);
        }

        [TestMethod]
        public void ListerRencontreEnsemble_Nominal_Test()
        {
            // Initialisation
            var arbitre = new Arbitre { Id = 1, Nom = "Damir Skomina" };
            var joueur = new Joueur { Id = 1, Nom = "Emil Forsberg" };

            rencontreRepositoryMock.Setup(x => x.ListerEnsemble(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new List<Rencontre> {
                    new Rencontre{Nom = "Suède vs Danemark", Date = new DateTime(2018,7,3)},
                    new Rencontre{Nom = "Suède vs Allemagne", Date = new DateTime(2014,7,5)},
                });

            var attendu = new List<string>{
                    "Suède vs Danemark le mardi 3 juillet 2018",
                    "Suède vs Allemagne le samedi 5 juillet 2014",
                };

            // Exécution
            var actuel = cible.ListerRencontreEnsemble(arbitre, joueur);

            // Vérification
            actuel.Should().BeEquivalentTo(attendu);
        }
    }
}
