using AnhBach;
using BookService.BookContext;
using BookService.Features.Books.Dtos;
using BookService.Features.Books.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Loader;



var builder = WebApplication.CreateBuilder(args);


// C?u hình d?ch v? MediatR
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

builder.Services.AddControllers();

builder.Services.AddApplicationServices();


// Add services to the container.
builder.Services.AddDbContext<BookDBContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
