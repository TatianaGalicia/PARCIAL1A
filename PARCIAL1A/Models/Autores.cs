﻿
using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.Models
{
    public class Autores
    {
 
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
    }
}
