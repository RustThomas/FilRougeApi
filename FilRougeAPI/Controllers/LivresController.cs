using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilRougeAPI.Configs;
using FilRougeAPI.Models;
using FilRougeAPI.Services;
using FilRougeAPI.Repositories;

namespace FilRougeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : ControllerBase
    {
        private readonly IService<Livre> _service;

        private readonly IService<Auteur> _serviceAuteur;
        private readonly IService<Domaine> _serviceDomaine;
        private readonly IService<Emprunt> _serviceEmprunt;

        public LivresController(IService<Livre> service, IService<Auteur> serviceAuteur, IService<Domaine> serviceDomaine, IService<Emprunt> serviceEmprunt)
        {
            _service = service;
            _serviceAuteur = serviceAuteur;
            _serviceDomaine = serviceDomaine;
            _serviceEmprunt = serviceEmprunt;
        }

        // GET: api/Livres
        [HttpGet]
        public IEnumerable<Livre> GetLivres()
        {
            return _service.GetAll();
        }

        // GET: api/Livres/5
        [HttpGet("{id}")]
        public ActionResult<Livre> GetLivre(int id)
        {
            var livre = _service.GetById(id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }

        // PUT: api/Livres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutLivre(int id, [FromBody] Livre livre)
        {
            if (id != livre.Id)
            {
                return BadRequest();
            }

            int idDomaine = livre.DomaineId;
            int idAuteur = livre.AuteurId;

            if (!_serviceAuteur.Exist(idAuteur))
            {
                return BadRequest("L'idAuteur n'existe pas");
            }

            else if (!_serviceDomaine.Exist(idDomaine))
            {
                return BadRequest("L'idDomaine n'existe pas.");
            }

            try
            {
                _service.Update(livre);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(livre);
        }

        // POST: api/Livres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Livre> PostLivre([FromBody] Livre livre)
        {
            int idDomaine = livre.DomaineId;
            int idAuteur = livre.AuteurId;
            
            if ( !_serviceAuteur.Exist(idAuteur))
            {
                return BadRequest("L'idAuteur n'existe pas");
            }

            else if (!_serviceDomaine.Exist(idDomaine))
            {
                return BadRequest("L'idDomaine n'existe pas.");
            }

            var result = _service.Add(livre);
            return CreatedAtAction("GetLivre", new { id = livre.Id }, result);
        }

        // DELETE: api/Livres/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLivre(int id)
        {
            var emprunts = _serviceEmprunt.GetAll();
            foreach (Emprunt emprunt in emprunts)
            {
                if (emprunt.LivreId == id)
                {
                    return BadRequest("Le livre est associée à au moins un Emprunt");
                }
            }

            var resultat = _service.Delete(id);

            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool LivreExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
