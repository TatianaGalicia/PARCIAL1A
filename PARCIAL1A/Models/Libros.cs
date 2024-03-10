using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.Models
{
    public class Libros
        {
            [Key]
            public int id { get; set; }
        public string titulo{ get; set; }
    }
}
