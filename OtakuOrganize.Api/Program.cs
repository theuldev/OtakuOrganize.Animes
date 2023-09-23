using OtakuOrganize.Application.Mapper;
using OtakuOrganize.Core.Interfaces.Repositories;
using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Services;
using OtakuOrganize.Infra.Context;
using OtakuOrganize.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using OtakuOrganize.Api.Extensions;
using OtakuOrganize.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastrutucture(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSubscribers();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "OtakuOrganize.Api", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {

            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."

        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
           {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },new string[]{}
            }

        });
    });
var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllers();
    app.UseConsul();
    app.UseSwagger();

    app.Run();
}
