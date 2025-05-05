using ApiCURD__practice.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.Xml;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//jwt 
var jwtKey = builder.Configuration["jwt:Key"];
var jwtIssuer = builder.Configuration["jwt:Issuer"];

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience=false,
            ValidateLifetime=true,
            ValidateIssuerSigningKey=true,
            ValidIssuer = jwtIssuer,
            IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
    
    



builder.Services.AddDbContext<AppDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//for custom create swahher 
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "Product API",
//        Version = "v1",
//        Description = "A simple example of a .NET Web API using EF Core",
//    });
//});

//Adding Cross
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              // policy.WithOrigins("http://localhost:4200")  //only your origin 

              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();               // Enable Swagger middleware
    app.UseSwaggerUI();            // Enable Swagger UI at /swagger
}

  

app.UseHttpsRedirection();
app.UseCors("AllowAll"); //Add Cross
app.UseAuthorization();

app.MapControllers();

app.Run();
