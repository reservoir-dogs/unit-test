using FluentNHibernate.Mapping;
using Modele;

namespace Mapping
{
    public class DossierMap : ClassMap<Dossier>
    {
        public DossierMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nom).Not.Nullable();
            Map(x => x.DateCreation).Not.Nullable();
            Map(x => x.SiArchive).Not.Nullable();

            HasMany(x => x.Fichiers);
        }
    }
}
