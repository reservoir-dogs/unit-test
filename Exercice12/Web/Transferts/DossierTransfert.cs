using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Transferts
{
    public class DossierTransfert
    {
        public long Id { get; set; }
        public virtual string Nom { get; set; }
        public virtual DateTime DateCreation { get; set; }
    }
}
