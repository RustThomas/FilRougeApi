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
    public class LecteursController : ControllerBase
    {
        private readonly IService<Lecteur> _service;

        private readonly IService<Adress> _serviceAdress;

        private readonly IService<Emprunt> _serviceEmprunt;
        public LecteursController(IService<Lecteur> service, IService<Adress> serviceAdress, IService<Emprunt> serviceEmprunt)
        {
            _service = service;
            _serviceAdress = serviceAdress;
            _serviceEmprunt = serviceEmprunt;
        }

        // GET: api/Lecteurs
        [HttpGet]
        public IEnumerable<Lecteur> GetLecteurs()
        {
            return _service.GetAll();
        }

        // GET: api/Lecteurs/5
        [HttpGet("{id}")]
        public ActionResult<Lecteur> GetLecteur(int id)
        {
            var lecteur = _service.GetById(id);

            if (lecteur == null)
            {
                return NotFound();
            }

            return lecteur;
        }

        // PUT: api/Lecteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutLecteur(int id, [FromBody] Lecteur lecteur)
        {
            if (id != lecteur.Id)
            {
                return BadRequest();
            }

            int idAdres = lecteur.AdresseId;

            if (_serviceAdress.Exist(idAdres))
            {
                /*
                //Il semblerait que ce morceau de code ne soit pas nécessaire, le db context fait le job directement
                var adress = _serviceAdress.GetById(idAdres);
                if (adress != null)
                {
                    adress.Lecteurs.Add(lecteur);
                    _serviceAdress.Update(adress);
                }
                */
                
            }
            else
            {
                return BadRequest("L'idAdresse n'existe pas");
            }

            try
            {
                _service.Update(lecteur);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(lecteur);
        }

        // POST: api/Lecteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Lecteur> PostLecteur(Lecteur lecteur)
        {
            int idAdres = lecteur.AdresseId;
            if (_serviceAdress.Exist(idAdres))
            {
                lecteur.Adresse = _serviceAdress.GetById(idAdres);
            }
            else
            {
                return BadRequest("L'idAdresse n'existe pas");
            }
            var result = _service.Add(lecteur);
            return CreatedAtAction("GetLecteur", new { id = lecteur.Id }, result);
        }

        // DELETE: api/Lecteurs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLecteur(int id)
        {
            var emprunts= _serviceEmprunt.GetAll();
            foreach (Emprunt emprunt in emprunts)
            {
                if (emprunt.LecteurId == id)
                {
                    return BadRequest("Le lecteur est associée à au moins un Emprunt");
                }
            }


            var resultat = _service.Delete(id);

            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool LecteurExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
