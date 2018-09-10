using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Noticias.Models
{
    public class Municipio : Base
    {
        
        [Display(Name ="Municipio")]
        [MaxLength(100)]
        public string Nome { get; set; }

        
        [Display(Name = "UF")]
        [MaxLength(2)]
        public string Uf { get; set; }
    }
}