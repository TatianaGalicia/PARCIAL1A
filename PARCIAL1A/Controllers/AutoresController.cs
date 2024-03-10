using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;
using System.Diagnostics.Contracts;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly parcial1AContext _parcial1AContexto;
        public AutoresController(parcial1AContext parcial1AContexto)
        {
            _parcial1AContexto = parcial1AContexto;
        }
        //metodoget
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Autores> listaautores = (from e in _parcial1AContexto.Autores select e).ToList();
            if (listaautores.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaautores);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Autores auto)
        {
            try
            {
                _parcial1AContexto.Autores.Add(auto);
                _parcial1AContexto.SaveChanges();
                return Ok(auto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }

        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarautor(int id , [FromBody] Autores autoactualizar)
        { 
            Autores? auto =(from e in _parcial1AContexto.Autores
                            where e.id==id 
                            select e).FirstOrDefault();
        if(auto==null) return NotFound();
        auto.nombre = autoactualizar.nombre;
         _parcial1AContexto.Entry(auto).State=EntityState.Modified;
        _parcial1AContexto.SaveChanges();
            return Ok(autoactualizar);

        }
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult eliminarautores(int id)
        {
            Autores? auto = (from e in _parcial1AContexto.Autores
                             where e.id == id
                             select e).FirstOrDefault();
            if (auto == null) return NotFound();
            _parcial1AContexto.Autores.Attach(auto);
            _parcial1AContexto.Autores.Remove(auto);
            return Ok(auto);


        }

       
    }
}
