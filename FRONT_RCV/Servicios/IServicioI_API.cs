using FRONT_RCV.Models;

namespace FRONT_RCV.Servicios
{
    public interface IServicioI_API
    {

        Task<List<Imagen>> ListaI();
        Task<Imagen> ObtenerI(int idImagen);
        Task<bool>GuardarI(Imagen objeto);
        Task<bool>EditarI(Imagen objeto);
        Task<bool>EliminarI(int idImagen);
    }
}
