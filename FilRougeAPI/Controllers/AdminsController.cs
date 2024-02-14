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
    public class AdminsController : ControllerBase
    {
        private readonly IService<Admin> _service;

        public AdminsController(IService<Admin> service)
        {
            _service = service;
        }
 
        // GET: api/Admins
        [HttpGet]
        public IEnumerable<Admin> GetAdmins()
        {
            return  _service.GetAll();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public ActionResult<Admin> GetAdmin(int id)
        {
            var admin = _service.GetById(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAdmin(int id, Admin admin)
        {
            if (id != admin.Id)
            {
                return BadRequest();
            }

            //_context.Entry(admin).State = EntityState.Modified;

            try
            {
                _service.Update(admin);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Accepted(admin);
        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Admin> PostAdmin(Admin admin)
        {
            var result = _service.Add(admin);
            return CreatedAtAction("GetAdmin", new { id = admin.Id }, result);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var resultat = _service.Delete(id);

            if (!resultat)
            {
                return NotFound();
            }
            return NoContent();
        }


        private bool AdminExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
