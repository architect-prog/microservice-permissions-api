using ArchitectProg.Kernel.Extensions.Exceptions;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.WebApi.Extensions.Filters;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Creators;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Core.Validators.Application;
using Microservice.Permissions.Core.Validators.Area;
using Microservice.Permissions.Core.Validators.Common;
using Microservice.Permissions.Core.Validators.Role;
using Microservice.Permissions.Database;
using Microservice.Permissions.Database.Repositories;
using Microservice.Permissions.Database.Services;
using Microservice.Permissions.Database.Settings;
using Microsoft.EntityFrameworkCore;
using UpdateRoleRequestValidator = Microservice.Permissions.Core.Validators.Role.UpdateRoleRequestValidator;
using ValidationException = ArchitectProg.Kernel.Extensions.Exceptions.ValidationException;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new BadRequestOnExceptionFilter(typeof(ValidationException)));
    options.Filters.Add(new NotFoundOnExceptionFilter(typeof(ResourceNotFoundException)));
}).AddControllersAsServices();

builder.Services.AddScoped<IRoleCreator, RoleCreator>();
builder.Services.AddScoped<IAreaCreator, AreaCreator>();
builder.Services.AddScoped<IAreaRoleCreator, AreaRoleCreator>();
builder.Services.AddScoped<IApplicationCreator, ApplicationCreator>();

builder.Services.AddScoped<IAreaMapper, AreaMapper>();
builder.Services.AddScoped<IRoleMapper, RoleMapper>();
builder.Services.AddScoped<IPermissionMapper, PermissionMapper>();
builder.Services.AddScoped<IApplicationMapper, ApplicationMapper>();
builder.Services.AddScoped<IAreaPermissionsMapper, AreaPermissionsMapper>();

builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.Decorate<IAreaService, AreaServiceValidationDecorator>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.Decorate<IRoleService, RoleServiceValidationDecorator>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.Decorate<IApplicationService, ApplicationServiceValidationDecorator>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IAreaRoleService, AreaRoleService>();

builder.Services.AddDbContext<ApplicationDatabaseContext>();
builder.Services.AddScoped<DbContext, ApplicationDatabaseContext>();
builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

builder.Services.AddScoped<IValidator<(int?, int?)>, SkipTakeValidator>();
builder.Services.AddScoped<IValidator<CreateRoleRequest>, CreateRoleRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateRoleRequest>, UpdateRoleRequestValidator>();
builder.Services.AddScoped<IValidator<CreateApplicationRequest>, CreateApplicationRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateApplicationRequest>, UpdateApplicationRequestValidator>();
builder.Services.AddScoped<IValidator<CreateAreaRequest>, CreateAreaRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateAreaRequest>, UpdateAreaRequestValidator>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

builder.Services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();