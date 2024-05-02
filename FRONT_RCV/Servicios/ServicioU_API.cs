using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using FRONT_RCV.Models;
namespace FRONT_RCV.Servicios
{
    public class ServicioU_API : IServicioU_API
    {
        public static string _usuario;
        public static string _clave;
        public static string _baseurl;
        public static string _token;

        public ServicioU_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSettings:usuario").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        //Ejecutar metodo autenticar Usuario
        public async Task Autenticar()
        {
            var usuario = new HttpClient();

            usuario.BaseAddress = new Uri(_baseurl);

            //Crear Credenciales, propiedades, 
            var credenciales = new Credencial() { documento = _usuario, clave = _clave };

            //Crear contenido
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");

            //Ejecutar api autenticar peticion
            var response = await usuario.PostAsync("api/Autenticacion/Validar", content);

            var json_respuesta = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);

            //Utilizar dentro del metodo
            _token = resultado.token;
        }

        public async Task<List<Usuario>> ListaU()
        {
            List<Usuario> listaU = new List<Usuario>();

            //await Autenticar();

            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await usuario.GetAsync("api/Imagen/ListaI");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                listaU = resultado.listaU;
            }
            return listaU; // En caso de error, devuelve vacio
        }

        public async Task<Usuario> ObtenerU(int IdUsuario)
        {
            Usuario objetoU = new Usuario(); // Inicializamos objetoU como null
            await Autenticar();
            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await usuario.GetAsync($"api/Usuario/ObtenerU/{IdUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objetoU = resultado.objetoU;
            }

            return objetoU;
        }

        public async Task<bool> GuardarU(Usuario objetoU)
        {
            bool respuesta = false;

            await Autenticar();

            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objetoU), Encoding.UTF8, "application/json");

            var response = await usuario.PostAsync("api/Usuario/GuardaU/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> EditarU(Usuario objetoU)
        {
            bool respuesta = false;

            await Autenticar();

            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objetoU), Encoding.UTF8, "application/json");

            var response = await usuario.PutAsync("api/Usuario/EditarU/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> EliminarU(int IdUsuario)
        {
            bool respuesta = false;

            await Autenticar();
            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await usuario.DeleteAsync($"api/Usuario/EliminarU/{IdUsuario}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public Task<Usuario> GetUsuario(string Documento, string Clave)
        {
            throw new NotImplementedException();
        }
    }
}

