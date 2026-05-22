using EscolaAPI.Data;
using EscolaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte aos Controllers
builder.Services.AddControllers();

// Configuração do PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Chave JWT a partir da configuração
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT key is not configured.");
var key = Encoding.UTF8.GetBytes(jwtKey);
// Configuração da autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Autorização
builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware de autenticação/autorização
app.UseAuthentication();
app.UseAuthorization();

// Garante banco e popula com dados de teste
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Alunos.Any())
    {
        db.Alunos.AddRange(
            new Aluno
            {
                Nome = "Ana Silva",
                Email = "ana.silva@example.com",
                Curso = "Matemática",
                DataNascimento = new DateTime(2004, 3, 10)
            },
            new Aluno
            {
                Nome = "Bruno Costa",
                Email = "bruno.costa@example.com",
                Curso = "Física",
                DataNascimento = new DateTime(2003, 7, 22)
            },
            new Aluno
            {
                Nome = "Carla Oliveira",
                Email = "carla.oliveira@example.com",
                Curso = "Química",
                DataNascimento = new DateTime(2005, 1, 15)
            }
        );
        db.SaveChanges();
    }
}

// Mapeia controllers
app.MapControllers();

app.Run();