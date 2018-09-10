using System;
using System.ComponentModel.DataAnnotations;

namespace Noticias.Models
{
    public abstract class Base
    {
        public int id { get; set; }
        
        public int UsuarioCriacao { get; set; }
        
        public int UsuarioAlteracao { get; set; }

        public DateTime? DataCriacao { get; set; }

        public DateTime? DataAlteracao { get; set; }
    }
}
