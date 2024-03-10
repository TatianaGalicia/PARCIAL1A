using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL1A.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly parcial1AContext _parcial1AContexto;
        public LibrosController(parcial1AContext parcial1AContexto)
        {
            _parcial1AContexto = parcial1AContexto;
        }
        //metodoget
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Libros> listalibro = (from e in _parcial1AContexto.libros select e).ToList();
            if (listalibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listalibro);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Libros Libros)
        {
            try
            {
                _parcial1AContexto.libros.Add(Libros);
                _parcial1AContexto.SaveChanges();
                return Ok(Libros);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarautor(int id, [FromBody] Libros librosActualizar)
        {
            Libros? autoresLib = (from e in _parcial1AContexto.libros
                                        where e.id == id
                                        select e).FirstOrDefault();
            if (autoresLib == null) return NotFound();
            autoresLib.titulo = librosActualizar.titulo;
            _parcial1AContexto.Entry(autoresLib).State = EntityState.Modified;
            _parcial1AContexto.SaveChanges();
            return Ok(librosActualizar);

        }
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult eliminarautores(int id)
        {
            Libros? listaLibros = (from e in _parcial1AContexto.libros
                                        where e.id == id
                                        select e).FirstOrDefault();
            if (listaLibros == null) return NotFound();
            _parcial1AContexto.libros.Attach(listaLibros);
            _parcial1AContexto.libros.Remove(listaLibros);
            return Ok(listaLibros);


        }
        [HttpGet]
        [Route("FiltrarPorAutor/{filter}")]
        public IActionResult findByName(String filter)
        {
            var listadoEquipo = (from au in _parcial1AContexto.Autores
                                 join auLi in _parcial1AContexto.AutoresLibro
                                    on au.id equals auLi.autor_id
                                 join li in _parcial1AContexto.libros
                                    on auLi.libro_id equals li.id
                                 where au.nombre.Contains(filter)
                                 select new
                                 {
                                     idLibro = li.id,
                                     tituloLibro = li.titulo,
                                     autorLibro = au.nombre,

                                 }).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
    }
}
