using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        private readonly parcial1AContext _parcial1AContexto;
        public AutorLibroController(parcial1AContext parcial1AContexto)
        {
            _parcial1AContexto = parcial1AContexto;
        }
        //metodoget
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<AutoresLibro> listaAutoresLibro = (from e in _parcial1AContexto.AutoresLibro select e).ToList();
            if (listaAutoresLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaAutoresLibro);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] AutoresLibro autoresLibro)
        {
            try
            {
                _parcial1AContexto.AutoresLibro.Add(autoresLibro);
                _parcial1AContexto.SaveChanges();
                return Ok(autoresLibro);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarautor(int id, [FromBody] AutoresLibro autoresLactualizar)
        {
            AutoresLibro? autoresLib = (from e in _parcial1AContexto.AutoresLibro
                             where e.libro_id == id
                             select e).FirstOrDefault();
            if (autoresLib == null) return NotFound();
            autoresLib.orden = autoresLactualizar.orden;
            _parcial1AContexto.Entry(autoresLib).State = EntityState.Modified;
            _parcial1AContexto.SaveChanges();
            return Ok(autoresLactualizar);

        }
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult eliminarautores(int id)
        {
            AutoresLibro? autoresLib = (from e in _parcial1AContexto.AutoresLibro
                             where e.libro_id == id
                             select e).FirstOrDefault();
            if (autoresLib == null) return NotFound();
            _parcial1AContexto.AutoresLibro.Attach(autoresLib);
            _parcial1AContexto.AutoresLibro.Remove(autoresLib);
            return Ok(autoresLib);


        }
        
    }
}
