using PPM_API.Endpoints;
using PPM_API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

DependencyExtensions.RegisterDependencies(builder);

var app = builder.Build();

DependencyExtensions.UseBuilderMethods(app);

AppEndpoints.GETEndpoints(app);
AppEndpoints.POSTEndpoints(app);
AppEndpoints.DELETEEndpoints(app);

app.Run();