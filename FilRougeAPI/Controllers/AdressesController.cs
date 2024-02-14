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
using NuGet.Versioning;

namespace FilRougeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly IService<Adress> _service;

        private readonly IService<Lecteur> _serviceLecteur;

        public AdressesController(IService<Adress> service, IService<Lecteur> serviceLecteur)
        {
            _service = service;
            _serviceLecteur = serviceLecteur;
        }

        // GET: api/Adresses
        [HttpGet]
        public IEnumerable<Adress> GetAdresses()
        {
            return _service.GetAll();
        }

        // GET: api/Adresses/5
        [HttpGet("{id}")]
        public ActionResult<Adress> GetAdress(int id)
        {
            var adress = _service.GetById(id);

            if (adress == null)
            {
                return NotFound();
            }

            return adress;
        }

        // PUT: api/Adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAdress(int id, Adress adress)
        {
            if (id != adress.Id)
            {
                return BadRequest();
            }

            //_context.Entry(admin).State = EntityState.Modified;

            try
            {
                _service.Update(adress);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(adress);
        }

        // POST: api/Adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Adress> PostAdress(Adress adress)
        {
            var result = _service.Add(adress);
            return CreatedAtAction("GetAdress", new { id =adress.Id }, result);
        }

        // DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAdress(int id)
        {
  
          
            var lecteurs = _serviceLecteur.GetAll();
            foreach(Lecteur lecteur in lecteurs)
            {
                if(lecteur.AdresseId == id)
                {
                    return BadRequest("L'adresse est associée à au moins un Lecteurs");
                }
            }

            var resultat = _service.Delete(id);
            
            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool AdressExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
