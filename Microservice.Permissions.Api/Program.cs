using ArchitectProg.Kernel.Extensions.Exceptions;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.WebApi.Extensions.Filters;
using Microservice.Permissions.Api.Configuration;
using Microservice.Permissions.Api.Configuration.Interfaces;
using Microservice.Permissions.Core.Creators;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database;
using Microservice.Permissions.Database.Repositories;
using Microservice.Permissions.Database.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new BadRequestOnExceptionFilter(typeof(ValidationException)));
    options.Filters.Add(new NotFoundOnExceptionFilter(typeof(ResourceNotFoundException)));
}).AddControllersAsServices();

builder.Services.AddScoped<IApplicationCreator, ApplicationCreator>();
builder.Services.AddScoped<IRoleCreator, RoleCreator>();
builder.Services.AddScoped<IAreaCreator, AreaCreator>();

builder.Services.AddScoped<IApplicationMapper, ApplicationMapper>();
builder.Services.AddScoped<IAreaMapper, AreaMapper>();
builder.Services.AddScoped<IRoleMapper, RoleMapper>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddScoped<DbContext, ApplicationDatabaseContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

builder.Services.AddScoped<IDatabaseSettingsProvider, DatabaseSettingsProvider>();
builder.Services.AddScoped(x =>
{
    var settingsProvider = x.GetRequiredService<IDatabaseSettingsProvider>();
    var databaseSettings = settingsProvider.DatabaseSettings;
    return databaseSettings;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();