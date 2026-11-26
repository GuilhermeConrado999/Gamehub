var builder = DistributedApplication.CreateBuilder(args);

var server = builder.AddDotnetProject("server", "../Gamehub.AuthService.Api.Server/Gamehub.AuthService.Api.Server.csproj")
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server);

server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.Build().Run();
