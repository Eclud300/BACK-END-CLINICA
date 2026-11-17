using Microsoft.EntityFrameworkCore;
using Clinica.Data; // Ajusta este namespace si es distinto

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN CORS PARA ANGULAR
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. CONEXIÓN A POSTGRES CON MINÚSCULAS AUTOMÁTICAS
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLowerCaseNamingConvention()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. ACTIVAR CORS (Siempre ANTES de Authorization)
app.UseCors("PermitirAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();