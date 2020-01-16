using System.Collections.Generic;
using Modele;

namespace Traitement
{
    public interface IClasseur
    {
        void Archiver(Dossier dossier);
        Dossier Creer(string nom, IList<string> nomFichiers);
        IList<Dossier> Nettoyer(IList<Dossier> dossiers);
        IList<Dossier> Trier(IList<Dossier> dossiers);
    }
}