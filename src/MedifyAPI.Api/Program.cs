using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Services;
using MedifyAPI.Infrastructure.Repositories.EfCore;
using Microsoft.Extensions.Options;
using System.Text; 
using Microsoft.IdentityModel.Tokens;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string LocalHostUrl = "http://localhost:5271";

builder.Services.AddScoped<ITokenRepository, TokenEfCoreRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddScoped<IHospitalRepository, HospitalEfCoreRepository>();
builder.Services.AddScoped<IHospitalService, HospitalService>();


builder.Services.AddScoped<IPatientRepository, PatientEfCoreRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IDoctorRepository, DoctorEfCoreRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddScoped<ILogRepository, LogEfCoreRepository>();
builder.Services.AddScoped<ILogService, LogService>();



builder.Services.AddDbContext<MedifyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.`
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
