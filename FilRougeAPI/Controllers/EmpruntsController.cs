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
    public class EmpruntsController : ControllerBase
    {
        private readonly IService<Emprunt> _service;

        private readonly IService<Livre> _serviceLivre;

        private readonly IService<Lecteur> _serviceLecteur;

        public EmpruntsController(IService<Emprunt> service, IService<Livre> serviceLivre, IService<Lecteur> serviceLecteur)
        {
            _service = service;
            _serviceLivre = serviceLivre;
            _serviceLecteur = serviceLecteur;
        }

        // GET: api/Emprunts
        [HttpGet]
        public IEnumerable<Emprunt> GetEmprunts()
        {
            return _service.GetAll();
        }

        // GET: api/Emprunts/5
        [HttpGet("{id}")]
        public ActionResult<Emprunt> GetEmprunt(int id)
        {
            var emprunt = _service.GetById(id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return emprunt;
        }

        // PUT: api/Emprunts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutEmprunt(int id, [FromBody] Emprunt emprunt)
        {
            if (id != emprunt.Id)
            {
                return BadRequest();
            }

            int idLivre = emprunt.LivreId;
            int idLecteur = emprunt.LecteurId;

            if (!_serviceLivre.Exist(idLivre))
            {
                return BadRequest("L'idLivre n'existe pas");
            }

            else if (!_serviceLecteur.Exist(idLecteur))
            {
                return BadRequest("L'idLecteur n'existe pas.");
            }


            try
            {
                _service.Update(emprunt);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(emprunt);
        }

        // POST: api/Emprunts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Emprunt> PostEmprunt([FromBody] Emprunt emprunt)
        {
            int idLivre = emprunt.LivreId;
            int idLecteur = emprunt.LecteurId;

            if (!_serviceLivre.Exist(idLivre))
            {
                return BadRequest("L'idLivre n'existe pas");
            }

            else if (!_serviceLecteur.Exist(idLecteur))
            {
                return BadRequest("L'idLecteur n'existe pas.");
            }

            var result = _service.Add(emprunt);
            return CreatedAtAction("GetEmprunt", new { id = emprunt.Id }, result);
        }

        // DELETE: api/Emprunts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmprunt(int id)
        {
            var resultat = _service.Delete(id);

            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool EmpruntExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
