using Imkery.Api;
using Imkery.Api.Endpoints;
using Imkery.Application;
using Imkery.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddProblemDetails()
    .AddSerilog((services, config) => config
        .ReadFrom.Configuration(builder.Configuration)
        .WriteTo.Console());

builder.Services
    .AddInfrastructure()
    .AddApplication();

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.AddInfrastructureMiddleware();

    if (app.Environment.IsDevelopment())
    {
        app.EnsureDevelopmentInfrastructure();

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapApiaryEndpoints();
    app.MapHivesEndpoints();

    app.Run();
}