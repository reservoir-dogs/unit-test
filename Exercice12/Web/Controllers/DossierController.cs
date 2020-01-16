using Donnee;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Web.Transferts;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class DossierController : Controller
    {
        private readonly IDossierRepository dossierRepository;

        public DossierController(IDossierRepository dossierRepository)
        {
            this.dossierRepository = dossierRepository;
        }

        [HttpGet]
        public IList<DossierTransfert> Lister()
        {
            var resultat = dossierRepository.Lister()
                .Select(d => new DossierTransfert { Id = d.Id.Value, Nom = d.Nom, DateCreation = d.DateCreation })
                .ToList();

            return resultat;
        }
    }
}