using ApiPaplria.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicio para la conexión a SQL Server
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

// Agregar servicios de controladores
builder.Services.AddControllers();

// Configurar Swagger para documentar la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.AllowAnyOrigin()   // Permite cualquier dominio (ajústalo si necesitas mayor control)
              .AllowAnyHeader()   // Permite cualquier encabezado
              .AllowAnyMethod();  // Permite cualquier método HTTP
    });
});

var app = builder.Build();

// Configurar la tubería de solicitudes HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();          // Generar documentación Swagger
    app.UseSwaggerUI();        // Habilitar la interfaz gráfica de Swagger
}

app.UseHttpsRedirection();      // Redirigir automáticamente HTTP a HTTPS

app.UseCors("AllowSpecificOrigin");  // Aplicar la política de CORS

app.UseAuthorization();         // Manejar la autorización

app.MapControllers();           // Mapear controladores a las rutas correspondientes

app.Run();                      // Iniciar la aplicación

