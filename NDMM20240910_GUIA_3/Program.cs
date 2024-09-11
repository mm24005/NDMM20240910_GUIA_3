//Agregar autenticación por cooki//

using Microsoft.AspNetCore.Authentication.Cookies;

using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias.
// Agrega el servicio de controladores al contenedor
builder.Services.AddControllers();
// Agrega el servicio para la exploración de API de puntos finales
builder.Services.AddEndpointsApiExplorer();
// Agrega el servicio para la generación de Swagger
builder.Services.AddSwaggerGen();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
