using Framework.Helper;
using System;

namespace Modele
{
    public class Rencontre
    {
        public long? Id { get; set; }
        public string Nom { get; set; }
        public DateTime Date { get; set; }
        public bool SiCommence { get; set; }

        public override bool Equals(object obj)
        {
            var builder = new EqualsBuilder<Rencontre>(this, obj);

            builder.With(m => m.Id);
            builder.With(m => m.Nom);
            builder.With(m => m.Date);
            builder.With(m => m.SiCommence);

            return builder.Equals();
        }

        public override int GetHashCode()
        {
            var builder = new HashCodeBuilder<Rencontre>(this);

            builder.With(m => m.Id);
            builder.With(m => m.Nom);
            builder.With(m => m.Date);
            builder.With(m => m.SiCommence);

            return builder.HashCode;
        }
    }
}
