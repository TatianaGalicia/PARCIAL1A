using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase

    {
        private readonly parcial1AContext _parcial1AContexto;
        public PostsController(parcial1AContext parcial1AContexto)
        {
            _parcial1AContexto = parcial1AContexto;
        }
        //metodoget
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Posts> listaPosts = (from e in _parcial1AContexto.Posts select e).ToList();
            if (listaPosts.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaPosts);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Posts posts)
        {
            try
            {
                _parcial1AContexto.Posts.Add(posts);
                _parcial1AContexto.SaveChanges();
                return Ok(posts);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarautor(int id, [FromBody] Posts postsActualizar)
        {
            Posts? postsLista = (from e in _parcial1AContexto.Posts
                                 where e.id == id
                                 select e).FirstOrDefault();
            if (postsLista == null) return NotFound();
            postsLista.titulo = postsActualizar.titulo;
            postsLista.contenido = postsActualizar.contenido;
            postsLista.fecha_publicacion = postsActualizar.fecha_publicacion;
            postsLista.autor_id = postsActualizar.autor_id;


            _parcial1AContexto.Entry(postsLista).State = EntityState.Modified;
            _parcial1AContexto.SaveChanges();
            return Ok(postsActualizar);

        }
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult eliminarautores(int id)
        {
            Posts? listaPosts = (from e in _parcial1AContexto.Posts
                                 where e.id == id
                                 select e).FirstOrDefault();
            if (listaPosts == null) return NotFound();
            _parcial1AContexto.Posts.Attach(listaPosts);
            _parcial1AContexto.Posts.Remove(listaPosts);
            return Ok(listaPosts);


        }
        [HttpGet]
        [Route("FiltrarPorAutor/{filter}")]
        public IActionResult FiltrarPorAutor(String filter)
        {
            var listadoPosts = (from au in _parcial1AContexto.Autores
                               join ps in _parcial1AContexto.Posts
                                  on au.id equals ps.autor_id
                               where au.nombre.Contains(filter)
                               select new
                               {
                                   id = ps.id,
                                   titulo = ps.titulo,
                                   contenido = ps.contenido,
                                   fecha_publicacion = ps.fecha_publicacion,
                                   nombre = au.nombre,

                               }).Take(20).ToList();
            if (listadoPosts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);
        }

        [HttpGet]
        [Route("FiltrarPorLibro/{filter}")]
        public IActionResult FiltrarPorLibro(String filter)
        {
            var listadoPosts = (from li in _parcial1AContexto.libros
                               join auli in _parcial1AContexto.AutoresLibro
                                  on li.id equals auli.libro_id
                               join au in _parcial1AContexto.Autores
                                  on auli.autor_id equals au.id
                               join ps in _parcial1AContexto.Posts
                                  on au.id equals ps.autor_id
                               where li.titulo.Contains(filter)
                               select new
                               {
                                   id = ps.id,
                                   titulo = ps.titulo,
                                   contenido = ps.contenido,
                                   fecha_publicacion = ps.fecha_publicacion,
                                   nombre = au.nombre,
                                   tituloLibro = li.titulo

                               }).ToList();
            if (listadoPosts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);

        }
        [HttpGet]
        [Route("FiltrarPorLibro")]
        public IActionResult FiltrarPorLibro()
        {
            var listadoPosts = (from li in _parcial1AContexto.libros
                               join auli in _parcial1AContexto.AutoresLibro
                                  on li.id equals auli.libro_id
                               join au in _parcial1AContexto.Autores
                                  on auli.autor_id equals au.id
                               join ps in _parcial1AContexto.Posts
                                  on au.id equals ps.autor_id
                               select new
                               {
                                   id = ps.id,
                                   titulo = ps.titulo,
                                   contenido = ps.contenido,
                                   fecha_Publicacion = ps.fecha_publicacion,
                                   nombre = au.nombre,
                                   Libros = li.id,
                                   libro_id = li.titulo,
                               }).OrderBy(resultado => resultado.libro_id).ToList();
            if (listadoPosts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);
        }
    }
}
