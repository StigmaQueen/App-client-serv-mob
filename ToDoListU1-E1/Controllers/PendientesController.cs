using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListU1_E1.Models;
using ToDoListU1_E1.Repositories;

namespace ToDoListU1_E1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendientesController : ControllerBase
    {
        public Sistem21TodolistContext context { get; set; }
        Repository<Actividades> repository;
        public PendientesController(Sistem21TodolistContext Context)
        {
            context = Context;
            repository = new Repository<Actividades>(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = repository.GetAll();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Actividades a)
        {
            if (string.IsNullOrWhiteSpace(a.Descripcion))
            {
                return BadRequest("Escriba la descripcion de la actividad");
            }
            a.Id = 0;
            repository.Insert(a);
            return Ok();
        }
        [HttpPut]
        public IActionResult Put(Actividades actividad)
        {
            if (string.IsNullOrWhiteSpace(actividad.Descripcion))
            {
                return BadRequest("La descripción no debe estar vacío. Escriba una para continuar");
            }

            var act = repository.GetByID(actividad.Id);

            if (act != null)
            {
                act.Descripcion = actividad.Descripcion;
                repository.Update(act);
                return Ok();
            }
            else
                return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var act = repository.GetByID(id);

            if (act != null)
            {
                repository.Delete(act);
                return Ok();
            }
            else
                return NotFound();

        }
    }
}
