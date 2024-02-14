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
    public class DomainesController : ControllerBase
    {
        private readonly IService<Domaine> _service;
        private readonly IService<Livre> _serviceLivre;

        public DomainesController(IService<Domaine> service, IService<Livre> serviceLivre)
        {
            _service = service;
            _serviceLivre = serviceLivre;
        }

        // GET: api/Domaines
        [HttpGet]
        public IEnumerable<Domaine> GetDomaines()
        {
            return _service.GetAll();
        }

        // GET: api/Domaines/5
        [HttpGet("{id}")]
        public ActionResult<Domaine> GetDomaine(int id)
        {
            var domaine = _service.GetById(id);

            if (domaine == null)
            {
                return NotFound();
            }

            return domaine;
        }

        // PUT: api/Domaines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutDomaine(int id, Domaine domaine)
        {
            if (id != domaine.Id)
            {
                return BadRequest();
            }

            try
            {
                _service.Update(domaine);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(domaine);
        }

        // POST: api/Domaines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Domaine> PostDomaine(Domaine domaine)
        {
            var result = _service.Add(domaine);
            return CreatedAtAction("GetDomaine", new { id = domaine.Id }, result);
        }

        // DELETE: api/Domaines/5
        [HttpDelete("{id}")]
        public ActionResult DeleteDomaine(int id)
        {
            var livres = _serviceLivre.GetAll();
            foreach (Livre livre in livres)
            {
                if (livre.DomaineId == id)
                {
                    return BadRequest("Le domaines est associé à au moins un Livre");
                }
            }

            var resultat = _service.Delete(id);

            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool DomaineExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
