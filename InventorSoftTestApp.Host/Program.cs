using System.Text.Json.Serialization;
using FluentMigrator.Runner;
using Hangfire;
using Hangfire.SqlServer;
using InventorSoftTestApp.Application.Controllers;
using InventorSoftTestApp.Application.Handlers.User;
using InventorSoftTestApp.Application.Mappings;
using InventorSoftTestApp.Application.Models.Responses;
using InventorSoftTestApp.Domain.Contexts;
using InventorSoftTestApp.Domain.Jobs;
using InventorSoftTestApp.Domain.Jobs.Abstractions;
using InventorSoftTestApp.Domain.Mappings;
using InventorSoftTestApp.Domain.Models.Enums;
using InventorSoftTestApp.Domain.Repositories;
using InventorSoftTestApp.Domain.Repositories.Abstractions;
using InventorSoftTestApp.Domain.Services;
using InventorSoftTestApp.Domain.Services.Abstractions;
using InventorSoftTestApp.Middlewares;
using InventorSoftTestApp.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

const string persistenceSectionName = "Persistence";

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

IServiceCollection serviceCollection = builder.Services;
ConfigureServices(serviceCollection, builder);
serviceCollection.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "InventorSoft APIs" });
});

var app = builder.Build();
using var scope = app.Services.CreateScope();
UpdateDatabase(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

RegisterHangfireJobs();
app.Run();

void ConfigureServices(IServiceCollection services, WebApplicationBuilder webApplicationBuilder)
{
    services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        })
        .ConfigureApiBehaviorOptions(ConfigureFluentValidationResponse)
        .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
        .AddApplicationPart(typeof(UsersController).Assembly);

    services.AddHttpContextAccessor();
    
    services.AddHangfire(config =>
    {
        config.UseSqlServerStorage(webApplicationBuilder.Configuration.GetSection($"{persistenceSectionName}:ConnectionString").Value);
    });
    JobStorage.Current = new SqlServerStorage(webApplicationBuilder.Configuration.GetSection($"{persistenceSectionName}:ConnectionString").Value);

    services.AddHangfireServer();

    RegisterFluentMigrator(services, webApplicationBuilder.Configuration);

    RegisterServices(services);
    RegisterRepositories(services);
    RegisterHandlers(services);

    services.AddDbContext<InventorSoftDbContext>((sp, options) =>
    {
        options.UseSqlServer(webApplicationBuilder.Configuration.GetSection($"{persistenceSectionName}:ConnectionString").Value);
    });
    
    RegisterJobs(services);

    services.AddAutoMapper(configAction => configAction.AddProfile(new ApplicationMappingsProfile()), typeof(Program));
    services.AddAutoMapper(configAction => configAction.AddProfile(new DomainMappingsProfile()), typeof(Program));
}

static void RegisterFluentMigrator(IServiceCollection services, IConfiguration configuration)
{
    services.AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddSqlServer()
            .WithGlobalConnectionString(configuration.GetSection($"{persistenceSectionName}:ConnectionString").Value)
            .ScanIn(typeof(Migration001_AddUserTable).Assembly).For.Migrations());
}

static void RegisterServices(IServiceCollection services)
{
    services
        .AddScoped<IUserService, UserService>()
        .AddScoped<ITaskService, TaskService>();
}

static void RegisterRepositories(IServiceCollection services)
{
    services
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<ITaskRepository, TaskRepository>();
}

static void RegisterHandlers(IServiceCollection services)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>());
}

static void RegisterJobs(IServiceCollection services)
{
    services
        .AddScoped<ITransferTaskJob, TransferTaskJob>();
}

static void ConfigureFluentValidationResponse(ApiBehaviorOptions options)
{
    options.InvalidModelStateResponseFactory = c =>
    {
        var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage);

        var response = new ErrorResponse
        {
            Code = ErrorCode.ValidationFailed.GetDisplayName(),
            Message = string.Join(" ", errors)
        };

        return new BadRequestObjectResult(response);
    };
}

static void UpdateDatabase(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    Log.Information("Starting migration...");

    runner.MigrateUp();
    runner.ListMigrations();

    Log.Information("Migration finished!");
}

void RegisterHangfireJobs()
{
    RecurringJob.AddOrUpdate<ITransferTaskJob>("Transfer task to another user",
        job => job.Run(), cronExpression: "*/2 * * * *");
}