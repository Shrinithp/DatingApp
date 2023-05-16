using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    //connections that to be made needs to be specified here.
    //connection are them made in appsettings.development.json file.
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

    //after making connections in json file, install migrations of creating all the connections.
    //dotnet tool install --global dotnet-ef --version 7.0.2 is the command to create migration.

    
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
