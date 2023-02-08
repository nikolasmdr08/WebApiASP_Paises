using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return context.provincia.ToList();
        }

        [HttpGet("{id}", Name = "paisCreado") ]
        public IActionResult GetById(int id)
        {
            var pais = context.provincia.Include(x => x.Provincias).FirstOrDefault(x => x.Id == id);
            if (pais == null)
                return NotFound(); //404

            return Ok(pais); //201
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.provincia.Add(pais);
                context.SaveChanges();
                return new CreatedAtRouteResult("paisCreado", new { id = pais.Id },pais); //201
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }
            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pais = context.provincia.FirstOrDefault(x => x.Id == id);

            if(pais == null)
            {
                return NotFound();
            }

            context.provincia.Remove(pais);
            context.SaveChanges();
            return Ok(pais);
        }
    }
}
