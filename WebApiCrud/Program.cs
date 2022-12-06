using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiCrud.Data;
using WebApiCrud.Data.Interfaces;
using WebApiCrud.Mapper;
using WebApiCrud.Services;
using WebApiCrud.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//Para utlizar el patron repositoriy
builder.Services.AddScoped<IApiRepository, ApiRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//Agrego config para el uso de TOKEN (paso 1) (primero configurar el token en appsettings)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

//Se agraga Token Service (paso 2)
builder.Services.AddScoped<ITokenService, TokenService>();



builder.Services.AddControllers();
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
//app.UseAuthentication() lo agrego para la autenticacion por web token
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
