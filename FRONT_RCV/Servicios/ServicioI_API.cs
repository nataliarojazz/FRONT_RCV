using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using FRONT_RCV.Models;
namespace FRONT_RCV.Servicios
{
    public class ServicioI_API : IServicioI_API
    {
        public static string _usuario;
        public static string _clave;
        public static string _baseurl;
        public static string _token;

        public ServicioI_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSettings:usuario").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<List<Imagen>> ListaI()
        {
            List<Imagen> listaI = new List<Imagen>();

            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            var response = await usuario.GetAsync("api/Imagen/ListaI");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                listaI = resultado.listaI;
            }
            return listaI; // En caso de error, devuelve vacío
        }

        public async Task<Imagen> ObtenerI(int IdImagen)
        {
            Imagen objetoI = new Imagen(); // Inicializamos objetoU como null
                                             // await Autenticar();
            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await usuario.GetAsync($"api/Imagen/ObtenerI/{IdImagen}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objetoI = resultado.objetoI;
            }

            return objetoI;
        }

        public async Task<bool> GuardarI(Imagen objetoI)
        {
            bool respuesta = false;

            //await Autenticar();

            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objetoI), Encoding.UTF8, "application/json");

            var response = await usuario.PostAsync("api/Imagen/GuardaI/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> EditarI(Imagen objetoI)
        {
            bool respuesta = false;

            //await Autenticar();

            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objetoI), Encoding.UTF8, "application/json");

            var response = await usuario.PutAsync("api/Imagen/EditarI/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> EliminarI(int IdImagen)
        {
            bool respuesta = false;

           // await Autenticar();
            var usuario = new HttpClient();
            usuario.BaseAddress = new Uri(_baseurl);
            usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await usuario.DeleteAsync($"api/Imagen/EliminarI/{IdImagen}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public Task<Imagen> GetUsuario(string Documento, string Clave)
        {
            throw new NotImplementedException();
        }
    }
}

