using System.Collections.Generic;

namespace Modele
{
    public class Dossier
    {
        public long? Id { get; set; }
        public string Nom { get; set; }
        public IList<Fichier> Fichiers { get; set; }
        public bool SiArchive { get; set; }
    }
}
