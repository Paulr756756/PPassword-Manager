using PPM_API.Endpoints;
using PPM_API.Extensions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

DependencyExtensions.RegisterDependencies(builder);

WebApplication? app = builder.Build();

DependencyExtensions.UseBuilderMethods(app);

AppEndpoints.GETEndpoints(app);
AppEndpoints.POSTEndpoints(app);
AppEndpoints.DELETEEndpoints(app);

app.Run();