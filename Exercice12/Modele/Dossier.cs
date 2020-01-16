using System;
using System.Collections.Generic;

namespace Modele
{
    public class Dossier
    {
        public virtual long? Id { get; set; }
        public virtual string Nom { get; set; }
        public virtual DateTime DateCreation { get; set; }
        public virtual bool SiArchive { get; set; }
        public virtual IList<Fichier> Fichiers { get; set; }
    }
}
