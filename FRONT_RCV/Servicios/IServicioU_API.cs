using FRONT_RCV.Models;

namespace FRONT_RCV.Servicios
{
    public interface IServicioU_API
    {

        Task<List<Usuario>> ListaU();
        Task<Usuario> ObtenerU(int idUsuario);
        Task<bool>GuardarU(Usuario objeto);
        Task<bool>EditarU(Usuario objeto);
        Task<bool>EliminarU(int idUsuario);
        Task<Usuario> GetUsuario(string Documento, string Clave);
    }
}
