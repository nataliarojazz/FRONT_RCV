using Microsoft.AspNetCore.Mvc;
using FRONT_RCV.Models;
using System.Diagnostics;
using FRONT_RCV.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FRONT_RCV.Controllers
{
    public class InicioController : Controller
    {
        private readonly IServicioI_API _servicioApi;

        private readonly IServicioU_API _servicioUApi;

        public InicioController(IServicioI_API servicioApi, IServicioU_API servicioUApi)
        {
            _servicioApi = servicioApi;
            _servicioUApi = servicioUApi;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(string Documento, string Clave)
        {
            Usuario usuario_encontrado = await _servicioUApi.GetUsuario(Documento, Clave);

            if (usuario_encontrado == null)
            {

                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.Name, usuario_encontrado.Nombre)
    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("index");
        }
    }
}