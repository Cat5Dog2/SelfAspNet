using SelfAspNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyContext>(options =>
  options.UseSqlServer(
    builder.Configuration.GetConnectionString("MyContext")
  )
);

// Add services to the container.

builder.Services.AddControllers()
  .AddXmlSerializerFormatters()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
  });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v2", new OpenApiInfo
  {
    Title = "Book API",
    Description = "書籍を管理するためのASP.NET Core Web API",
    Version = "v2",
    TermsOfService = new Uri("https://wings.msn.to/terms"),
    Contact = new OpenApiContact
    {
      Name = "お問い合わせ",
      Url = new Uri("https://wings.msn.to/contact")
    },
    License = new OpenApiLicense
    {
      Name = "ライセンス",
      Url = new Uri("https://wings.msn.to/license")
    }
  });

  var name = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, name));
});

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "CorePolicy",
    policy =>
    {
      policy.WithMethods("GET")
        .WithOrigins("https://localhost:52642", "https://wings.msn.to");
    });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "CoreApi");
  });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
