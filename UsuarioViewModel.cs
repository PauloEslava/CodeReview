using System.ComponentModel.DataAnnotations;

namespace AppWebAWAQ.Models
{
    public class UsuarioViewModel
    {
        public string Username {get; set;}
        public string Nombre {get; set;}

        [Phone(ErrorMessage = "Ingrese un número teléfonico válido")]
        public string? Contacto_Numero {get; set;}

        [Required(ErrorMessage = "Ingrese un correo electrónico")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        public string Correo_Electronico {get; set;}
        public string? Biografia {get; set;}
        public List<EnlaceUsuario>? Enlaces { get; set; }
        public string LigaFotoPerfil {get; set;}
    }

    public class EnlaceUsuario
    {
        public string? Red_Social {get; set;}
        
        [Url(ErrorMessage = "Ingrese una liga válida")]
        public string? Liga {get; set;}
        
    }
}
