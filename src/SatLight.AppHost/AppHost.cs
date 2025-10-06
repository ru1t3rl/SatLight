using SatLight.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
var composeEnv = builder.AddDockerComposeEnvironment("compose");

var cache = builder.AddRedis("cache");

var postgres = builder
	.AddPostgres("postgres")
	.WithDbGate()
	.WithDataVolume()
	.WithLifetime(ContainerLifetime.Persistent)
	.WithHostPort(5432);

var database = postgres
	.AddDatabase("SatLight");

var apiService = builder
	.AddProject<Projects.SatLight_WebApi>("webapi")
	.WithHttpHealthCheck("/health")
	.WithReference(database)
	.WithMigrator<Projects.SatLight_Migrator>(database);

var unity = builder
	.AddUnityProject("unity", "../SatLight.Unity", -1);

builder.Build().Run();