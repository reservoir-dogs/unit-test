using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using System.Collections.Generic;

namespace Traitement.Tests
{
    [TestClass]
    public class ClasseurTest
    {
        [TestMethod]
        public void Nettoyer_Nominal_Succes()
        {
            // Initialisation
            var cible = new Classeur();
            var dossiers = new List<Dossier> {
                new Dossier { Nom = "un dossier quelconque"},
                new Dossier { Nom = "01-02-2017 - Dossier Hernandez" }
            };
            var attendu = new List<Dossier> {
                new Dossier { Nom = "01-02-2017 - Dossier Hernandez" }
            };

            // Execution
            var actuel = cible.Nettoyer(dossiers);

            // Vérification
            Assert.That.AreDeepEqual(attendu, actuel);
        }

        [TestMethod]
        public void Trier_Nominal_Succes()
        {
            // Initialisation
            var cible = new Classeur();
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
    }
}
