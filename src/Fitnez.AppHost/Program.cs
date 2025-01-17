var builder = DistributedApplication.CreateBuilder(args);

var pgUsername = builder.AddParameter("pg-username", "postgres");
var pgPassword = builder.AddParameter("pg-password", "postgres");
var postgres = builder.AddPostgres("postgres", pgUsername, pgPassword, 5440)
    .WithContainerName("fitnez-postgres")
    .WithDataVolume(isReadOnly: false)    
    .WithExternalHttpEndpoints();

var postgresDb = postgres.AddDatabase("adm", "postgres");
var fitnezDb = postgres.AddDatabase("db", "fitnez");

var identity = builder.AddProject<Projects.Fitnez_Identity>("fitnez-identity")
    .WithReference(fitnezDb)
    .WaitFor(fitnezDb)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Fitnez_App>("fitnez-app")
    .WithReference(postgresDb)
    .WithReference(fitnezDb)
    .WaitFor(fitnezDb)
    .WithReference(identity)
    .WaitFor(identity)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Fitnez_Web>("fitnez-web")
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Share_UI_Test>("share-ui-test")
    .WithExternalHttpEndpoints();

builder.Build().Run();