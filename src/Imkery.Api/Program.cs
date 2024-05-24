using Imkery.Api;
using Imkery.Api.Endpoints;
using Imkery.Application;
using Imkery.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddPresentation();

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