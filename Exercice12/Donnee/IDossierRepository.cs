using Modele;
using System.Collections.Generic;

namespace Donnee
{
    public interface IDossierRepository
    {
        void Enregistrer(Dossier dossier);

        IList<Dossier> Lister();
    }
}