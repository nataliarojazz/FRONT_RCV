namespace FRONT_RCV.Models
{
    public class Usuario
    {
     public int IdUsuario { get; set; }
     public string Nombre { get; set; }
     public string Apellido { get; set; }
     public string Telefono { get; set; }
     public string Documento { get; set; }
     public string Clave { get; set; }
	 public string Rol { get; set; }
        
        // Constructor para inicializar el rol predeterminado
        public Usuario()
        {
            Rol = "Usuario"; // Rol predeterminado
        }
    }
}
