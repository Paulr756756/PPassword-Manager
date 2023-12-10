using PPM_API.Endpoints;
using PPM_API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

DependencyExtensions.RegisterDependencies(builder);

var app = builder.Build();

DependencyExtensions.UseBuilderMethods(app);

AppEndpoints.GetEndpoints(app);
AppEndpoints.PostEndpoints(app);
AppEndpoints.DeleteEndpoints(app);

app.Run();