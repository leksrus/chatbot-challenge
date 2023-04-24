using GaliciaSeguros.IaaS.Service.Chassis.ErrorHandling.WebApi;
using GaliciaSeguros.IaaS.Service.Chassis.Logging;
using ChatService.Infrastructure;
using GaliciaSeguros.IaaS.Service.Chassis.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.UseLoggingWebApi();

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCustomizedSwagger(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseErrorHandlingWebApi();

app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapHealthChecks("/health");        
    });

app.UseCustomizedSwagger(app.Environment);
app.MapControllers();

if (app.Environment.IsDevelopment())
{
}

app.Run();
