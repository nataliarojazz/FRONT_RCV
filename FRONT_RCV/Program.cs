using FRONT_RCV.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Servicio API
builder.Services.AddScoped<IServicioI_API, ServicioI_API>();
builder.Services.AddScoped<IServicioU_API, ServicioU_API>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.Cookie.Name = "MiCookie";
           options.LoginPath = "/Inicio/IniciarSesion"; // Ruta para el inicio de sesión
           options.AccessDeniedPath = "/Inicio/AccessDenied"; // Ruta para el acceso denegado
           options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // Tiempo de expiración de la cookie
       });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");

app.Run();
