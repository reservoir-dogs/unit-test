using Modele;
using System.Collections.Generic;

namespace Donnee
{
    public interface IRencontreRepository
    {
        Rencontre Enregistrer(Rencontre rencontre);

        IList<Rencontre> ListerEnsemble(long arbitreId, long joueurId);
    }
}
