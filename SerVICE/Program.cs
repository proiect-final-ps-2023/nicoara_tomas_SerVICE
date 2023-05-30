global using SerVICE.Models;
global using Microsoft.EntityFrameworkCore;
global using SerVICE.Data;

using SerVICE.Services.ServiceForService;
using SerVICE.Services.ServiceForCategory;
using Microsoft.AspNetCore.Identity;
using SerVICE.Services.ServiceForUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IService, ServiceV2Service>();
builder.Services.AddScoped<ICategoryService, CategoryServiceV1>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DataContext>();

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
