using Donnee;
using Framework.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Transferts;

namespace Web.Tests.Solution
{
    [TestClass]
    public class DossierControllerTest : TestIntegration<Startup, ApplicationDbContext>
    {
        private readonly HttpClient client;

        public DossierControllerTest() : base()
        {
            client = this.CreateClient();
        }

        [TestMethod]
        public async Task Lister_Tout_Succes()
        {
            // Initialisation
            var dossier1 = new Dossier { Nom = "Hernandez", DateCreation = new DateTime(2018, 7, 1) };
            DbContext.Add(dossier1);
            var dossier2 = new Dossier { Nom = "Bigot", DateCreation = new DateTime(2018, 7, 5) };
            DbContext.Add(dossier2);
            DbContext.SaveChanges();

            var expect = new List<DossierTransfert> {
                    new DossierTransfert{Id = dossier1.Id.Value, Nom = "Hernandez", DateCreation = new DateTime(2018,7,1) },
                    new DossierTransfert{Id = dossier2.Id.Value, Nom = "Bigot", DateCreation = new DateTime(2018,7,5) },
                };

            // Exécution
            var response = await client.GetAsync("api/dossiers");

            var actuel = JsonConvert.DeserializeObject<IList<DossierTransfert>>(await response.Content.ReadAsStringAsync());

            // Vérification
            Assert.That.AreDeepEqual(expect, actuel);
        }
    }
}
