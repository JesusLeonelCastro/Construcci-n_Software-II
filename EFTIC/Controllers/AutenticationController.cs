using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EFTIC.Controllers
{
    public class AutenticationController : Controller
    {
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: AutenticationController.cs
         * PROPÓSITO: Gestiona el proceso de autenticación de usuarios.
         * AUTOR: Jesus leonel Castro Gutierrez / Alexis artinez 
         * FECHA:20-11-2024
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador forma parte del sistema EFTIC y se encarga de:
         * - Mostrar el formulario de inicio de sesión (Login).
         * - Validar las credenciales del usuario contra la base de datos.
         * - Manejar la autenticación y creación de sesión usando FormsAuthentication.
         * - Redirigir al usuario a la vista correspondiente después del login.
         * - Finalizar la sesión del usuario con LogOut().
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Se usa FormsAuthentication para el control de acceso autenticado.
         * - Manejo de sesiones mediante `Session["Usuarios"]` y `Session.Abandon()`.
         * - Uso de TempData para comunicar mensajes de error al usuario.
         * - Métodos separados para mayor claridad y responsabilidad única.
         * - Se proporciona compatibilidad con `ReturnUrl` para redirección segura.
         * 
         * NOTAS:
         * - El modelo `Usuario` debe implementar el método `Autenticar()` para verificar credenciales.
         * - Considerar cifrado de contraseñas y validaciones adicionales en producción.
         * ------------------------------------------------------------
         */


        // GET: Autentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(Usuario Usuario, string ReturnUrl)
        {
            HomeController obj = new HomeController();

            if (IsValid(Usuario))
            {
                Session["Usuarios"] = Usuario.UsuarioID;

                FormsAuthentication.SetAuthCookie(Usuario.DNI, false);

                //Para mostar Nombre y Apellido en el _Layout.cshtml
                ViewBag.NombreCompleto = Usuario.Nombre_Usuario + " " + Usuario.Apellido_Usuario;

                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");

            }
            TempData["mensaje"] = "ERROR : El Numero Documento o contraseña que ingresaste no está correcto a verifique bien sus dato.";

            return RedirectToAction("Login");

        }


        //Cerrar session de usuario
        private bool IsValid(Usuario usuarios)
        {

            return usuarios.Autenticar();
        }

        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        //FR 001- REQUERIMIENTO FUNCIONAL - AUTENTICAR USUARIO
    }
}