using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.Models
{
    public class AutoresLibro
    {
        [Key]
        public int autor_id { get; set; }
        public int libro_id { get; set;}
        public string orden { get; set; }
    }
}
