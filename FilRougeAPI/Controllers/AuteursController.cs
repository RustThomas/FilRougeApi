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

namespace FilRougeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteursController : ControllerBase
    {
        private readonly IService<Auteur> _service;
        private readonly IService<Livre> _serviceLivre;

        public AuteursController(IService<Auteur> service, IService<Livre> serviceLivre)
        {
            _service = service;
            _serviceLivre = serviceLivre;
        }

        // GET: api/Auteurs
        [HttpGet]
        public IEnumerable<Auteur> GetAuteurs()
        {
            return _service.GetAll();
        }

        // GET: api/Auteurs/5
        [HttpGet("{id}")]
        public ActionResult<Auteur> GetAuteur(int id)
        {
            var auteur = _service.GetById(id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        // PUT: api/Auteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.Id)
            {
                return BadRequest();
            }

            //_context.Entry(admin).State = EntityState.Modified;

            try
            {
                _service.Update(auteur);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(auteur);
        }

        // POST: api/Auteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Auteur> PostAuteur(Auteur auteur)
        {
            var result = _service.Add(auteur);
            return CreatedAtAction("GetAuteur", new { id = auteur.Id }, result);
        }

        // DELETE: api/Auteurs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuteur(int id)
        {

            var livres = _serviceLivre.GetAll();
            foreach (Livre livre in livres)
            {
                if (livre.AuteurId == id)
                {
                    return BadRequest("L'auteur est associé à au moins un Livre");
                }
            }

            var resultat = _service.Delete(id);

            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool AuteurExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
