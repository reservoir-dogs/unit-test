using FluentNHibernate.Mapping;
using Modele;

namespace Mapping
{
    public class FichierMap : ClassMap<Fichier>
    {
        public FichierMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nom).Not.Nullable();
            Map(x => x.DateCreation).Not.Nullable();
        }
    }
}
