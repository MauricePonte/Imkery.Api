using Imkery.Api;
using Imkery.Api.Endpoints;
using Imkery.Application;
using Imkery.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services
    .AddInfrastructure()
    .AddPresentation()
    .AddApplication()
    .AddEndpointsApiExplorer()
    .AddIdentityApiEndpoints<IdentityUser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapIdentityApi<IdentityUser>();
app.MapApiaryEndpoints();
app.MapHivesEndpoints();

app.Run();
