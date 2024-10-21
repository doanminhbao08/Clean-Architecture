using AnhBach.Context;
using AnhBach.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


// Add services to DI container
builder.Services.AddScoped<IMyService, MyServiceNext>();
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddDbContext<AnhBachDBContext>();
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
