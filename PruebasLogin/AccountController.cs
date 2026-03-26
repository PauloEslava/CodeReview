using Microsoft.AspNetCore.Mvc;
using AppWebAWAQ.Models;


    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Este post recibe la informacion ingresada al form
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //
            if (ModelState.IsValid)
            {
                // Datos de prueba xd
                if (model.Username == "jimegonz@gmail.com" && model.Password == "gonzales96$")
                {
                    HttpContext.Session.SetString("UserSession", model.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // En caso de que los datos sean erroneos, se indica al usuario
                    ModelState.AddModelError(string.Empty, "El usuario o la contraseña son invalidos.");
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
        // Esto borra la sesion actual
            HttpContext.Session.Clear();
            // Y enviamos al usuario de vuelta al login
            return RedirectToAction("Login", "Account");
        }
    }
