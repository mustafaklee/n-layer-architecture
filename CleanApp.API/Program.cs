using App.Application.Extensions;
using App.Persistence.Extensions;
using CleanApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration).AddControllersExt()
    .ExceptionHandlerExt().AddSwaggerGenExt().CachingExtension();

var app = builder.Build();

app.ConfigurePipelineExt();

app.MapControllers();

app.Run();
