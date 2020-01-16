using System.Collections.Generic;

namespace Modele
{
    public class Dossier
    {
        public string Nom { get; set; }
        public IList<Fichier> Fichiers { get; set; }
    }
}
