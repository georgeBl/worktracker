//database setup?
using Microsoft.EntityFrameworkCore;
using WorkTracker.Models;
using System.Diagnostics;
using WorckTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<WorkTrackerDatabaseSettings>(
    builder.Configuration.GetSection("WorkTrackerDatabase"));

builder.Services.AddSingleton<TasksService>();
builder.Services.AddControllers().AddNewtonsoftJson();
// builder.Services.AddDbContext<TaskContext>(opt =>
//     opt.UseInMemoryDatabase("TaskList"));

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
