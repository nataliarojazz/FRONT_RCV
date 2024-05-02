using FRONT_RCV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FRONT_RCV.Servicios;
namespace FRONT_RCV.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicioI_API _servicioApi;
        private readonly IServicioU_API _servicioUApi;

        public HomeController(IServicioI_API servicioAPI, IServicioU_API servicioUApi)
        {
            _servicioApi = servicioAPI;
            _servicioUApi = servicioUApi;
        }

        public async Task <IActionResult> Index()
        {
            List<Usuario> ListaU = await _servicioUApi.ListaU();

            return View(ListaU);
        }

        public async Task<IActionResult> IndexU()
        {
            List<Usuario> ListaU = await _servicioUApi.ListaU();

            return View(ListaU);
        }


        //USUARIO
        //usar este controlador para editar y guardar Usuario
        public async Task<IActionResult> Usuario(int IdUsuario)
        {
            Usuario modelo_usuario = new Usuario();
            ViewBag.Accion = "Nuevo Usuario";
            if (IdUsuario != 0)//==
            {
                modelo_usuario = await _servicioUApi.ObtenerU(IdUsuario);
                ViewBag.Accion = "Editar Usuario";
            }
            return View(modelo_usuario);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambiosU(Usuario ob_usuario)
        {
            bool respuesta;

            if (ob_usuario.IdUsuario == 0)
            {
                respuesta = await _servicioUApi.GuardarU(ob_usuario);
            }
            else
            {
                respuesta = await _servicioUApi.EditarU(ob_usuario);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> EliminarU(int IdUsuario)
        {
            var respuesta = await _servicioUApi.EliminarU(IdUsuario);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }


        //Lista Imagenes
        public async Task<IActionResult> ImagenL()
        {
            List<Imagen> ListaI = await _servicioApi.ListaI();

            return View(ListaI);
        }
        //usar este controlador para editar y guardar Imagen
        public async Task<IActionResult> Imagen(int IdImagen)
        {
            Imagen modelo_imagen = new Imagen();
            ViewBag.Accion = "Nueva Imagen";
            if (IdImagen != 0)
            {
                modelo_imagen = await _servicioApi.ObtenerI(IdImagen);
                ViewBag.Accion = "Editar Imagen";
            }
            return View(modelo_imagen);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambiosI(Imagen ob_imagen)
        {
            bool respuesta;

            if (ob_imagen.IdImagen == 0)
            {
                respuesta = await _servicioApi.GuardarI(ob_imagen);
            }
            else
            {
                respuesta = await _servicioApi.EditarI(ob_imagen);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> EliminarI(int idImagen)
        {
            var respuesta = await _servicioApi.EliminarI(idImagen);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
