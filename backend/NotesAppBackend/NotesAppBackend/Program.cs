using Microsoft.EntityFrameworkCore;
using NotesAppBackend.Repositories;
using NotesAppBackend.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddDbContext<NoteAppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);


//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Politic", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});




var app = builder.Build();


//Create DB when running app
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NoteAppDBContext>();
    context.Database.Migrate();
}

//Add-Migration InitDB -OutputDir Repositories/Migrations

// Configure the HTTP request pipeline.

app.UseCors("Politic");
app.UseAuthorization();

app.MapControllers();



app.Run();
