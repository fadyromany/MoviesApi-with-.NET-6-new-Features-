using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionstring = builder.Configuration.GetConnectionString(name:"Defaultconnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseSqlServer(connectionstring));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(options=>
{
    options.SwaggerDoc(name: "v1", info: new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        //Title = "Test"

    });
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme 
    {
        Name = "Autheorization",
        Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme= "Bearer",
        BearerFormat="JWT",
        In= Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description="Enter JWT KEY"

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().WithOrigins(origins: "localhost:30000"));

app.UseAuthorization();

app.MapControllers();

app.Run();
