using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Traitement
{
    public class Classeur
    {
        /// <summary>
        /// Conserve uniquement les dossiers qui ont le format suivant : dd-mm-yyyy - description
        /// </summary>
        public IList<Dossier> Nettoyer(IList<Dossier> dossiers)
        {
            var result = dossiers
                .Where(d => Regex.IsMatch(d.Nom, @"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4} - .*$"))
                .ToList();

            return result;
        }

        /// <summary>
        /// Trie les dossiers par année, mois, jour (yyyy/mm/dd) puis description
        /// </summary>
        public IList<Dossier> Trier(IList<Dossier> dossiers)
        {
            throw new NotImplementedException();
        }
    }
}
