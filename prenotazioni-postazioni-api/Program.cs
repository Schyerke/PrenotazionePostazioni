using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;
using prenotazioni_postazioni_api.Controllers;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;



var builder = WebApplication.CreateBuilder(args);

// Add logging Log4Net
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FestaController, FestaController>();
builder.Services.AddSingleton<FestaService, FestaService>();
builder.Services.AddSingleton<FestaRepository, FestaRepository>();

builder.Services.AddSingleton<ImpostazioneController, ImpostazioneController>();
builder.Services.AddSingleton<ImpostazioneRepository, ImpostazioneRepository>();
builder.Services.AddSingleton<ImpostazioneService, ImpostazioneService>();

builder.Services.AddSingleton<PrenotazioneController, PrenotazioneController>();
builder.Services.AddSingleton<PrenotazioneService, PrenotazioneService>();
builder.Services.AddSingleton<PrenotazioneRepository, PrenotazioneRepository>();

builder.Services.AddSingleton<RuoloController, RuoloController>();
builder.Services.AddSingleton<RuoloService, RuoloService>();
builder.Services.AddSingleton<RuoloRepository, RuoloRepository>();

builder.Services.AddSingleton<StanzaController, StanzaController>();
builder.Services.AddSingleton<StanzaService, StanzaService>();
builder.Services.AddSingleton<StanzaRepository, StanzaRepository>();

builder.Services.AddSingleton<UtenteController, UtenteController>();
builder.Services.AddSingleton<UtenteService, UtenteService>();
builder.Services.AddSingleton<UtenteRepository, UtenteRepository>();

builder.Services.AddSingleton<VotoController, VotoController>();
builder.Services.AddSingleton<VotoService, VotoService>();
builder.Services.AddSingleton<VotoRepository, VotoRepository>();


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
