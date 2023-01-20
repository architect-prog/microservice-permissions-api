using ArchitectProg.Kernel.Extensions.Exceptions;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.WebApi.Extensions.Filters;
using ArchitectProg.WebApi.Extensions.Responses;
using FluentValidation;
using Microservice.Permissions.Api.Extensions;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Creators;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Factories;
using Microservice.Permissions.Core.Factories.Interfaces;
using Microservice.Permissions.Core.Mappers;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Core.Validators.Application;
using Microservice.Permissions.Core.Validators.Area;
using Microservice.Permissions.Core.Validators.Common;
using Microservice.Permissions.Core.Validators.Permission;
using Microservice.Permissions.Core.Validators.Role;
using Microservice.Permissions.Kernel.Interfaces;
using Microservice.Permissions.Persistence.EfCore;
using Microservice.Permissions.Persistence.EfCore.Repositories;
using Microservice.Permissions.Persistence.EfCore.Services;
using Microservice.Permissions.Persistence.EfCore.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValidationException = ArchitectProg.Kernel.Extensions.Exceptions.ValidationException;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new BadRequestOnExceptionFilter(typeof(ValidationException)));
        options.Filters.Add(new NotFoundOnExceptionFilter(typeof(ResourceNotFoundException)));
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var messages = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            var message = string.Join(" ", messages);

            var response = new ErrorResult(StatusCodes.Status400BadRequest, message);
            return new BadRequestObjectResult(response);
        };
    })
    .AddControllersAsServices();

builder.Services.AddScoped<IRoleCreator, RoleCreator>();
builder.Services.AddScoped<IAreaCreator, AreaCreator>();
builder.Services.AddScoped<IPermissionCollectionCreator, PermissionCollectionCreator>();
builder.Services.AddScoped<IApplicationCreator, ApplicationCreator>();
builder.Services.AddScoped<IPermissionCreator, PermissionCreator>();

builder.Services.AddScoped<IAreaMapper, AreaMapper>();
builder.Services.AddScoped<IRoleMapper, RoleMapper>();
builder.Services.AddScoped<IPermissionMapper, PermissionMapper>();
builder.Services.AddScoped<IApplicationMapper, ApplicationMapper>();
builder.Services.AddScoped<IPermissionCollectionMapper, PermissionCollectionMapper>();
builder.Services.AddScoped<IPermissionCollectionDetailsMapper, PermissionCollectionDetailsMapper>();

builder.Services.AddScoped<IDefaultPermissionFactory, DefaultPermissionFactory>();

builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IPermissionCollectionService, PermissionCollectionService>();

builder.Services.Decorate<IRoleService, RoleServiceValidationDecorator>();
builder.Services.Decorate<IAreaService, AreaServiceValidationDecorator>();
builder.Services.Decorate<IApplicationService, ApplicationServiceValidationDecorator>();
builder.Services.Decorate<IPermissionService, PermissionServiceValidationDecorator>();

builder.Services.AddScoped<IValidator<int>, IdentifierValidator>();
builder.Services.AddScoped<IValidator<int[]>, IdentifierArrayValidator>();
builder.Services.AddScoped<IValidator<string[]>, PermissionNamesValidator>();
builder.Services.AddScoped<IValidator<(int?, int?)>, SkipTakeValidator>();
builder.Services.AddScoped<IValidator<CreateRoleRequest>, CreateRoleRequestValidator>();
builder.Services.AddScoped<IValidator<(int, UpdateRoleRequest)>, UpdateRoleRequestValidator>();
builder.Services.AddScoped<IValidator<CreateApplicationRequest>, CreateApplicationRequestValidator>();
builder.Services.AddScoped<IValidator<(int, UpdateApplicationRequest)>, UpdateApplicationRequestValidator>();
builder.Services.AddScoped<IValidator<CreateAreaRequest>, CreateAreaRequestValidator>();
builder.Services.AddScoped<IValidator<(int, UpdateAreaRequest)>, UpdateAreaRequestValidator>();
builder.Services.AddScoped<IValidator<UpdatePermissionsRequest>, UpdatePermissionsRequestValidator>();

builder.Services.AddDbContext<ApplicationDatabaseContext>();
builder.Services.AddScoped<DbContext, ApplicationDatabaseContext>();
builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddScoped<IDatabaseMigrationApplier, DatabaseMigrationApplier>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

builder.Services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

var app = builder.Build();

app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(policy =>
{
    var corsOrigins = configuration
        .GetSection("AllowedCorsOrigins")
        .Get<string[]>();

    policy.WithOrigins(corsOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.MapControllers();
app.Run();