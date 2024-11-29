using ApiPaplria.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicio para la conexi�n a SQL Server
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
        policy.AllowAnyOrigin()   // Permite cualquier dominio (aj�stalo si necesitas mayor control)
              .AllowAnyHeader()   // Permite cualquier encabezado
              .AllowAnyMethod();  // Permite cualquier m�todo HTTP
    });
});

var app = builder.Build();

// Configurar la tuber�a de solicitudes HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();          // Generar documentaci�n Swagger
    app.UseSwaggerUI();        // Habilitar la interfaz gr�fica de Swagger
}

app.UseHttpsRedirection();      // Redirigir autom�ticamente HTTP a HTTPS

app.UseCors("AllowSpecificOrigin");  // Aplicar la pol�tica de CORS

app.UseAuthorization();         // Manejar la autorizaci�n

app.MapControllers();           // Mapear controladores a las rutas correspondientes

app.Run();                      // Iniciar la aplicaci�n

