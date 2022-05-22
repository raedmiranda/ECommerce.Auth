using System;

namespace ECommerce.Auth.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int Intentos { get; set; }
        public int NivelSeg { get; set; }
        public DateTime FechaCracion { get; set; }
    }
}
