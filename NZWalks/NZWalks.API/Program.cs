using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositiories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding db context to IServiceCollection container
builder.Services.AddDbContext<NZWalksDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks")); });

//adding repo to container
builder.Services.AddScoped<IRegionRepository, RegionRepository>();

//registering auto mapper to the container to be used
//when application starts it looks up for the assembly Program and looks for all the Profiles created to map models
builder.Services.AddAutoMapper(typeof(Program).Assembly);


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
