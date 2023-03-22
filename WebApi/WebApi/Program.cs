﻿using WebApi.DAL;
using WebApi.Helpers;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddSingleton<CourseContext>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICourseInstanceRepository, CourseInstanceRepository>();
builder.Services.AddTransient<IFileParser, FileParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
