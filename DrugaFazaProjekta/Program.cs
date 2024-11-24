using Microsoft.EntityFrameworkCore;
using Hangfire;
using DrugaFazaProjekta.Models;
using DrugaFazaProjekta.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PriceOptimizationService>();

builder.Services.AddHangfire(conf =>
{
    conf.UseSqlServerStorage("Server=(localdb)\\mssqllocaldb;Database=DrugaFazaProjekta;Trusted_Connection=True;TrustServerCertificate=True;");
    conf.UseSimpleAssemblyNameTypeSerializer();
    conf.UseRecommendedSerializerSettings();

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.UseHangfireServer();

RecurringJob.AddOrUpdate<PriceOptimizationService>(
    "optimizacija-cena",
    servis => servis.OptimizujCene(),
    Cron.Daily
);

app.Run();
