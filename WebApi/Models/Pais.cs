using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Pais
    {
        public int Id { get; set;}

        [StringLength(30)]
        public string Nombre { get; set; }
    }
}
