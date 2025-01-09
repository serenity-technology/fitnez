var builder = DistributedApplication.CreateBuilder(args);

var pgUsername = builder.AddParameter("pg-username", "postgres");
var pgPassword = builder.AddParameter("pg-password", "postgres");
var postgres = builder.AddPostgres("postgres", pgUsername, pgPassword, 5440)
    .WithContainerName("fitnez-postgres")
    .WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("postgres-db", "fitnez");

var identity = builder.AddProject<Projects.Fitnez_Identity>("fitnez-identity")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.Fitnez_App>("fitnez-app")
    .WithReference(postgresdb)
    .WaitFor(postgresdb)
    .WithReference(identity)
    .WaitFor(identity);

builder.AddProject<Projects.Fitnez_Web>("fitnez-web");

builder.AddProject<Projects.Share_UI_Test>("share-ui-test");

builder.Build().Run();