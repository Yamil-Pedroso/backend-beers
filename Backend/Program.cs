using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, People2Service>();
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

//Acceso a la misma clase con diferentes tipos de inyeccion
// Inyeccion Singleton
builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");

// Inyeccion Scoped
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");

// Inyeccion Transient
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

// Inyeccion Scoped to PostsService
builder.Services.AddScoped<IPostsService, PostsService>();

// Inyeccion Scoped to BeerService
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");

// Repositories
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

// HttpClient service jsonplaholder
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    //c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts"); // podemos insertar nuestra url aca y va a funcionar pero si queremos trabajar en desarrollo o produccion seria conveniente guardarla en appsettings.json
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});

// Entity Framework ---> Connection to the DB Server Managment Studio
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// Validators
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

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
