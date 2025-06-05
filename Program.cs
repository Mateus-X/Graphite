using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Graphite.Database;
using Graphite.Source.Domain.Entities; // Ajuste conforme a localização da entidade
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Adicionada para corrigir o erro
using Microsoft.Extensions.DependencyInjection; // Adicionada para corrigir o erro
using Microsoft.EntityFrameworkCore.SqlServer; // Adicionada para corrigir o erro

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDatabaseContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();