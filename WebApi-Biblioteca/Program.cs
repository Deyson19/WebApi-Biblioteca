using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi_DataAccess;
using WebApi_Services.Contrato;
using WebApi_Services.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Usando sql server
var connectionString_SQLServer = builder.Configuration.GetConnectionString("NameConnection");
builder.Services.AddDbContext<BibliotecaDbContext>(options => options.UseSqlServer(connectionString_SQLServer));


//usando postgre
var connectionString_Postgre = builder.Configuration.GetConnectionString("Postgres");
//builder.Services.AddDbContext<BibliotecaDbContext>(op => op.UseNpgsql(connectionString_Postgre));


// agregar servicios
builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();

//HAbilitar los cors

builder.Services.AddCors(op =>
{
    /*Mejor practica para cors*/
    op.AddPolicy("angularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseCors("angularApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
