//Agregar autenticaci�n por cooki//

using Microsoft.AspNetCore.Authentication.Cookies;

using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias.
// Agrega el servicio de controladores al contenedor
builder.Services.AddControllers();
// Agrega el servicio para la exploraci�n de API de puntos finales
builder.Services.AddEndpointsApiExplorer();
// Agrega el servicio para la generaci�n de Swagger
builder.Services.AddSwaggerGen();

// Configuraci�n para la autenticaci�n por cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Configura el nombre del par�metro de URL para redireccionamiento no autorizado
        options.ReturnUrlParameter = "unauthorized";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                // Cambia el c�digo de estado a No autorizado
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                // Establece el tipo de contenido como JSON (u otro formato deseado)
                context.Response.ContentType = "application/json";

                var message = new
                {
                    error = "No autorizado",
                    message = "Debe iniciar sesi�n para acceder a este recurso."
                };

                // Serializa el objeto 'message' en formato JSON
                var jsonMessage = JsonSerializer.Serialize(message);

                // Escribe el mensaje JSON en la respuesta HTTP
                return context.Response.WriteAsync(jsonMessage);
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
