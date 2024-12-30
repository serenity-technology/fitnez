var builder = DistributedApplication.CreateBuilder(args);

var pgUsername = builder.AddParameter("pg-username", "postgres");
var pgPassword = builder.AddParameter("pg-password", "postgres");
var postgres = builder.AddPostgres("postgres", pgUsername, pgPassword)
    .WithContainerName("fitnez-postgres")
    .WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("postgres-db", "fitnez");

var identity = builder.AddProject<Projects.Fitnez_Identity>("fitnez-identity")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.Fitnez_App>("fitnez-app")
    .WithReference(postgresdb)
    .WaitFor(postgresdb)
    .WaitFor(identity);

builder.AddProject<Projects.Fitnez_Web>("fitnez-web");

builder.Build().Run();