using Member.Context;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using MySQL;
using MySqlX.XDevAPI;
using Microsoft.EntityFrameworkCore;
using Member.Infrastructure.Repostory;
using Member.Infrastructure.Middleware;
using Member.Infrastructure.Abstraction.Interfaces;
using Member.Web.Api.Extentions;
using Member.Infrastructure.Abstraction.Repostory;
using Member.Service.Model;
using System.Configuration;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext(builder.Configuration);
builder.Configuration.BindModel(builder.Services);
builder.Services.AddRepostory();
builder.Services.ConfigureJWT();
builder.Services.AddAuthorizationService();
builder.Services.AddAutoMapper();
builder.Services.AddAccountManager();
builder.Services.AddEmailSender();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
