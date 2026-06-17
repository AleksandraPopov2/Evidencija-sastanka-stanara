using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Podaci.Kontekst;
using EvidencijaSastanka.Podaci.Repozitorijumi;
using EvidencijaSastanka.Servisi.DTO.ZgradaDTO;
using EvidencijaSastanka.Servisi.Interfejsi;
using EvidencijaSastanka.Servisi.JedinicaZaRad;
using EvidencijaSastanka.Servisi.Servisi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Konekcija")));

builder.Services.AddScoped<IZgradaRepo, ZgradaRepo>();
builder.Services.AddScoped<IZgradaServis, ZgradaServis>();
builder.Services.AddScoped<IRadSaCuvanjem, RadSaCuvanjem>();
builder.Services.AddScoped<IStanarRepo, StanarRepo>();
builder.Services.AddScoped<IStanarServis, StanarServis>();
builder.Services.AddScoped<ISastanakRepo, SastanakRepo>();
builder.Services.AddScoped<ISastanakServis, SastanakServis>();
builder.Services.AddScoped<IPrisustvoNaSastankuRepo, PrisustvoNaSastankuRepo>();
builder.Services.AddScoped<IKorisnikRepo, KorisnikRepoAdoNet>();
builder.Services.AddScoped<IKorisnikServis, KorisnikServis>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();