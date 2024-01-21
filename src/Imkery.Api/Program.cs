using Imkery.Api;
using Imkery.Api.Endpoints;
using Imkery.Application;
using Imkery.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddPresentation()
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapApiaryEndpoints();
app.MapHivesEndpoints();

app.Run();
