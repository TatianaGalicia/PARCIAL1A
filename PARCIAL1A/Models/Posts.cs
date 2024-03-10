using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.Models
{
    public class Posts
    {
        [Key]
        public int id { get; set; }
        public string titulo { get; set; }
        public string contenido { get; set; }
        public string fecha_publicacion { get; set; }
        public int autor_id { get; set; }

    }
    
    }

   


