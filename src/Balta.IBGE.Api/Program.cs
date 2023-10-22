using Balta.IBGE.Application;
using Balta.IBGE.Application.Behavior;
using Balta.IBGE.Domain.Accounts.Repositories;
using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;
using Balta.IBGE.Domain.Core.Configuration;
using Balta.IBGE.Domain.Core.Extensions;
using Balta.IBGE.Domain.Core.Services;
using Balta.IBGE.Infra;
using Balta.IBGE.Infra.Core.Services;
using Balta.IBGE.Infra.Database;
using Balta.IBGE.Infra.UseCases.Accounts;
using Balta.IBGE.Infra.UseCases.Cities;

using Carter;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<IBGEDbContext>(options =>
{
    options.UseSqlServer(connectionString,
        b =>
        {
            b.MigrationsAssembly(typeof(InfraAssemblyReference).Assembly.ToString());
            b.EnableRetryOnFailure(3);
        });

    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging(true);
    options.LogTo(action: Console.WriteLine,
                  categories: new[]
                  {
                  DbLoggerCategory.Database.Command.Name
                  },
                  minimumLevel: LogLevel.Information);
});

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
builder.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.TryAddScoped<ICityRepository, CityRepository>();
builder.Services.TryAddScoped<IUserRepository, UserRepository>();
builder.Services.TryAddScoped<ISendEmailService, SendEmailService>();

builder.Services.AddCarter();

builder.Services.AddOptions<Configuration.SecretsConfiguration>()
    .BindConfiguration("Secrets")
    .Validate(secretsSettings =>
    {
        if (secretsSettings.JwtPrivateKey.IsNullOrWhiteSpace())
            return false;

        if (secretsSettings.PasswordSaltKey.IsNullOrWhiteSpace())
            return false;

        return true;
    });

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter();

using IServiceScope serviceScope = app.Services.CreateScope();
using IBGEDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<IBGEDbContext>();
dbContext.Database.Migrate();

app.Run();
