using System.Collections.Generic;
using System.Linq;
using Modele;

namespace Donnee
{
    public class DossierRepository : IDossierRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DossierRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Enregistrer(Dossier dossier)
        {
            throw new System.NotImplementedException();
        }

        public IList<Dossier> Lister()
        {
            var result  = applicationDbContext.Dossiers
                .Where(x => !x.SiArchive)
                .ToList();

            return result;
        }
    }
}
