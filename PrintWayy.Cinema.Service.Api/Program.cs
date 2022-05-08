using MediatR;
using Microsoft.Extensions.FileProviders;
using PrintWayy.Cinema.Domain.Commands.Requests.Film;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;
using PrintWayy.Cinema.Domain.Commands.Responses.Session;
using PrintWayy.Cinema.Domain.Handlers;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Infra.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Register Session Request
builder.Services.AddScoped<IRequestHandler<CreateSessionRequest, CreateSessionResponse>, SessionHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteSessionRequest, DeleteSessionResponse>, SessionHandler>();
//Register Film Request
builder.Services.AddScoped<IRequestHandler<CreateFilmRequest, CreateFilmResponse>, FilmHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateFilmRequest, UpdateFilmResponse>, FilmHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteFilmRequest, DeleteFilmResponse>, FilmHandler>();
//Register Data
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
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
