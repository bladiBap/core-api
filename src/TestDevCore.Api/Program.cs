using TestDevCore.Api.Extension;
using TestDevCore.Api.Extensions;
using TestDevCore.Application;
using TestDevCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddApiPrefix("api");
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersionConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
