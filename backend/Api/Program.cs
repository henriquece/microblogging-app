using System.Reflection;
using System.Text.Json;
using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

builder.Services.AddDbContext<AppDbContext>((sp, options) => {
  options.UseNpgsql(connectionString);
});

builder.Services.AddControllers()
  .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
