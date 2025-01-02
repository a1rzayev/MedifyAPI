using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Services;
using MedifyAPI.Infrastructure.Repositories.EfCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string LocalHostUrl = "http://localhost:5271";

builder.Services.AddScoped<IHospitalRepository, HospitalEfCoreRepository>();
builder.Services.AddScoped<IHospitalService, HospitalService>();

builder.Services.AddScoped<IPatientRepository, PatientEfCoreRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();


builder.Services.AddDbContext<MedifyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.`
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
