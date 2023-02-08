using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/pais/{PaisID}/provincia")]
    public class ProvinciaController : Controller
    {

        private readonly ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            return context.provincias.Where(x => x.PaisId == PaisId).ToList();
        }

        [HttpGet("{id}", Name = "provinciaById")]
        public IActionResult GetById(int id)
        {
            var pais = context.provincias.FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            return new ObjectResult(pais);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Provincia provincia, int PaisId)
        {
            provincia.PaisId = PaisId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.provincias.Add(provincia);
            context.SaveChanges();

            return new CreatedAtRouteResult("provinciaById", new { id = provincia.Id }, provincia);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var provincia = context.provincias.FirstOrDefault(x => x.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            context.provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);
        }
    }
}

