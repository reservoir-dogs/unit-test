using Modele;
using System.Collections.Generic;

namespace Donnee.Solution
{
    public interface IRencontreRepository
    {
        Rencontre Enregistrer(Rencontre rencontre);

        IList<Rencontre> ListerEnsemble(Arbitre arbitre, Joueur joueur);
    }
}
