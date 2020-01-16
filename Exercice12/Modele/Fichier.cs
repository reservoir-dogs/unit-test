using System;

namespace Modele
{
    public class Fichier
    {
        public virtual long? Id { get; set; }
        public virtual string Nom { get; set; }
        public virtual DateTime DateCreation { get; set; }
    }
}
