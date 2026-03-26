using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppWebAWAQ.Models;

namespace AppWebAWAQ.Controllers;

public class InfoUserController : Controller
{
    private string CurrentUsername => HttpContext.Session.GetString("UserSession"); /* Se lee el usuario de la sesion actual */

    private static EnlaceUsuario enlace_dummy_01 = new EnlaceUsuario
    {
        Red_Social = "LinkedIn",
        Liga = "https://www.linkedin.com/in/jimena-gonzales-022700275/"
    };

    private static EnlaceUsuario enlace_dummy_02 = new EnlaceUsuario
    {
        Red_Social = "Facebook",
        Liga = "https://www.facebook.com/share/1AjSR1t7Nj/"
    };

    private static UsuarioViewModel user_dummy = new UsuarioViewModel /* Simulacion de usuario de BD */
    {
        Username = "jimegonz@gmail.com",
        Nombre = "Jimena Gonzales",
        Contacto_Numero = "+52 33 1901 1011",
        Correo_Electronico = "jimegonz@gmail.com",
        Biografia = "Soy Jimena Gonzales, investigadora ambiental apasionada por comprender los ecosistemas y los desafíos que enfrenta nuestro planeta. He dedicado mi carrera al estudio riguroso del medio ambiente, desarrollando conocimientos que me permiten proponer soluciones concretas y con impacto real frente a la crisis ambiental que vivimos hoy.\n\nMi trabajo va más allá de la investigación académica. Creo profundamente que el cambio verdadero ocurre cuando las organizaciones con valores compartidos colaboran y suman esfuerzos. Por eso, he orientado mi trayectoria hacia la construcción de alianzas estratégicas con instituciones, empresas y colectivos que, como yo, tienen como meta preservar y restaurar el entorno natural para las generaciones futuras.\n\nMe considero un puente entre la ciencia y la acción. Mi capacidad para traducir datos complejos en estrategias claras y aplicables me permite ser una aliada efectiva para cualquier organización que busque incorporar prácticas ambientalmente responsables. Estoy convencida de que la colaboración entre mentes afines es la herramienta más poderosa que tenemos para construir un futuro más sostenible.",
        Enlaces = [enlace_dummy_01, enlace_dummy_02],
        LigaFotoPerfil = "~/images/avtr_carpintero_cuello_rojo.jpg"
    };

    private UsuarioViewModel CurrentUser = new UsuarioViewModel();
    
    /* funcion basica para revisar si el usuario es el que esta en sesion */
    private void checkUser()
    {
        if (CurrentUsername == user_dummy.Username)
        {
            CurrentUser = user_dummy;
        }
        else
        {
            CurrentUser = new UsuarioViewModel();
        }
    }

    public IActionResult DetallesUsuario()
    {     
        checkUser();
        return View(CurrentUser);
    }

    public IActionResult EditarUsuario()
    {
        checkUser();
        return View(CurrentUser);
    }

    
    [HttpPost]
    /* recibimos el usuario actualizado */
    public IActionResult EditarUsuario(UsuarioViewModel updateUser)
    {
        /* establecemos valores preexistentes que no se pueden cambiar */
        updateUser.Username = user_dummy.Username;
        updateUser.Nombre = user_dummy.Nombre;

        /* quitamos valores de la revision (ya que revisa el modelo como llego, y no actualizado) */
        ModelState.Remove("Username");
        ModelState.Remove("Nombre");

        if (ModelState.IsValid)
        {
            user_dummy = updateUser;
            /* volvemos a pantalla de perfil */
            return RedirectToAction("DetallesUsuario");  
        }

        // Vuelve a mostrar el modelo en caso de que algo haya fallado
        return View(updateUser);        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}