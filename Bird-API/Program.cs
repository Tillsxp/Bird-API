using Bird_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection

builder.Services.AddDbContext<BirdContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")) 
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Service Provider

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<BirdContext>();
    await context.Database.MigrateAsync();
    await SeedData.LoadBirdsData(context);
}
catch (Exception ex)
{
    Console.WriteLine($"{0} - {1}", ex.Message, ex.InnerException!.Message);
	throw;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
