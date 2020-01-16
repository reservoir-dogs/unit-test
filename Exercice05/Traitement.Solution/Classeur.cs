using Framework.Error;
using Modele;
using Ressource.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Traitement.Solution
{
    public class Classeur
    {
        const string patternDossier = @"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4} - .*$";

        /// <summary>
        /// Conserve uniquement les dossiers qui ont le format suivant : dd-mm-yyyy - description
        /// </summary>
        public IList<Dossier> Nettoyer(IList<Dossier> dossiers)
        {
            var result = dossiers
                .Where(d => Regex.IsMatch(d.Nom, patternDossier))
                .ToList();

            return result;
        }

        /// <summary>
        /// Trie les dossiers par année, mois, jour (yyyy/mm/dd) puis description
        /// </summary>
        public IList<Dossier> Trier(IList<Dossier> dossiers)
        {
            var result = dossiers
                .OrderBy(d =>
                {
                    var elements = d.Nom.Split(new string[] { " - " }, StringSplitOptions.None);
                    var moments = elements[0].Split('-');

                    var r = string.Concat(moments[2], moments[1], moments[0], elements[1]);

                    return r;
                })
                .ToList();

            return result;
        }

        public Dossier Creer(string nom, IList<Fichier> fichiers)
        {
            if (!Regex.IsMatch(nom, patternDossier))
                throw new BusinessException(Resources.MessageMauvaisFormatDossier);

            if (fichiers.Count == 0)
                throw new BusinessException(Resources.MessageDossierVide);

            var result = new Dossier { Nom = nom, Fichiers = fichiers };

            return result;
        }
    }
}