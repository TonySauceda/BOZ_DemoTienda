using DemoTienda.Application.Services;
using DemoTienda.Infraestructure.Context;
using DemoTienda.Infraestructure.Extensions;
using DemoTiendaApiController.Settings;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DemoTiendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoTienda")));

builder.Services.AddOptions<ProductoSettings>()
    .Bind(builder.Configuration.GetSection("ProductoSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddInfraestructure();

builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProductoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.DeepSpace;
        options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.HideClientButton();
        options.DarkMode = false;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
