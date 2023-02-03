using AnimesControl.Application.Mapper;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Services;
using AnimesControl.Infra.Context;
using AnimesControl.Infra.Extensions;
using AnimesControl.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastrutucture();
builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimesControl.Api", Version = "v1" });
        c.ResolveConflictingActions(x => x.First());
    });
var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseSwagger();

    app.Run();
}
