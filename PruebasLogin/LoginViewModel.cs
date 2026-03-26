using System.ComponentModel.DataAnnotations;

namespace AppWebAWAQ.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingresa un correo electrónico")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ingresa una contraseña")]
        [DataType(DataType.Password)] // Mostramos la contraseña como puntitos
        public string Password { get; set; }
    }
}