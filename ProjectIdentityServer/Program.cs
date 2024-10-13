using Config;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Validation;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISvUser, SvUser>();


// Configuración de IdentityServer
builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddResourceOwnerValidator<SvResourceOwnerPasswordValidator>()
    .AddProfileService<SvProfile>()
    .AddDeveloperSigningCredential(); 

builder.Services.AddControllers()
    .AddNewtonsoftJson(x =>
        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
