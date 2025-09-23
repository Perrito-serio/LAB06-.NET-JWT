using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Habilitar el uso de Controladores en lugar de APIs mínimas.
builder.Services.AddControllers();

// 2. Configuración de Swagger/OpenAPI para que entienda y use JWT.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSwaggerGen(options =>
{
    // Añadimos una definición de seguridad para que Swagger sepa cómo manejar el token Bearer.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingrese un token válido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Añadimos un requisito de seguridad que obliga a usar el token en los endpoints protegidos.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// 3. Configuración del servicio de autenticación con JWT.
// Le "enseñamos" a la API cómo debe validar los tokens que reciba.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // Aquí se usarán los valores del appsettings.json
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// 4. Configuración del servicio de autorización y políticas.
// Aquí definimos los "permisos" o "roles".
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Admin"));
    // Podríamos agregar más políticas aquí, por ejemplo:
    // options.AddPolicy("Vendedor", policy => policy.RequireRole("Vendedor"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 5. Añadir los middlewares de Autenticación y Autorización.
// ¡El orden es MUY importante! Primero autentica, luego autoriza.
app.UseAuthentication();
app.UseAuthorization();

// 6. Mapear los controladores que crearemos más adelante.
app.MapControllers();

app.Run();
