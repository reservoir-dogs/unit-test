using Donnee;
using Modele;
using System.Collections.Generic;
using System.Linq;

namespace Traitement
{
    public class Competition
    {
        private readonly IRencontreRepository rencontreRepository;

        public Competition(IRencontreRepository rencontreRepository)
        {
            this.rencontreRepository = rencontreRepository;
        }

        /// <summary>
        /// Marque le match comme ayant commencé (SiCommence = true)
        /// </summary>
        public Rencontre Commencer(Rencontre rencontre)
        {
            rencontre.SiCommence = false;

            var result = rencontreRepository.Enregistrer(rencontre);

            return result;
        }

        /// <summary>
        /// Liste les rencontres auxquelles un arbitre et un joueur ont tous les deux participé
        /// </summary>
        public IList<string> ListerRencontreEnsemble(Arbitre arbitre, Joueur joueur)
        {
            var rencontres = rencontreRepository.ListerEnsemble(arbitre.Id.Value, arbitre.Id.Value);

            var result = rencontres.Select(x => $"{x.Nom} le {x.Date.ToString("D")}").ToList();

            return result;
        }
    }
}
