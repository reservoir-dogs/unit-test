using Modele;
using System;
using System.Linq;

namespace Donnee
{
    public class Seeder
    {
        private readonly ApplicationDbContext applicationDbContext;

        public Seeder(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Seed()
        {
            if (!applicationDbContext.Dossiers.Any())
            {
                var dossier1 = new Dossier { Nom = "Hernandez", DateCreation = new DateTime(2018, 7, 1) };
                applicationDbContext.Add(dossier1);

                var dossier2 = new Dossier { Nom = "Bigot", DateCreation = new DateTime(2018, 7, 5) };
                applicationDbContext.Add(dossier2);

                applicationDbContext.SaveChanges();
            }
        }
    }
}
